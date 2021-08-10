using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using WebG9.Models;

namespace WebG9.Repository
{
    public class BaseRepository<T> where T:BaseModel
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Havan\Documents\GitHub\G9\WebG9\App_Data\Alcool.mdf;Integrated Security = True";
        public virtual void Create(T model)
        {
            ExecNoQuery("INSERT INTO Alcool" +
                        "( Nome,Valor)" +
                        $"VALUES ('{model.Nome}'" +
                        $",{model.Valor.ToString(CultureInfo.InvariantCulture)})"
                );
        }
        public virtual List<T> Read()
        {
            List<T> list = new List<T>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "SELECT Id,Nome, Valor FROM Alcool";
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            T model = new T();
                            model.Id = Convert.ToInt32(dataReader["id"]);
                            model.Nome = dataReader["Nome"].ToString();
                            model.Valor = Convert.ToDecimal(dataReader["Valor"]);
                            list.Add(model);
                        }

                    }
                }
            }
            return list;
        }
        public virtual T ReadByID(int id)
        {
            T model = new T();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "SELECT Id,Nome, Valor FROM Alcool";
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            T model = new T();
                            model.Id = Convert.ToInt32(dataReader["id"]);
                            model.Nome = dataReader["Nome"].ToString();
                            model.Valor = Convert.ToDecimal(dataReader["Valor"]);
                            list.Add(model);
                        }

                    }
                }
            }
            return list;
        }
        public virtual void Update(T model)
        {
            ExecNoQuery("UPDATE Alcool" +
                            "SET" +
                           $"Nome = {model.Nome}" +
                           $",Valor = {model.Valor.ToString(CultureInfo.InvariantCulture)}" +
                           $"WHERE Id = {model.Id}");
        }
        public virtual void Delete(int id)
        {
            ExecNoQuery($"DELETE FROM Alcool WHERE Id ={id}");
        }
         public virtual void ExecNoQuery(string comando)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = comando;
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}