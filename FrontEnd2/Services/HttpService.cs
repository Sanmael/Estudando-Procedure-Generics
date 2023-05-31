using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontEnd2.Services
{
    public static class HttpService
    {
        public static async Task<string> GetAsync(string url, long? id = null)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(3000);

                HttpResponseMessage response = await httpClient.GetAsync($"{url}/{id}");

                string jsonResponse = String.Empty;

                if (response.IsSuccessStatusCode)
                {
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }

                else
                    throw new Exception("Erro ao se comunicar com o Servidor");

                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<string> PostAsync<T>(string url, T Content)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                httpClient.Timeout = TimeSpan.FromSeconds(3000);

                var json = JsonSerializer.Serialize(Content);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync($"{url}",stringContent);

                string jsonResponse = String.Empty;

                if (response.IsSuccessStatusCode)
                {
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }

                else
                    throw new Exception("Erro ao se comunicar com o Servidor");

                return jsonResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
