using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Pais;

namespace WebApp.ApiServices
{
    public interface IPaisApi
    {
        Task<List<ListarPaisViewModel>> GetAsync();
        Task<DetailsPaisViewModel> GetPaisAsync(Guid id);
        Task<ListarPaisViewModel> GetPaisByIdAsync(Guid id);
        Task<CriarPaisViewModel> PostAsync(CriarPaisViewModel criarPaisViewModel);
        Task<EditarPaisViewModel> PutAsync(Guid id, EditarPaisViewModel editarPaisViewModel);
        Task<string> DeleteAsync(Guid id);
    }
}
