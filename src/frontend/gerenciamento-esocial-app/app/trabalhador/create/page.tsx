"use client";

import React, { useState, useEffect } from "react";
import { NavBar } from "../../components/NavBar/NavBar";
import { createTrabalhador } from "../../lib/api";
import { useRouter } from "next/navigation";
import { DependenteModal } from "@/app/components/DependenteModal/DependenteModal";
import { useAuth } from '../../../context/AuthProvider'

// ====== Tipos ======
export type TipoVinculo = 1 | 2 | 3; // CLT=1, Estagiário=2, Outro=3
export type Sexo = "Masculino" | "Feminino" | "";
export type RacaCor = "Branco" | "Negro" | "Pardo" | "Indígena" | "Amarelo" | "";
export type EstadoCivil = "Solteiro" | "Casado" | "Divorciado" | "Viúvo" | "";
export type GrauInstrucao = "Fundamental" | "Medio" | "Superior" | "Pos" | "Mestrado" | "Doutorado" | "";

export interface Dependente {
  nomeDependente: string;
  tipoDependente: 1 | 2 | 3; // Filho=1, Cônjuge=2, Outro=3
}

export interface TrabalhadorForm {
  nome: string;
  tipo: TipoVinculo;
  sexo: Sexo;
  racaCor: RacaCor;
  estadoCivil: EstadoCivil;
  grauInstrucao: GrauInstrucao;
  isPrimeiroEmprego: boolean;
  codigoNomeTravTrans: string;
  dataNascimento: string;
  municipioNascimento: string;
  ufNascimento: string;
  paisNascimento: string;
  nacionalidade: string;
  nomeMae: string;
  nomePai: string;
  recebeBeneficioPrevidencia: boolean;
  possuiDependente: boolean;
  dependentes: Dependente[];
}

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

export default function CreateTrabalhador() {
  const router = useRouter();
  const { user, loading } = useAuth();

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
        <h2 className="text-2xl font-semibold mb-6">Novo Trabalhador</h2>

        {/* ===== Dados Pessoais ===== */}
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

        {/* ===== Nascimento ===== */}
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

        {/* ===== Filiação ===== */}
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

        {/* Outros Dados */}
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

              {/* Lista de dependentes */}
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

        {/* Ações */}
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
      {/* Modal reutilizável */}
      <DependenteModal
        open={openModal}
        onClose={() => setOpenModal(false)}
        onSave={(dep) => {
          setDependentes([...dependentes, dep]);
          setOpenModal(false);
        }}
      />
    </div>
  );
}
