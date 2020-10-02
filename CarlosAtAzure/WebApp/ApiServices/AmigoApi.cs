using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.Amigo;

namespace WebApp.ApiServices
{
    public class AmigoApi : IAmigoApi
    {
        private readonly HttpClient _httpClient;

        public AmigoApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.BaseAddress = new Uri("http://localhost:53942/");
        }


        public async Task<CriarAmigoViewModel> PostAsync(CriarAmigoViewModel criarAmigoViewModel)
        {
            var criarAmigoViewModelJson = JsonConvert.SerializeObject(criarAmigoViewModel);
            //para fazer requisição Http das APIs

            var conteudo = new StringContent(criarAmigoViewModelJson, Encoding.UTF8, "application/Json");
            var response = await _httpClient.PostAsync("api/amigos", conteudo);
            if (response.IsSuccessStatusCode)
            {
                return criarAmigoViewModel;
            }
            else if(response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var respostaContent = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<List<string>>(respostaContent);
                criarAmigoViewModel.Erros  = erros;
            }
            return criarAmigoViewModel;
        }
        public async Task<List<ListarAmigoViewModel>> GetAsync()
        {
            var response = await _httpClient.GetAsync("api/amigos");
            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarAmigoViewModel>>(responseContent);
            return list;
        }

        public async Task<ListarAmigoViewModel> GetAsync(string id)
        {
            var response = await _httpClient.GetAsync("api/amigos/" + id);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ListarAmigoViewModel>(responseContent);
            
        }

        public async Task<DetailsAmigoViewModel> GetDetailsAmigoAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var amigo = JsonConvert.DeserializeObject<DetailsAmigoViewModel>(responseContent);

            return amigo;
        }

        public async Task<EditarAmigoViewModel> PutAsync(Guid id, EditarAmigoViewModel editarAmigoViewModel)
        {
            var editarAmigoViewModelJson = JsonConvert.SerializeObject(editarAmigoViewModel);

            var conteudo = new StringContent(editarAmigoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/amigos/" + id, conteudo);

            if (response.IsSuccessStatusCode)
            {
                return editarAmigoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                editarAmigoViewModel.Errors = listErro;

                return editarAmigoViewModel;
            }

            return editarAmigoViewModel;
        }

        public async Task<ListarAmigoViewModel> GetAmigoByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            var amigo = JsonConvert.DeserializeObject<ListarAmigoViewModel>(responseContent);

            return amigo;
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync("/api/amigos/" + id);

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }

        public async Task<CriarAmigosDoAmigoViewModels> PostAmigosDoAmigoAsync(Guid id, CriarAmigosDoAmigoViewModels criarAmigosDoAmigoViewModel)
        {
            var criarAmigosDoAmigoViewModelJson = JsonConvert.SerializeObject(criarAmigosDoAmigoViewModel);

            var conteudo = new StringContent(criarAmigosDoAmigoViewModelJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://localhost:44334/api/amigos/{id}/amigosdoamigos", conteudo);

            if (response.IsSuccessStatusCode)
            {
                return criarAmigosDoAmigoViewModel;
            }

            else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listErro = JsonConvert.DeserializeObject<List<string>>(responseContent);

                criarAmigosDoAmigoViewModel.Errors = listErro;

                return criarAmigosDoAmigoViewModel;
            }

            return criarAmigosDoAmigoViewModel;
        }

        public async Task<List<ListarAmigosDoAmigoViewModel>> GetAmigosDoAmigoAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44334/api/amigos/{id}/amigosdoamigos");

            var responseContent = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<ListarAmigosDoAmigoViewModel>>(responseContent);

            return list;
        }

        public async Task<ListarAmigosDoAmigoViewModel> GetAmigosDoAmigoByIdAsync(Guid amigosDoAmigosid)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44334/api/amigos/deletaramigosDoAmigos/{amigosDoAmigosid}");

            var responseContent = await response.Content.ReadAsStringAsync();

            var amizade = JsonConvert.DeserializeObject<ListarAmigosDoAmigoViewModel>(responseContent);

            return amizade;
        }
 
        public async Task<string> DeleteAmigosDoAmigoAsync(Guid amigosDoAmigosid)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:44334/api/amigos/deletaramigosDoAmigos/{amigosDoAmigosid}");

            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
