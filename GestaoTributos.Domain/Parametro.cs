using System;

namespace GestaoTributos.Domain
{
    public class Parametro
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public string ValorTexto { get; set; }
        public int? ValorInt { get; set; }
        public DateTime? ValorData { get; set; }
        public bool? ValorBooleano { get; set; }
        public string Descricao { get; set; }
    }
}
