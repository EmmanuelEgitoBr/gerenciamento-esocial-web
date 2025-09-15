import { Contato } from "./Contato";
import { Dependente } from "./Dependente";
import { Documento } from "./Documento";
import { Endereco } from "./Endereco";
import { Deficiencia } from "./Deficiencia";

// ====== Tipos ======
export type TipoVinculo = 1 | 2 | 3; // CLT=1, Estagiário=2, Outro=3
export type Sexo = "Masculino" | "Feminino" | "";
export type RacaCor = "Branco" | "Negro" | "Pardo" | "Indígena" | "Amarelo" | "";
export type EstadoCivil = "Solteiro" | "Casado" | "Divorciado" | "Viúvo" | "";
export type GrauInstrucao = "Fundamental" | "Medio" | "Superior" | "Pos" | "Mestrado" | "Doutorado" | "";
export type StatusCadastro = "Criado" | "Pendente" | "Concluido";

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
  documentosPessoais?: Documento;
  enderecoResidencial?: Endereco;
  dadosDeficiencia?: Deficiencia;
  recebeBeneficioPrevidencia: boolean;
  contato?: Contato;
  statusCadastro: StatusCadastro;
  possuiDependente: boolean;
  dependentes: Dependente[];
}