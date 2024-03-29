﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using WebG9.Models;

namespace WebG9.Repository
{
    public class CachacaRepository : BaseRepository<Cachaca>
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Havan\Documents\GitHub\G9\WebG9\App_Data\Alcool.mdf;Integrated Security = True";
        public override void Create(Cachaca model)
        {
            ExecNonQuery("INSERT INTO CACHACA" +
                        "( Nome,Valor)" +
                        $"VALUES ('{model.Nome}'" +
                        $",{model.Valor.ToString(CultureInfo.InvariantCulture)})"
                );
            base.Create(model);
        }
        public override List<Cachaca> Read()
        {
            List<Cachaca> list = new List<Cachaca>();
            
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
                            Cachaca model = new Cachaca();
                            model.Id = Convert.ToInt32(dataReader["id"]);
                            model.Nome = dataReader["Nome"].ToString();
                            model.Valor = Convert.ToDecimal(dataReader["Valor"]);
                            list.Add(model);
                        }

                    }
                }
            }
            //return base.Read(id);
            return list;
        }
        public override Cachaca ReadByID(int id)
        {
            Cachaca model = new Cachaca();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                // Comando SQL de Insert
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = $"SELECT Id, Nome, Valor FROM CACHACA WHERE Id={id}";
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            model.Id = Convert.ToInt32(dataReader["Id"]);
                            model.Nome = dataReader["Nome"].ToString();
                            model.Valor = Convert.ToDecimal(dataReader["Valor"]);
                        }
                    }
                }
            }
            return model;
            //return base.ReadByID(id);
        }
        public override void Update(Cachaca model)
        {
            ExecNonQuery("UPDATE CACHACA" +
                           "SET" +
                          $"Nome= '{model.Nome}'" +
                          $",Valor= '{model.Valor.ToString(CultureInfo.InvariantCulture)}'" +
                          $"WHERE Id = {model.Id}");
            base.Update(model);
        }
    }
}