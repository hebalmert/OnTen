using Newtonsoft.Json;
using OnTen.Common.Request;
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

        public async Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request)
        {
            try
            {
                //Serializar el Modelo request (Tiene el usuario y clave)
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };


                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = token
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
