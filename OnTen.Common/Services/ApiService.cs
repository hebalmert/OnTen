using Newtonsoft.Json;
using OnTen.Common.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnTen.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                //Sistema de Conexion del API de Internet
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                //Cargamos el Prefijo y el Controlador del API de Internet
                string url = $"{servicePrefix}{controller}";

                //Tenemos una Respuesta con el Servicio hhtpResponseMessage
                HttpResponseMessage response = await client.GetAsync(url);

                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
