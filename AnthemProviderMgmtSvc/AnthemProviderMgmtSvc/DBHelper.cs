using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Data.Common;
using System.Data.OleDb;

namespace AnthemProviderMgmtSvc
{
    public class DBHelper : IDisposable
    {
        #region PRIVATE MEMBER VARIABLES


        private static DbConnection _connection;
        private static bool _dispose;
        private static DbProviderFactory _provider;

        #endregion

        public DBHelper()
        {
            _connection = CreateConnection();
        }

        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        private static string ProviderName
        {
            get { return ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName; }
        }

        private static DbConnection CreateConnection()
        {
            // Assume failure.
            DbConnection connection = null;

            // Create the DbProviderFactory and DbConnection.
            if (ConnectionString != null)
            {
                try
                {
                    _provider = DbProviderFactories.GetFactory(ProviderName);

                    connection = _provider.CreateConnection();
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                }
                catch (Exception ex)
                {
                    // Set the connection to null if it was created.
                    if (connection != null)
                    {
                        connection = null;
                    }
                }
            }
            else
            {
                throw new NullReferenceException(
                    "Invalid or missing connection string . Check if it exists in configuration file.");
            }

            // Return the connection.
            return connection;
        }


        private static void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _provider = null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            CloseConnection();
            Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string commandText, CommandType commandType, object parameters)
        {
            object obj = null;

            // Check for valid DbConnection.
            if (_connection != null)
            {
                using (_connection)
                {
                    try
                    {
                        // Create the command.
                        DbCommand command = _connection.CreateCommand();
                        command.CommandText = commandText;
                        command.CommandType = commandType;
                        if (parameters != null)
                            command.Parameters.Add(parameters);
                        // Open the connection.
                        _connection.Open();
                        obj = command.ExecuteScalar();
                        command.Parameters.Clear();
                    }

                    catch (DbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return obj;
        }

        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string commandText, CommandType commandType, object[] parameters)
        {
            int rowsAffected = 0;

            try
            {
                // Create the command.
                DbCommand command = _connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                rowsAffected = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }

            catch (DbException ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
        #endregion

        #region GetDataSet
        public DataSet ExecuteDataSet(string commandText, List<object> paramCollection, CommandType commandType)
        {
            DataSet dataSet = new DataSet();

            // Check for valid DbConnection.
            if (_connection != null)
            {
                using (_connection)
                {
                    try
                    {
                        // Create the command.
                        DbCommand command = _connection.CreateCommand();
                        command.CommandText = commandText;
                        command.CommandType = commandType;
                        if (paramCollection != null && paramCollection.Count > 0)
                        {
                            foreach (IDataParameter param in paramCollection)
                                command.Parameters.Add(param);
                        }
                        // Open the connection.
                        //_connection.Open();
                        var factory = DbProviderFactories.GetFactory(ProviderName);
                        if (factory != null)
                        {
                            DbDataAdapter da = factory.CreateDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(dataSet);
                        }
                    }

                    catch (DbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return dataSet;
        }
        #endregion


        private static void Dispose(bool disposing)
        {
            if (_dispose) return;
            if (disposing)
            {
                if (_connection != null)
                {
                    _provider = null;
                    _connection.Dispose();
                }
            }

            _dispose = true;
        }

        
    }
}
        