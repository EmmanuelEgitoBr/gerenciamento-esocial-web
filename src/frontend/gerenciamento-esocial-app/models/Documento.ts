export interface Documento {
  cpf?: string;
  nisPisPasep?: string;
  numeroCtps?: string;
  numeroSerieCtps?: string;
  ufCtps?: string;
  numeroRg?: string;
  emissaoRg?: string;
  dataExpedicaoRg?: string; // Date -> string (ISO)
  numeroRegistroOc?: string;
  emissaoOc?: string;
  dataExpedOc?: string;
  dataValidadeOc?: string;
  numeroCnh?: string;
  dataExpedicaoCnh?: string;
  ufCnh?: string;
  dataValidadeCnh?: string;
  dataPrimeiraHabilitacao?: string;
  categoriaCnh?: string; // CategoriaCnh (A, B, AB etc)
}