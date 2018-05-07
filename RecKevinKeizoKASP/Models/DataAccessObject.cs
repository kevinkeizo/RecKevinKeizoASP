using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecKevinKeizoKASP.Models
{/// <summary>
/// Chamada do Banco Modelo para Tratar (DAO)
/// </summary>
    public class DataAccessObject:DbContext
    {
        public DataAccessObject() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<RecKevinKeizoKASP.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<RecKevinKeizoKASP.Models.News> News { get; set; }

    }
}