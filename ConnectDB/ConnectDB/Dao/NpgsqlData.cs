using ConnectDB.Models;
using ConnectDB.Repository;
using Dapper;
using log4net;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

namespace ConnectDB.DDL
{
    public class NpgsqlData<T> : INpgsqlDataSource, INpgsqlTransaction, IDatabaseDao<T>
    {
        private const int defaultPorts = 5432;
        private const string server = "localhost";
        private const string username = "postgres";
        private const string password = "1234";
        private const string databaseName = "Users";


        private string connectUrl = "Server=" + server + ";Port=" + defaultPorts + ";Database=" + databaseName + ";Username=" + username + ";Password=" + password;
        private NpgsqlConnection connection = null;

        public NpgsqlData()
        {
            connectionPostgres();
        }


        public NpgsqlTransaction benginTransaction()
        {
            var transaction = connection != null ? connection.BeginTransaction() : null;
            return transaction;
        }

        public void close()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void commit()
        {
            NpgsqlTransaction transaction = benginTransaction();
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public NpgsqlConnection connectionPostgres()
        {
            try
            {
                connection = new NpgsqlConnection(connectUrl);

                Console.WriteLine("connect");
                connection.Open();
                return connection;

            }
            catch (NpgsqlException e)
            {

                Console.WriteLine("Eror" + e);

            }
            return connection;

        }

        public long insertOrUpdate(string sql, params object[] values)
        {
            NpgsqlCommand command = null;

            try
            {
                connection = connectionPostgres();
                command = connection.CreateCommand();
                command.CommandText = sql;
                setParam(command, values);
                int result = command.ExecuteNonQuery();
                if (result >= 0)
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                rollback();
                Console.WriteLine(e);
            }
            finally
            {
                close();
            }
            return -1;
        }

        private void setParam(NpgsqlCommand command, params object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                object parameters = values[i];
                Console.WriteLine(values);
                int index = i + 1;
                if (parameters is long)
                {
                    command.Parameters.AddWithValue("@" + index, parameters);

                }
                else if (parameters is string)
                {
                    command.Parameters.AddWithValue("@" + index, parameters);

                }
                else if (parameters is int)
                {
                    command.Parameters.AddWithValue("@" + index, parameters);

                }
                else if (parameters is bool)
                {
                    command.Parameters.AddWithValue("@" + index, parameters);

                }

            }
        }

        public List<T> query(string sql)
        {
            List<T> results = new List<T>();
            try
            {
                connection = connectionPostgres();

                var objects = connection.Query<T>(sql);

                foreach (var item in objects)
                {
                    results.Add(item);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                close();
            }

            return results;

        }

        public void rollback()
        {
            NpgsqlTransaction transaction = benginTransaction();
            transaction.Rollback();
        }




    }
}
