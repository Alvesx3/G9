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
        T model = null;
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Havan\Documents\GitHub\G9\WebG9\App_Data\Alcool.mdf;Integrated Security = True";
        public virtual void Create(T model)
        {
            ExecNonQuery("INSERT INTO Alcool" +
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
                    command.CommandText = "SELECT Id,Nome, Valor FROM CACHACA";
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            //T model = new T();
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
            
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                // Comando SQL de Insert
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"SELECT Id, Nome, Salario, Setor FROM Profissao WHERE Id={id}";
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            model.Id = Convert.ToInt32(dataReader["Id"]);
                            model.Nome = dataReader["Nome"].ToString();
                            model.Valor = Convert.ToDecimal(dataReader["Salario"]);
                        }
                    }
                }
            }
            return model;
        }
        public virtual void Update(T model)
        {
            ExecNonQuery("UPDATE Alcool" +
                            "SET" +
                           $"Nome = {model.Nome}" +
                           $",Valor = {model.Valor.ToString(CultureInfo.InvariantCulture)}" +
                           $"WHERE Id = {model.Id}");
        }
        public virtual void Delete(int id)
        {
            ExecNonQuery($"DELETE FROM Alcool WHERE Id ={id}");
        }
         public virtual void ExecNonQuery(string comando)
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