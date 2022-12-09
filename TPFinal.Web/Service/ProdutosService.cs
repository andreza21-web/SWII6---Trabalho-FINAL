using System.Net.Http;
using System;
using TPFinal.Web.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TPFinal.Web.Service
{
    public class ProdutosService
    {
        private const string URI = "http://localhost:7044/api/Produto";

        public async Task<List<Produto>> List()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var usuariosJsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Produto[]>(usuariosJsonString).ToList();
                    }
                    else
                    {
                        throw new Exception("Não foi possível listar os produtos\nErro: " + response.StatusCode);
                    }
                }
            }
        }

        public async Task<Produto> Find(int? id)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{URI}/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var usuariosJsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Produto>(usuariosJsonString);
                    }
                    else
                    {
                        throw new Exception("Não foi possível listar os produtos\nErro: " + response.StatusCode);
                    }
                }
            }
        }

        public async Task<bool> Create(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(URI, returnProdutoContent(produto));
                return result.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Update(Produto produto)
        {
            using (var client = new HttpClient())
            {

                HttpResponseMessage responseMessage = await client.PutAsync($"{URI}/{produto.Id}", returnProdutoContent(produto));
                return responseMessage.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Remove(int id)
        {
            using (var client = new HttpClient())
            {

                HttpResponseMessage responseMessage = await client.DeleteAsync($"{URI}/{id}");
                return responseMessage.IsSuccessStatusCode;
            }
        }

        private StringContent returnProdutoContent(Produto produto)
        {
            var serializedProduto = JsonConvert.SerializeObject(produto);
            return new StringContent(serializedProduto, Encoding.UTF8, "application/json");
        }
    }
}
