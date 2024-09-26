using System.ComponentModel.DataAnnotations;

namespace FinanceiroWeb.Models
{
    public enum StatusNotaFiscal
    {
        [Display(Name = "Emitida")]
        Emitida = 1,

        [Display(Name = "Cobrança realizada")]
        CobrancaRealizada = 2,

        [Display(Name = "Pagamento em atraso")]
        PagamentoEmAtraso = 3,

        [Display(Name = "Pagamento realizado")]
        PagamentoRealizado = 4
    }


    public class NotaFiscal
    {
        public int NotaId { get; set; }

        [Required]
        public string NomePagador { get; set; }

        [Required]
        public string NumeroNota { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataCobranca { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataPagamento { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Valor { get; set; }

        public string DocumentoNota { get; set; }
        public string DocumentoBoleto { get; set; }

        [Required]
        public StatusNotaFiscal Status { get; set; }
    }
}
