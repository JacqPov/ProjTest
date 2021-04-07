using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjPendencia.API.Model;

namespace ProjPendencia.API.Data
{
    public class PendenciaContext : DbContext
    {
        public PendenciaContext(DbContextOptions<PendenciaContext> options)
           : base(options)
        {
        }

        public DbSet<Pendencia> Pendencia { get; set; }
    }
}
