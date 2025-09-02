namespace Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

public class Deficiencia
{
    public int DeficienciaId { get; set; }
    public bool TemDeficienciaFisica { get; set; }
    public bool TemDeficienciaVisual { get; set; }
    public bool TemDeficienciaAuditiva { get; set; }
    public bool TemDeficienciaMental { get; set; }
    public bool TemDeficienciaIntelectual { get; set; }
}
