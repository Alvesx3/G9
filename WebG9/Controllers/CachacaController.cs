using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebG9.Models;
using WebG9.Repository;

namespace WebG9.Controllers
{
    public class CachacaController : BaseController<Cachaca, CachacaRepository>
    {
        public CachacaController(): base(new CachacaRepository())
        {

        }
    }
}