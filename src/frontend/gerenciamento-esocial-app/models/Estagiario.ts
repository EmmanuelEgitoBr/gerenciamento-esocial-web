// ====== Enums ======
export enum NaturezaEstagio {
  Obrigatoria = 0,
  NaoObrigatoria = 1,
}

export enum AreaAtuacao {
  Exatas = 0,
  Humanas = 1,
  CienciasMedicas = 2,
}

// ====== Interface ======
export interface Estagiario {
  estagiarioId: number;
  trabalhadorId: number;
  naturezaEstagio: NaturezaEstagio;
  areaAtuacao: AreaAtuacao;
  razaoSocialInstEnsino?: string;
  cnpjInstEnsino?: string;
  nomeSupervisor?: string;
}

// ====== Função de mapeamento do backend para TypeScript ======
export function mapEstagiarioFromApi(apiData: any): Estagiario {
  return {
    estagiarioId: apiData.EstagiarioId,
    trabalhadorId: apiData.TrabalhadorId,
    naturezaEstagio: apiData.NaturezaEstagio as NaturezaEstagio,
    areaAtuacao: apiData.AreaAtuacao as AreaAtuacao,
    razaoSocialInstEnsino: apiData.RazaoSocialInstEnsino ?? undefined,
    cnpjInstEnsino: apiData.CnpjInstEnsino ?? undefined,
    nomeSupervisor: apiData.NomeSupervisor ?? undefined,
  };
}