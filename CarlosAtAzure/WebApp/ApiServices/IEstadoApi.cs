using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Estado;

namespace WebApp.ApiServices
{
    public interface IEstadoApi
    {
        Task<List<ListarEstadoViewModel>> GetAsync();
        Task<DetailsEstadoViewModel> GetEstadoAsync(Guid id);
        Task<ListarEstadoViewModel> GetEstadoByIdAsync(Guid id);
        Task<CriarEstadoViewModel> PostAsync(CriarEstadoViewModel criarEstadoViewModel);
        Task<EditarEstadoViewModel> PutAsync(Guid id, EditarEstadoViewModel editarEstadoViewModel);
        Task<string> DeleteAsync(Guid id);
    }
}
