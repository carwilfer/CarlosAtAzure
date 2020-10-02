using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Pais;

namespace WebApp.ApiServices
{
    public class PaisApi : IPaisApi
    {
        private readonly HttpClient _httpClient;

        public PaisApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri("http://localhost:53928/");
        }

        public async Task<CriarPaisViewModel> PostAsync(CriarPaisViewModel criarPaisViewModel)
        {
            var criarPaisViewModelJson = JsonConvert.SerializeObject(criarPaisViewModel);

            var conteudo = new StringContent(criarPaisViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/pais", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return criarPaisViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                criarPaisViewModel.Errors = listErro;

                return criarPaisViewModel;
            }

            return criarPaisViewModel;
        }

        public async Task<EditarPaisViewModel> PutAsync(Guid id, EditarPaisViewModel editarPaisViewModel)
        {
            var editarPaisViewModelJson = JsonConvert.SerializeObject(editarPaisViewModel);

            var conteudo = new StringContent(editarPaisViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/pais/" + id, conteudo);

            if (response.IsSuccessStatusCode)
            {
                return editarPaisViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                editarPaisViewModel.Errors = listErro;

                return editarPaisViewModel;
            }

            return editarPaisViewModel;
        }

        public async Task<List<ListarPaisViewModel>> GetAsync()
        {
            var response = await _httpClient.GetAsync("/api/pais");

            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarPaisViewModel>>(responseContent);

            return list;
        }

        public async Task<DetailsPaisViewModel> GetPaisAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/pais/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var pais = JsonConvert.DeserializeObject<DetailsPaisViewModel>(responseContent);

            return pais;
        }

        public async Task<ListarPaisViewModel> GetPaisByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/pais/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var pais = JsonConvert.DeserializeObject<ListarPaisViewModel>(responseContent);

            return pais;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync("/api/pais/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
