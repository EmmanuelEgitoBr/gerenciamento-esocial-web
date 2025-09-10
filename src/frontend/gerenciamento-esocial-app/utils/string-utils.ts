/**
 * Verifica se a string é nula ou vazia.
 * Equivalente ao C# string.IsNullOrEmpty
 */
export function isNullOrEmpty(value?: string | null): boolean {
  return value === null || value === undefined || value === "";
}

/**
 * Verifica se a string é nula, vazia ou contém apenas espaços em branco.
 * Equivalente ao C# string.IsNullOrWhiteSpace
 */
export function isNullOrWhiteSpace(value?: string | null): boolean {
  return value === null || value === undefined || value.trim().length === 0;
}
