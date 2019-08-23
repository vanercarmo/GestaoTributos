namespace GestaoTributos.Data
{
    public class Queries
    {
        public const string BUSCAR_LOTE_CONTABIL = "[dbo].[SP_BUSCAR_LOTE_CONTABIL]";
        public const string INSERIR_NUMERO_LOTE = "[dbo].[SP_INSERIR_WS_NUMEROLOTE]";
        public const string INFORMAR_LOTE_CONTABIL_PROCESSADO = "[dbo].[SP_INFORMAR_LOTE_CONTABIL_PROCESSADO]";
        public const string ALTERAR_STATUS_LANCAMENTO_CONTABIL = "[dbo].[SP_ALTERAR_STATUS_LANCAMENTO_CONTABIL]";
        public const string BUSCAR_LANCAMENTO_CONTABIL = "[dbo].[SP_BUSCAR_LANCAMENTOS_CONTABEIS]";
        public const string INSERIR_LOG_LANCAMENTO_CONTABIL = "[dbo].[SP_INSERIR_LOG_LANCAMENTO_CONTABIL]";
        public const string BUSCAR_PARAMETRO = "[dbo].[SP_BUSCAR_PARAMETRO]";
    }
}
