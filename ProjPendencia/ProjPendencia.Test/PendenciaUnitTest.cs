using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjPendencia.API.Controllers;
using ProjPendencia.API.Data;
using ProjPendencia.API.Model;
using Xunit;

namespace ProjPendencia.Test
{
    public class PendenciaUnitTest
    {
        private DbContextOptions<PendenciaContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<PendenciaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new PendenciaContext(options))
            {
                context.Pendencia.Add(new Pendencia { Id = 1, Descricao = "Descrição 1",  Data = "04/04/2021" });
                context.Pendencia.Add(new Pendencia { Id = 2, Descricao = "Descrição 2",  Data = "05/04/2021" });
                context.Pendencia.Add(new Pendencia { Id = 3, Descricao = "Descrição 3",  Data = "06/04/2021" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                IEnumerable<Pendencia> pendencias = pendenciaController.GetPendencia().Result.Value;

                Assert.Equal(3, pendencias.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                int pendenciaId = 2;
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.GetPendencia(pendenciaId).Result.Value;
                Assert.Equal(2, pend.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 4,
                Descricao = "Descrição 4",
                Data = "07/04/2021"
            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PostPendencia(pendencia).Result.Value;
                Assert.Equal(4, pend.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 3,
                Descricao = "Descrição 3",
                Data = "06/04/2021"
            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PutPendencia(3, pendencia).Result.Value;
                Assert.Equal("Descrição 3", pend.Descricao);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pendencia = pendenciaController.DeletePendencia(2).Result.Value;
                Assert.Null(pendencia);
            }
        }
    }
}
