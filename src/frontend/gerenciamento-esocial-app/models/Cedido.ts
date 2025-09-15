// ====== Enums ======
export enum TipoRegimeTrabalhista {
  CLT = 0,
  Estatutario = 1,
}

export enum TipoRegimePrevidenciario {
  RGPS = 0,
  RPPS = 1,
  Exterior = 2,
}

export enum OnusCessaoRequisicao {
  Cedente = 0,
  Cessionario = 1,
  Ambos = 2,
}

export enum CategoriaTrabalhador {
  EmpregadoGeral = 101,
  ServidorPublicoEfetivo = 301,
  ServidorCargoComissao = 302,
  ServidorPublicoTemporario = 306,
}

// ====== Interface ======
export interface Cedido {
  cedidoId: number;
  trabalhadorId: number;
  cnpjEmpregadoCedido?: string;
  matriculaTrabalhador?: string;
  dataAdmissao?: Date;
  tipoRegTrab: TipoRegimeTrabalhista;
  tipoRegPrev: TipoRegimePrevidenciario;
  onusCessReq: OnusCessaoRequisicao;
  categoria: CategoriaTrabalhador;
}

export function mapCedidoFromApi(apiData: any): Cedido {
  return {
    cedidoId: apiData.CedidoId,
    trabalhadorId: apiData.TrabalhadorId,
    cnpjEmpregadoCedido: apiData.CnpjEmpregadoCedido ?? undefined,
    matriculaTrabalhador: apiData.MatriculaTrabalhador ?? undefined,
    dataAdmissao: apiData.DataAdmissao ? new Date(apiData.DataAdmissao) : undefined,
    tipoRegTrab: apiData.TipoRegTrab as TipoRegimeTrabalhista,
    tipoRegPrev: apiData.TipoRegPrev as TipoRegimePrevidenciario,
    onusCessReq: apiData.OnusCessReq as OnusCessaoRequisicao,
    categoria: apiData.Categoria as CategoriaTrabalhador,
  };
}
