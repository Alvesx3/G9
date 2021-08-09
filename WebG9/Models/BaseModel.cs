using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebG9.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}