using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProjPendencia.API;
using ProjPendencia.API.Model;
using Xunit;

namespace ProjPendencia.Test
{
    public class PendenciaIntegrateTest
    {
        public HttpClient _client { get; }

        public PendenciaIntegrateTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async void GetAllPendencias()
        {
            var response = await _client.GetAsync("/api/Pendencia");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCountPendencias()
        {
            var response = await _client.GetAsync("/api/Pendencia");
            var pendencias = JsonConvert.DeserializeObject<Pendencia[]>(await response.Content.ReadAsStringAsync());
            Assert.True(pendencias.Length >= 1);
        }

        [Fact]
        public async void PostPendencia()
        {

            Pendencia pendencia = new Pendencia()
            {
                Descricao = "Descrição 3",
                Data = "06/04/2021"
            };

            var jsonContent = JsonConvert.SerializeObject(pendencia);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Pendencia", contentString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
