using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GestaoTributos.Logger
{
    public class DatabaseLogger : ILogger
    {
        private static SqlConnection CreateConnection()
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRKGE"].ConnectionString);

            if (_conn?.State != System.Data.ConnectionState.Open)
                _conn.Open();

            return _conn;
        }

        public void End()
        {
            using (var conn = DatabaseLogger.CreateConnection())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into log_thread (mensagem) values (@mensagem)";
                    command.Parameters.Add(new SqlParameter("mensagem", "Fim do processo"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Start()
        {
            using (var conn = DatabaseLogger.CreateConnection())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into log_thread (mensagem) values (@mensagem)";
                    command.Parameters.Add(new SqlParameter("mensagem", "Inicio do processo"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void WriteLog(string message)
        {
            using (var conn = DatabaseLogger.CreateConnection())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into log_thread (mensagem) values (@mensagem)";
                    command.Parameters.Add(new SqlParameter("mensagem", message));
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void WriteLog(Guid guid, string message)
        {
            using (var conn = DatabaseLogger.CreateConnection())
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into log_thread (guid, mensagem) values (@guid, @mensagem)";
                    command.Parameters.Add(new SqlParameter("guid", guid.ToString()));
                    command.Parameters.Add(new SqlParameter("mensagem", message));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
