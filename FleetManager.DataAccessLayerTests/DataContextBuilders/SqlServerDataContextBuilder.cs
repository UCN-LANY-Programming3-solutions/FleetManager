﻿using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.IO;

namespace FleetManager.DataAccessLayer.Tests.DataContextBuilders
{
    class SqlServerDataContextBuilder : DataContextBuilder
    {
        private readonly string _connectionString;

        public SqlServerDataContextBuilder(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override DataContextBuilder Feed<TEntity>(string pathToTestData)
        {
            string json = File.ReadAllText(pathToTestData);
            var obj = JsonConvert.DeserializeObject<dynamic>(json);

            string type = obj.type;
            string[] properties = obj.properties.ToObject<string[]>();
            var data = obj.data;

            string columns = string.Join(", ", properties);
            string parameters = "";
            Array.ForEach(properties, p => { parameters += $"@{p.ToLower()}, "; });
            parameters = parameters.Substring(0, parameters.Length - 2);

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string identityInsertOffSql = $"SET IDENTITY_INSERT {type} ON";
            SqlCommand setupCommand = new(identityInsertOffSql, connection);
            setupCommand.ExecuteNonQuery();


            string insertSql = $"INSERT INTO {type} ({columns}) VALUES ({parameters});";
            foreach (var row in data)
            {
                SqlCommand command = new(insertSql, connection);
                foreach (var item in row)
                {
                    command.Parameters.AddWithValue(item.Name, item.Value.Value ?? DBNull.Value);
                }
                command.ExecuteNonQuery();
            }

            return this;
        }

        public override DataContextBuilder Initialize<TDataContext>(Func<TDataContext> factory)
        {
            _context = factory();
            Database.Version.Upgrade(_connectionString);
            return this;
        }

        //public override DataContextBuilder<SqlServerDataContext> Initialize()
        //{
        //    
        //    
        //    return this;
        //}

        //public override DataContextBuilder<SqlServerDataContext> Feed(string pathToTestData)
        //{
        //    string json = File.ReadAllText(pathToTestData);
        //    var obj = JsonConvert.DeserializeObject<dynamic>(json);

        //    string type = obj.type;
        //    string[] properties = obj.properties.ToObject<string[]>();
        //    var data = obj.data;

        //    string columns = string.Join(", ", properties);
        //    string parameters = "";
        //    Array.ForEach(properties, p => { parameters += $"@{p.ToLower()}, "; });
        //    parameters = parameters.Substring(0, parameters.Length - 2);

        //    using SqlConnection connection = new(_connectionString);
        //    connection.Open();

        //    string identityInsertOffSql = $"SET IDENTITY_INSERT {type} ON";
        //    SqlCommand setupCommand = new(identityInsertOffSql, connection);
        //    setupCommand.ExecuteNonQuery();


        //    string insertSql = $"INSERT INTO {type} ({columns}) VALUES ({parameters});";
        //    foreach (var row in data)
        //    {
        //        SqlCommand command = new(insertSql, connection);
        //        foreach (var item in row)
        //        {
        //            command.Parameters.AddWithValue(item.Name, item.Value.Value ?? DBNull.Value);
        //        }
        //        command.ExecuteNonQuery();
        //    }

        //    return this;
        //}
    }
}