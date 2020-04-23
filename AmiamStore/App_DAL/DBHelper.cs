using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
namespace AmiamStore.App_DAL
{
    public class DBHelper
    {
        private readonly string _connectionString;

        public DBHelper(string dataBaseName= "AmiamGlovesStore.accdb")
        {
            string connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            connString += HttpContext.Current.Server.MapPath($"~/App_Data/{dataBaseName}");
            _connectionString = connString;
        }

        public void ExecuteNonQuery(string query)
        {
            OleDbConnection connection = null;
            OleDbCommand command = null;
            try
            {
                connection = new OleDbConnection(_connectionString);
                command = new OleDbCommand
                {
                    CommandText = query,
                    Connection = connection
                };
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }

            }
        }

        public DataTable GetData(string sql)
        {
            OleDbConnection connection=null;
            OleDbCommand command=null;
 
            OleDbDataReader dataReader;

            var dataTable = new DataTable();
            try
            {
                connection = new OleDbConnection(_connectionString);
                command = new OleDbCommand();
                command.CommandText = sql;
                command.Connection = connection;
                connection.Open();
                dataReader = command.ExecuteReader();

                dataTable.Load(dataReader);              
            }
            catch (Exception e)
            {
                string exception = e.Message;
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }

            }

            return dataTable;
        }
    }
    }

