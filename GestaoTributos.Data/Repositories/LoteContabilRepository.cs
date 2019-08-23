using Dapper;
using GestaoTributos.Domain;
using System;
using System.Data;
using GestaoTributos.Logger;

namespace GestaoTributos.Data.Repositories
{
    public class LoteContabilRepository : Repository, IDisposable
    {
        public int? Empresa { get; set; }

        public LoteContabilRepository() { }

        public LoteContabilRepository(ILogger logger)
        {
            this._logger = logger;
        }      

        public LoteContabil BuscarLoteContabil()
        {
            return this.Connection.QuerySingleOrDefault<LoteContabil>(
                Queries.BUSCAR_LOTE_CONTABIL,
                new { empresa = this.Empresa },
                commandType: CommandType.StoredProcedure);
        }

        public void InserirNumeroLote(LoteContabil lote, string erroExecucao, int? numLot)
        {
            if (lote != null)
            {
                this.Connection.Execute(
                    Queries.INSERIR_NUMERO_LOTE, new
                    {
                        erroExecucao = erroExecucao,
                        numLot = numLot?.ToString(),
                        chave = lote.CHAVE.ToString(),
                        empresa = lote.CODEMP,
                        filial = lote.CODFIL,
                        dataLote = lote.DATLOT,
                        numeroLoteCRK = lote.NNUMLOTCRK
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Connection.Close();
            this.Connection.Dispose();
        }
    }
}
