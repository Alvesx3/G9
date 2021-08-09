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
        //private static List<T> list = new List<T>();
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
            return list;
        }
        public virtual T Read(int id)
        {
            return list.Find(v => v.Id == id);
        }
        public void Update(T model)
        {
           int index = list.FindIndex(v => v.Id == model.Id);
            if (index != -1)
            {
                list[index] = model;
            }
        }
        public void Delete(int id)
        {
            T model = Read(id);
            if (model != null)
            {
                list.Remove(model);
            }
        }
        void ExecNoQuery(string comando)
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