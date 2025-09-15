"use client";

import React, { useState, useEffect } from "react";
import { NavBar } from "../../components/NavBar/NavBar";
import { createTrabalhador } from "../../lib/api";
import { useRouter } from "next/navigation";
import { TipoVinculo, Sexo, RacaCor, EstadoCivil, GrauInstrucao, StatusCadastro } from "@/models/Trabalhador";
import { TrabalhadorForm } from "@/models/Trabalhador";
import { Dependente } from "@/models/Dependente";
import { DependenteModal } from "@/app/components/DependenteModal/DependenteModal";
import { useAuth } from '../../../context/AuthProvider'

// ====== Mapeamentos para backend ======
const sexoMap: Record<Sexo, number> = { "": 0, Masculino: 1, Feminino: 2 };
const racaCorMap: Record<RacaCor, number> = {
  "": 0,
  Branco: 1,
  Negro: 2,
  Pardo: 3,
  Indígena: 4,
  Amarelo: 5,
};
const estadoCivilMap: Record<EstadoCivil, number> = {
  "": 0,
  Solteiro: 1,
  Casado: 2,
  Divorciado: 3,
  Viúvo: 4,
};
const grauInstrucaoMap: Record<GrauInstrucao, number> = {
  "": 0,
  Fundamental: 1,
  Medio: 2,
  Superior: 3,
  Pos: 4,
  Mestrado: 5,
  Doutorado: 6,
};
const statusCadastroMap: Record<StatusCadastro, number> = {
  Criado: 0,
  Pendente: 1,
  Concluido: 2
};


