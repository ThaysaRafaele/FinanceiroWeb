using System.ComponentModel.DataAnnotations; 

namespace FinanceiroWeb.Models
{
    public class NotaFiscal
    {
        [Key] 
        public int NotaId { get; set; }
        public string NomePagador { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataCobranca { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public string DocumentoNota { get; set; }
        public string DocumentoBoleto { get; set; }
        public string Status { get; set; }
    }
}
