using GestaoTributos.Domain;
using Dapper;
using System.Data;
using System;

namespace GestaoTributos.Data.Repositories
{
    public class ParametroRepository : Repository, IDisposable
    {
        public Parametro BuscarParametro(EnumTipoParametro parametro)
        {
            lock (this.ObjectLock)
            {
                return this.Connection.QuerySingleOrDefault<Parametro>(
                    Queries.BUSCAR_PARAMETRO,
                    new
                    {
                        CHAVE = parametro.GetDescription()
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
