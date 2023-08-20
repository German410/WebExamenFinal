using Microsoft.AspNetCore.Mvc;
using WebExamenFinal.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net;
using System.Security.Principal;

namespace WebExamenFinal.Controllers
{
    public class DenunciaController : Controller
    {
        public async Task<IActionResult> Index()
        {

            List<DenunciasModel> denunciasList = new List<DenunciasModel>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7297/api/Denuncias/");
                HttpResponseMessage response = await httpClient.GetAsync("GetDenuncias");
                string apirResponse = await response.Content.ReadAsStringAsync();
                denunciasList = JsonConvert.DeserializeObject<List<DenunciasModel>>(apirResponse).Select(
                s => new DenunciasModel
                {
                        idDenuncia = s.idDenuncia,
                        dni = s.dni,
                        Nombre = s.Nombre,
                        Apellidos = s.Apellidos,
                        Empresa = s.Empresa,
                        Ciudad = s.Ciudad,
                        Telefono = s.Telefono,
                        Denuncia = s.Denuncia
                    }  
                    ).ToList();
            }

                return View(denunciasList);
        }
    }
}
