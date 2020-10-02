using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Estado;

namespace WebApp.ApiServices
{
    public class EstadoApi : IEstadoApi
    {
        private readonly HttpClient _httpClient;

        public EstadoApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri("https://localhost:44390/");
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync("/api/estados/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        public async Task<List<ListarEstadoViewModel>> GetAsync()
        {
            var response = await _httpClient.GetAsync("/api/estados");

            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarEstadoViewModel>>(responseContent);

            return list;
        }

        public async Task<DetailsEstadoViewModel> GetEstadoAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/estados/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var estado = JsonConvert.DeserializeObject<DetailsEstadoViewModel>(responseContent);

            return estado;
        }

        public async Task<ListarEstadoViewModel> GetEstadoByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/estados/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var estado = JsonConvert.DeserializeObject<ListarEstadoViewModel>(responseContent);

            return estado;
        }

        public async Task<CriarEstadoViewModel> PostAsync(CriarEstadoViewModel criarEstadoViewModel)
        {
            var criarEstadoViewModelJson = JsonConvert.SerializeObject(criarEstadoViewModel);

            var conteudo = new StringContent(criarEstadoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/estados", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return criarEstadoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                criarEstadoViewModel.Errors = listErro;

                return criarEstadoViewModel;
            }

            return criarEstadoViewModel;
        }

        public async Task<EditarEstadoViewModel> PutAsync(Guid id, EditarEstadoViewModel editarEstadoViewModel)
        {
            var editarEstadoViewModelJson = JsonConvert.SerializeObject(editarEstadoViewModel);

            var conteudo = new StringContent(editarEstadoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/estados/" + id, conteudo);

            if (response.IsSuccessStatusCode)
            {
                return editarEstadoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                editarEstadoViewModel.Errors = listErro;

                return editarEstadoViewModel;
            }

            return editarEstadoViewModel;
        }
    }
}
