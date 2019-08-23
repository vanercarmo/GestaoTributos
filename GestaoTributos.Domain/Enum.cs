using System.ComponentModel;

namespace GestaoTributos.Domain
{
    public enum EnumTipoParametro
    {
        [Description("QTDE_THREADS_WS_LOTE_CONTABIL")]
        QtdeThreadsLoteContabil,
        [Description("URL_WS_NFS")]
        UrlWsNotaFiscalSaida,
        [Description("URL_WS_LOTCTB")]
        UrlWsLoteContabil,
        [Description("URL_WS_LANCTB")]
        UrlWsLancamentoContabil,
        [Description("URL_WS_FOR")]
        UrlWsFornecedores,
        [Description("QTDE_THREADS_WS_LANCAMENTO_CONTABIL")]
        QtdeThreadsLancamentoContabil,
        [Description("TIMEOUT_WS")]
        TimeoutWS,
        [Description("ENVIAR_LANCAMENTO_CONTABIL_ASSINCRONO")]
        EnviarLancamentoContabilAssincrono
    }

    public enum EnumStatus
    {
        PROCESSAR = 0,
        PROCESSANDO = 1,
        PROCESSADO = 2,
        ERRO = 3
    }
}
