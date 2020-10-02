using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Amigo;

namespace WebApp.ApiServices
{
    public interface IAmigoApi
    {
        Task<CriarAmigoViewModel> PostAsync(CriarAmigoViewModel criarAmigoViewModel);
        Task<CriarAmigosDoAmigoViewModels> PostAmigosDoAmigoAsync(Guid id, CriarAmigosDoAmigoViewModels criarAmigosDoAmigoViewModel);
        Task<List<ListarAmigoViewModel>> GetAsync();
        Task<ListarAmigoViewModel>GetAsync(string id);
        Task<List<ListarAmigosDoAmigoViewModel>> GetAmigosDoAmigoAsync(Guid id);
        Task<ListarAmigosDoAmigoViewModel> GetAmigosDoAmigoByIdAsync(Guid camaradaId);
        Task<DetailsAmigoViewModel> GetDetailsAmigoAsync(Guid id);
        Task<EditarAmigoViewModel> PutAsync(Guid id, EditarAmigoViewModel editarAmigoViewModel);
        Task<ListarAmigoViewModel> GetAmigoByIdAsync(Guid id);
        Task<string> DeleteAsync(Guid id);
        Task<string> DeleteAmigosDoAmigoAsync(Guid camaradaId);
    }
}