export default function CreateTrabalhador() {
  const router = useRouter();
  const { user, loading } = useAuth();
  const [activeTab, setActiveTab] = useState<"pessoal" | "documentos" | "endereco" | "deficiencia" | "contato">("pessoal");

  const [form, setForm] = useState<TrabalhadorForm>({
    nome: "",
    tipo: 1,
    sexo: "",
    racaCor: "",
    estadoCivil: "",
    grauInstrucao: "",
    isPrimeiroEmprego: false,
    codigoNomeTravTrans: "",
    dataNascimento: "",
    municipioNascimento: "",
    ufNascimento: "",
    paisNascimento: "",
    nacionalidade: "",
    nomeMae: "",
    nomePai: "",
    documentosPessoais: {
      cpf: "",
      nisPisPasep: "",
      numeroCtps: "",
      numeroSerieCtps: "",
      ufCtps: "",
      numeroRg: "",
      emissaoRg: "",
      dataExpedicaoRg: "",
      numeroRegistroOc: "",
      emissaoOc: "",
      dataExpedOc: "",
      dataValidadeOc: "",
      numeroCnh: "",
      dataExpedicaoCnh: "",
      ufCnh: "",
      dataValidadeCnh: "",
      dataPrimeiraHabilitacao: "",
      categoriaCnh: "",
    },
    enderecoResidencial: {
      tipoLogradouro: "",
      logradouro: "",
      numero: "",
      complemento: "",
      bairro: "",
      cep: "",
      municipio: "",
      uf: "",
    },
    dadosDeficiencia: {
      temDeficienciaFisica: false,
      temDeficienciaVisual: false,
      temDeficienciaAuditiva: false,
      temDeficienciaMental: false,
      temDeficienciaIntelectual: false,
    },
    contato: {
      telefone1: "",
      telefone2: "",
      email1: "",
      email2: "",
      descricao: "",
    },
    statusCadastro: "Criado",
    recebeBeneficioPrevidencia: false,
    possuiDependente: false,
    dependentes: [],
  });

  const [saving, setSaving] = useState(false);
  const [dependentes, setDependentes] = useState<any[]>([]);
  const [openModal, setOpenModal] = useState(false);

  useEffect(() => {
    if (!loading && !user) {
      router.push("/"); // redireciona se não estiver logado
    }
  }, [user, loading]);

  // Redireciona se não estiver logado
  useEffect(() => {
    if (!loading && !user) router.push("/");
  }, [user, loading]);

  // ====== Handle Change ======
  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.currentTarget;
    const checked = (e.currentTarget as HTMLInputElement).checked;

    let val: any = type === "checkbox" ? checked : value;

    // Converte tipo para número
    if (name === "tipo") val = parseInt(value);

    setForm({ ...form, [name]: val });
  };

  // ====== Handle Save ======
  const handleSave = async () => {
    setSaving(true);
    try {
      const payload = {
        UserId: user?.id ?? "",
        Tipo: form.tipo,
        Nome: form.nome,
        Sexo: sexoMap[form.sexo],
        RacaCor: racaCorMap[form.racaCor],
        EstadoCivil: estadoCivilMap[form.estadoCivil],
        GrauInstrucao: grauInstrucaoMap[form.grauInstrucao],
        IsPrimeiroEmprego: form.isPrimeiroEmprego,
        CodigoNomeTravTrans: form.codigoNomeTravTrans,
        DataNascimento: form.dataNascimento ? new Date(form.dataNascimento) : null,
        MunicipioNascimento: form.municipioNascimento,
        UfNascimento: form.ufNascimento,
        PaisNascimento: form.paisNascimento,
        Nacionalidade: form.nacionalidade,
        NomeMae: form.nomeMae,
        NomePai: form.nomePai,
        RecebeBeneficioPrevidencia: form.recebeBeneficioPrevidencia,
        StatusCadastro: 1,
        DataCadastro: new Date(),
        DataUltimaAtualizacao: null,
        DocumentosPessoais: null,
        EnderecoResidencial: null,
        DadosDeficiencia: null,
        Contato: null,
        dependentes: dependentes.map((d) => ({
          nomeDependente: d.nomeDependente,
          tipoDependente: d.tipoDependente,
        })),
      };

      console.log('PAYLOAD: ', payload);

      await createTrabalhador(payload); // enviado dentro de command
      router.push("/summary");
    } catch (err) {
      console.error(err);
    }
    setSaving(false);
  };

  return (
    <div>
      <NavBar />
      <div className="p-6 max-w-4xl mx-auto">
      <h2 className="text-2xl font-semibold mb-6">Cadastro de Novo Trabalhador</h2>
      <div className="flex border-b mb-4">
        {[
          { key: "pessoal", label: "Dados Pessoais" },
          { key: "documentos", label: "Documentos" },
          { key: "endereco", label: "Endereço" },
          { key: "contato", label: "Contato" },
          { key: "deficiencia", label: "Deficiência" }
        ].map((tab) => (
          <button
            key={tab.key}
            onClick={() => setActiveTab(tab.key as any)}
            className={`px-4 py-2 -mb-px border-b-2 cursor-pointer 
              ${activeTab === tab.key
                ? "border-blue-600 font-semibold"
                : "border-transparent"
              }`}
          >
            {tab.label}
          </button>
        ))}
      </div>

      {/*{activeTab === "pessoal" && ( */}
      <div className={`${activeTab === "pessoal" ? "block pointer-events-auto" : "hidden pointer-events-none"}`}>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Nome</label>
            <input
              name="nome"
              value={form.nome}
              onChange={handleChange}
              className="border w-full p-2"
              placeholder="Nome completo"
            />
          </div>

          <div>
            <label className="block">Tipo de vínculo</label>
            <select
              name="tipo"
              value={form.tipo}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value={1}>CLT</option>
              <option value={2}>Estagiário</option>
              <option value={3}>Outro</option>
            </select>
          </div>

          <div>
            <label className="block">Sexo</label>
            <select
              name="sexo"
              value={form.sexo}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="">Selecione</option>
              <option value="Masculino">Masculino</option>
              <option value="Feminino">Feminino</option>
            </select>
          </div>

          <div>
            <label className="block">Raça/Cor</label>
            <select
              name="racaCor"
              value={form.racaCor}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="">Selecione</option>
              <option value="Branco">Branco</option>
              <option value="Negro">Negro</option>
              <option value="Pardo">Pardo</option>
              <option value="Indígena">Indígena</option>
              <option value="Amarelo">Amarelo</option>
            </select>
          </div>

          <div>
            <label className="block">Estado Civil</label>
            <select
              name="estadoCivil"
              value={form.estadoCivil}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="">Selecione</option>
              <option value="Solteiro">Solteiro</option>
              <option value="Casado">Casado</option>
              <option value="Divorciado">Divorciado</option>
              <option value="Viúvo">Viúvo</option>
            </select>
          </div>

          <div>
            <label className="block">Grau de Instrução</label>
            <select
              name="grauInstrucao"
              value={form.grauInstrucao}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="">Selecione</option>
              <option value="Fundamental">Fundamental</option>
              <option value="Medio">Médio</option>
              <option value="Superior">Superior</option>
              <option value="Pos">Pós-Graduação</option>
              <option value="Mestrado">Mestrado</option>
              <option value="Doutorado">Doutorado</option>
            </select>
          </div>
        </div>

        <h3 className="text-lg font-semibold mb-2">Dados de Nascimento</h3>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Data de Nascimento</label>
            <input
              type="date"
              name="dataNascimento"
              value={form.dataNascimento}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
          <div>
            <label className="block">Município de Nascimento</label>
            <input
              name="municipioNascimento"
              value={form.municipioNascimento}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
          <div>
            <label className="block">UF Nascimento</label>
            <input
              name="ufNascimento"
              value={form.ufNascimento}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
          <div>
            <label className="block">País de Nascimento</label>
            <input
              name="paisNascimento"
              value={form.paisNascimento}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
          <div>
            <label className="block">Nacionalidade</label>
            <input
              name="nacionalidade"
              value={form.nacionalidade}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
        </div>

        <h3 className="text-lg font-semibold mb-2">Filiação</h3>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Nome da Mãe</label>
            <input
              name="nomeMae"
              value={form.nomeMae}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
          <div>
            <label className="block">Nome do Pai</label>
            <input
              name="nomePai"
              value={form.nomePai}
              onChange={handleChange}
              className="border w-full p-2"
            />
          </div>
        </div>


        <div className="mb-6">
          <label className="flex items-center gap-2">
            <input
              type="checkbox"
              name="isPrimeiroEmprego"
              checked={form.isPrimeiroEmprego}
              onChange={handleChange}
            />
            Primeiro Emprego
          </label>

          <label className="flex items-center gap-2 mt-2">
            <input
              type="checkbox"
              name="recebeBeneficioPrevidencia"
              checked={form.recebeBeneficioPrevidencia}
              onChange={handleChange}
            />
            Recebe Benefício Previdência
          </label>

          <label className="flex items-center gap-2 mt-2">
            <input
              type="checkbox"
              name="possuiDependente"
              checked={form.possuiDependente}
              onChange={handleChange}
            />
            Possui Dependente
          </label>

          {form.possuiDependente && (
            <div className="mt-3">
              <button
                type="button"
                onClick={() => setOpenModal(true)}
                className="px-4 py-2 bg-blue-600 text-white rounded cursor-pointer"
              >
                Cadastrar Dependente
              </button>

              {dependentes.length > 0 && (
                <div className="mt-4">
                  <h3 className="text-lg font-semibold mb-2">Dependentes</h3>
                  <ul className="list-disc pl-5">
                    {dependentes.map((d, i) => (
                      <li key={i}>
                        {d.nomeDependente} —{" "}
                        {d.tipoDependente === 1
                          ? "Filho(a)"
                          : d.tipoDependente === 2
                            ? "Cônjuge"
                            : "Outro"}
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </div>
          )}

        </div>

        <div className="flex gap-3 mt-6">
          <button
            onClick={handleSave}
            className="px-4 py-2 bg-green-600 text-white rounded cursor-pointer"
            disabled={saving}
          >
            {saving ? "Salvando..." : "Finalizar cadastro"}
          </button>
        </div>
      </div>
      {/*})}*/}
      {/*{activeTab === "documentos" && (*/}
      <div className={`${activeTab === "documentos" ? "block pointer-events-auto" : "hidden pointer-events-none"}`} >
        <h3 className="text-lg font-semibold mb-2">Documentos</h3>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">CPF:</label>
            <input
              type="text"
              placeholder="CPF"
              className="border p-2 w-full"
              value={form.documentosPessoais?.cpf}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Pis Pasep</label>
            <input
              type="text"
              placeholder="Número Pis Pasep"
              className="border p-2 w-full"
              value={form.documentosPessoais?.nisPisPasep || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Número CTPS:</label>
            <input
              type="text"
              placeholder="Número CTPS"
              className="border p-2 w-full"
              value={form.documentosPessoais?.numeroCtps || ""}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Número Série CTPS</label>
            <input
              type="text"
              placeholder="Número Série Ctps"
              className="border p-2 w-full"
              value={form.documentosPessoais?.numeroSerieCtps || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Emissão CTPS:</label>
            <select
              name="ufCtps"
              value={form.documentosPessoais?.ufCtps || ""}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="AC">AC</option>
              <option value="AL">AL</option>
              <option value="AM">AM</option>
              <option value="AP">AP</option>
              <option value="BA">BA</option>
              <option value="CE">CE</option>
              <option value="DF">DF</option>
              <option value="ES">ES</option>
              <option value="GO">GO</option>
              <option value="MA">MA</option>
              <option value="MG">MG</option>
              <option value="MS">MS</option>
              <option value="MT">MT</option>
              <option value="PA">PA</option>
              <option value="PB">PB</option>
              <option value="PE">PE</option>
              <option value="PI">PI</option>
              <option value="PR">PR</option>
              <option value="RJ">RJ</option>
              <option value="RN">RN</option>
              <option value="RO">RO</option>
              <option value="RS">RS</option>
              <option value="SC">SC</option>
              <option value="SE">SE</option>
              <option value="SP">SP</option>
              <option value="TO">TO</option>
            </select>
          </div>
          <div>
            <label className="block">Número RG</label>
            <input
              type="text"
              placeholder="RG"
              className="border p-2 w-full"
              value={form.documentosPessoais?.numeroRg || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Emissão RG:</label>
            <select
              name="emissaoRg"
              value={form.documentosPessoais?.emissaoRg || ""}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="AC">AC</option>
              <option value="AL">AL</option>
              <option value="AM">AM</option>
              <option value="AP">AP</option>
              <option value="BA">BA</option>
              <option value="CE">CE</option>
              <option value="DF">DF</option>
              <option value="ES">ES</option>
              <option value="GO">GO</option>
              <option value="MA">MA</option>
              <option value="MG">MG</option>
              <option value="MS">MS</option>
              <option value="MT">MT</option>
              <option value="PA">PA</option>
              <option value="PB">PB</option>
              <option value="PE">PE</option>
              <option value="PI">PI</option>
              <option value="PR">PR</option>
              <option value="RJ">RJ</option>
              <option value="RN">RN</option>
              <option value="RO">RO</option>
              <option value="RS">RS</option>
              <option value="SC">SC</option>
              <option value="SE">SE</option>
              <option value="SP">SP</option>
              <option value="TO">TO</option>
            </select>
          </div>
          <div>
            <label className="block">Expedição Rg</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataExpedicaoRg || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Número Registro Oc:</label>
            <input
              type="text"
              placeholder="Número Registro Oc"
              className="border p-2 w-full"
              value={form.documentosPessoais?.numeroRegistroOc || ""}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Emissão Oc</label>
            <select
              name="emissaoOc"
              value={form.documentosPessoais?.emissaoOc || ""}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="AC">AC</option>
              <option value="AL">AL</option>
              <option value="AM">AM</option>
              <option value="AP">AP</option>
              <option value="BA">BA</option>
              <option value="CE">CE</option>
              <option value="DF">DF</option>
              <option value="ES">ES</option>
              <option value="GO">GO</option>
              <option value="MA">MA</option>
              <option value="MG">MG</option>
              <option value="MS">MS</option>
              <option value="MT">MT</option>
              <option value="PA">PA</option>
              <option value="PB">PB</option>
              <option value="PE">PE</option>
              <option value="PI">PI</option>
              <option value="PR">PR</option>
              <option value="RJ">RJ</option>
              <option value="RN">RN</option>
              <option value="RO">RO</option>
              <option value="RS">RS</option>
              <option value="SC">SC</option>
              <option value="SE">SE</option>
              <option value="SP">SP</option>
              <option value="TO">TO</option>
            </select>
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Expedição Oc:</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataExpedOc || ""}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Validade Oc</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataValidadeOc || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Número CNH:</label>
            <input
              type="text"
              placeholder="Número Cnh"
              className="border p-2 w-full"
              value={form.documentosPessoais?.numeroCnh || ""}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Expedição CNH</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataExpedicaoCnh || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Uf CNH:</label>
            <select
              name="ufCnh"
              value={form.documentosPessoais?.ufCnh || ""}
              onChange={handleChange}
              className="border w-full p-2"
            >
              <option value="AC">AC</option>
              <option value="AL">AL</option>
              <option value="AM">AM</option>
              <option value="AP">AP</option>
              <option value="BA">BA</option>
              <option value="CE">CE</option>
              <option value="DF">DF</option>
              <option value="ES">ES</option>
              <option value="GO">GO</option>
              <option value="MA">MA</option>
              <option value="MG">MG</option>
              <option value="MS">MS</option>
              <option value="MT">MT</option>
              <option value="PA">PA</option>
              <option value="PB">PB</option>
              <option value="PE">PE</option>
              <option value="PI">PI</option>
              <option value="PR">PR</option>
              <option value="RJ">RJ</option>
              <option value="RN">RN</option>
              <option value="RO">RO</option>
              <option value="RS">RS</option>
              <option value="SC">SC</option>
              <option value="SE">SE</option>
              <option value="SP">SP</option>
              <option value="TO">TO</option>
            </select>
          </div>
          <div>
            <label className="block">Validade CNH</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataValidadeCnh || ""}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="grid grid-cols-2 gap-4 mb-6">
          <div>
            <label className="block">Primeira Habilitação:</label>
            <input
              type="date"
              className="border p-2 w-full"
              value={form.documentosPessoais?.dataPrimeiraHabilitacao || ""}
              onChange={handleChange}
            />
          </div>
          <div>
            <label className="block">Categoria CNH</label>
            <input
              type="text"
              placeholder="Categoria Cnh"
              className="border p-2 w-full"
              value={form.documentosPessoais?.categoriaCnh || ""}
              onChange={handleChange}
            />
          </div>
        </div>
      </div>
      {/*})}*/}

      {activeTab === "endereco" && (
        <div className={activeTab === "endereco" ? "block" : "hidden"}>
          <h3 className="text-lg font-semibold mb-2">Endereço</h3>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <label className="block">Tipo de Logradouro:</label>
              <select
                name="tipoLogradouro"
                value={form.enderecoResidencial?.tipoLogradouro || ""}
                onChange={handleChange}
                className="border w-full p-2"
              >
                <option value="">Selecione</option>
                <option value="Avenida">Avenida</option>
                <option value="Rua">Rua</option>
                <option value="Travessa">Travessa</option>
                <option value="Alameda">Alameda</option>
                <option value="Praça">Praça</option>
                <option value="Largo">Largo</option>
                <option value="Rodovia">Rodovia</option>
                <option value="Viela">Viela</option>
                <option value="Beira">Beira</option>
                <option value="Pátio">Pátio</option>
                <option value="Setor">Setor</option>
              </select>
            </div>
            <div>
              <label className="block">Logradouro:</label>
              <input
                type="text"
                placeholder="Logradouro"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.logradouro || ""}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <label className="block">Número:</label>
              <input
                type="text"
                placeholder="Númeroo"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.numero || ""}
                onChange={handleChange}
              />
            </div>
            <div>
              <label className="block">Complemento:</label>
              <input
                type="text"
                placeholder="Complemento"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.complemento || ""}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <label className="block">Bairro:</label>
              <input
                type="text"
                placeholder="Bairro"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.bairro || ""}
                onChange={handleChange}
              />
            </div>
            <div>
              <label className="block">CEP:</label>
              <input
                type="text"
                placeholder="CEP"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.cep || ""}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <label className="block">Município:</label>
              <input
                type="text"
                placeholder="Município"
                className="border p-2 w-full"
                value={form.enderecoResidencial?.municipio || ""}
                onChange={handleChange}
              />
            </div>
            <div>
              <label className="block">UF:</label>
              <select
                name="uf"
                value={form.enderecoResidencial?.uf || ""}
                onChange={handleChange}
                className="border w-full p-2"
              >
                <option value="AC">AC</option>
                <option value="AL">AL</option>
                <option value="AM">AM</option>
                <option value="AP">AP</option>
                <option value="BA">BA</option>
                <option value="CE">CE</option>
                <option value="DF">DF</option>
                <option value="ES">ES</option>
                <option value="GO">GO</option>
                <option value="MA">MA</option>
                <option value="MG">MG</option>
                <option value="MS">MS</option>
                <option value="MT">MT</option>
                <option value="PA">PA</option>
                <option value="PB">PB</option>
                <option value="PE">PE</option>
                <option value="PI">PI</option>
                <option value="PR">PR</option>
                <option value="RJ">RJ</option>
                <option value="RN">RN</option>
                <option value="RO">RO</option>
                <option value="RS">RS</option>
                <option value="SC">SC</option>
                <option value="SE">SE</option>
                <option value="SP">SP</option>
                <option value="TO">TO</option>
              </select>
            </div>
          </div>
        </div>
      )}

      {activeTab === "contato" && (
        <div className={activeTab === "contato" ? "block" : "hidden"}>
          <h3 className="text-lg font-semibold mb-2">Dados de Contato</h3>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <input
                type="text"
                placeholder="Telefone Principal"
                className="border p-2 w-full"
                value={form.contato?.telefone1 || ""}
                onChange={handleChange}
              />
            </div>
            <div>
              <input
                type="text"
                placeholder="Telefone Alternativo"
                className="border p-2 w-full"
                value={form.contato?.telefone2 || ""}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <input
                type="email"
                placeholder="Email Principal"
                className="border p-2 w-full"
                value={form.contato?.email1 || ""}
                onChange={handleChange}
              />
            </div>
            <div>
              <input
                type="email"
                placeholder="Email Alternativo"
                className="border p-2 w-full"
                value={form.contato?.email2 || ""}
                onChange={handleChange}
              />
            </div>
          </div>
          <div className="grid grid-cols-2 gap-4 mb-6">
            <div>
              <input
                type="text"
                placeholder="Descrição do contato"
                className="border p-2 w-full"
                value={form.contato?.descricao || ""}
                onChange={handleChange}
              />
            </div>
          </div>
        </div>
      )}

      {activeTab === "deficiencia" && (
        <div className={activeTab === "deficiencia" ? "block" : "hidden"}>
          <div className="space-y-2">
            {Object.keys(form.dadosDeficiencia!).map((key) => (
              <label key={key} className="flex items-center gap-2">
                <input
                  type="checkbox"
                  checked={(form.dadosDeficiencia as any)[key]}
                  onChange={(e) =>
                    setForm({
                      ...form,
                      dadosDeficiencia: {
                        ...form.dadosDeficiencia,
                        [key]: e.target.checked,
                      },
                    })
                  }
                />
                {key}
              </label>
            ))}
          </div>
        </div>
      )}
    </div>
      {/* Modal reutilizável */ }
  <DependenteModal
    open={openModal}
    onClose={() => setOpenModal(false)}
    onSave={(dep) => {
      setDependentes([...dependentes, dep]);
      setOpenModal(false);
    }}
  />
    </div >
  );
}
