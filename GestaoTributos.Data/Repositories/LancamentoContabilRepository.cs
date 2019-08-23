using Dapper;
using System.Data;
using GestaoTributos.Domain;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using GestaoTributos.Data.wsLancamentoContabil;
using System.Linq;
using GestaoTributos.Logger;
using System.Data.SqlClient;
using System.Configuration;

namespace GestaoTributos.Data.Repositories
{
    public class LancamentoContabilRepository : Repository, IDisposable
    {
        public int? Empresa { get; set; }

        public LancamentoContabilRepository() { }

        public LancamentoContabilRepository(ILogger logger)
        {
            this._logger = logger;
        }

        public IEnumerable<LancamentoContabil> BuscarLancamentoContabil()
        {
            return this.Connection.Query<LancamentoContabil>(
                Queries.BUSCAR_LANCAMENTO_CONTABIL,
                new { empresa = this.Empresa },
                commandType: CommandType.StoredProcedure);
        }

        public async Task GravarLogExecucaoAsync(importacaolctctbImportar3Out retorno)
        {
            if (retorno != null && retorno.gridLctResult != null && retorno.gridLctResult.Length > 0)
            {
                var _datatable = new DataTable("WS_LOG_LANCAMENTO_CONTABIL");
                _datatable.Columns.Add("ERROEXECUCAO", typeof(string));
                _datatable.Columns.Add("NUMEROLOTE", typeof(int));
                _datatable.Columns.Add("NUMEROLANCAMENTO", typeof(double));
                _datatable.Columns.Add("LINHA", typeof(int));
                _datatable.Columns.Add("RESULTADO", typeof(string));

                foreach (var r in retorno.gridLctResult)
                {
                    _datatable.Rows.Add(retorno.erroExecucao, r.numLot, r.numLct, r.linha, r.resultado);
                }

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRKGE"].ConnectionString))
                {
                    conn.Open();

                    using (var copy = new SqlBulkCopy(conn))
                    {
                        copy.DestinationTableName = _datatable.TableName;
                        await copy.WriteToServerAsync(_datatable);
                    }
                }
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
