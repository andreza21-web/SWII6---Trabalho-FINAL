using System.Net.Http;
using System;
using TPFinal.Web.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace TPFinal.Web.Service
{
    public class UsuarioService
    {
        private const string URI = "http://localhost:7044/api/Produto";

        public async Task<Usuario> logar(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                var serializedUsuario = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(serializedUsuario, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{URI}/login", content);

                var usuariosJsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario>(usuariosJsonString);
            }
        }
    }
}
