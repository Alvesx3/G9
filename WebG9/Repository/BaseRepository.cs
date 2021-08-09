using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebG9.Models;

namespace WebG9.Repository
{
    public class BaseRepository<T> where T:BaseModel
    {
        private static List<T> list = new List<T>();
        static int id = 1;
        public virtual void Create(T model)
        {
            model.Id = id;
            list.Add(model);
            id++;
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

    }
}