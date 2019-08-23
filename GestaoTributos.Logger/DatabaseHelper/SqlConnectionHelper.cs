//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;

//namespace GestaoTributos.Logger.DatabaseHelper
//{
//    public class SqlConnectionHelper : IDbConnection
//    {
//        private SqlConnection conn;

//        public SqlConnectionHelper(string connectionString)
//        {
//            if (conn == null)
//            {
//                conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);
//                conn.Open();
//            }
//        }

//        public SqlConnection Connection
//        {
//            get
//            {
//                return conn;
//            }
//        }

//        public string ConnectionString
//        {
//            get
//            {
//                return ((IDbConnection)conn).ConnectionString;
//            }

//            set
//            {
//                ((IDbConnection)conn).ConnectionString = value;
//            }
//        }

//        public int ConnectionTimeout
//        {
//            get
//            {
//                return ((IDbConnection)conn).ConnectionTimeout;
//            }
//        }

//        public string Database
//        {
//            get
//            {
//                return ((IDbConnection)conn).Database;
//            }
//        }

//        public ConnectionState State
//        {
//            get
//            {
//                return ((IDbConnection)conn).State;
//            }
//        }

//        public IDbTransaction BeginTransaction()
//        {
//            return ((IDbConnection)conn).BeginTransaction();
//        }

//        public IDbTransaction BeginTransaction(IsolationLevel il)
//        {
//            return ((IDbConnection)conn).BeginTransaction(il);
//        }

//        public void ChangeDatabase(string databaseName)
//        {
//            ((IDbConnection)conn).ChangeDatabase(databaseName);
//        }

//        public void Close()
//        {
//            ((IDbConnection)conn).Close();
//        }

//        public IDbCommand CreateCommand()
//        {
//            return new WrappedDbCommand(((IDbConnection)conn).CreateCommand());
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            conn.Dispose();
//            conn = null;
//        }

//        public void Open()
//        {
//            ((IDbConnection)conn).Open();
//        }

//        public void BulkCopy(DataTable table)
//        {
//            if (table != null)
//            {
//                using (var copy = new SqlBulkCopy(this.Connection))
//                {
//                    copy.DestinationTableName = table.TableName;
//                    copy.WriteToServerAsync(table);
//                }
//            }
//        }
//    }

//    public class WrappedDbCommand : IDbCommand
//    {
//        private readonly IDbCommand _cmd;
//        public WrappedDbCommand(IDbCommand command)
//        {
//            if (command == null)
//                throw new ArgumentNullException("command");

//            _cmd = command;
//        }

//        public string CommandText
//        {
//            get { return _cmd.CommandText; }
//            set { _cmd.CommandText = value; }
//        }

//        public int CommandTimeout
//        {
//            get { return _cmd.CommandTimeout; }
//            set { _cmd.CommandTimeout = value; }
//        }

//        public CommandType CommandType
//        {
//            get { return _cmd.CommandType; }
//            set { _cmd.CommandType = value; }
//        }

//        public IDbConnection Connection
//        {
//            get { return _cmd.Connection; }
//            set { _cmd.Connection = value; }
//        }

//        public IDataParameterCollection Parameters
//        {
//            get { return _cmd.Parameters; }
//        }

//        public IDbTransaction Transaction
//        {
//            get { return _cmd.Transaction; }
//            set { _cmd.Transaction = value; }
//        }

//        public UpdateRowSource UpdatedRowSource
//        {
//            get { return _cmd.UpdatedRowSource; }
//            set { _cmd.UpdatedRowSource = value; }
//        }

//        public void Cancel()
//        {
//            _cmd.Cancel();
//        }

//        public IDbDataParameter CreateParameter()
//        {
//            return _cmd.CreateParameter();
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            _cmd.Dispose();
//        }

//        public int ExecuteNonQuery()
//        {
//            Debug.WriteLine(string.Format("[ExecuteNonQuery] {0}", _cmd.CommandText));
//            return _cmd.ExecuteNonQuery();
//        }

//        public IDataReader ExecuteReader()
//        {
//            Debug.WriteLine(string.Format("[ExecuteReader] {0}", _cmd.CommandText));
//            return _cmd.ExecuteReader();
//        }

//        public IDataReader ExecuteReader(CommandBehavior behavior)
//        {
//            Debug.WriteLine(string.Format("[ExecuteReader({0})] {1}", behavior, _cmd.CommandText));
//            return _cmd.ExecuteReader();
//        }

//        public object ExecuteScalar()
//        {
//            Debug.WriteLine(string.Format("[ExecuteScalar] {0}", _cmd.CommandText));
//            return _cmd.ExecuteScalar();
//        }

//        public void Prepare()
//        {
//            _cmd.Prepare();
//        }
//    }
//}
