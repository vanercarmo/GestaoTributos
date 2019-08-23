using GestaoTributos.Logger;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GestaoTributos.Data.Repositories
{
    public abstract class Repository
    {
        internal Object ObjectLock = new Object();

        private SqlConnection _conn;
        internal SqlConnection Connection
        {
            get
            {
                if (this._conn == null)
                    this._conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRKGE"].ConnectionString);

                if (this._conn.State != System.Data.ConnectionState.Open)
                    this._conn.Open();

                return this._conn;
            }
        }

        internal ILogger _logger;
        internal ILogger Logger
        {
            get
            {
                if (this._logger == null)
                    return this._logger = new ConsoleLogger();
                return this._logger;
            }
        }

        private ParametroRepository _ParametroRepository;
        internal ParametroRepository ParametroRepository
        {
            get
            {
                if (this._ParametroRepository == null)
                    this._ParametroRepository = new ParametroRepository();
                return this._ParametroRepository;
            }
        }
    }
}
