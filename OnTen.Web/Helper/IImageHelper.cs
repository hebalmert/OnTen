using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnTen.Web.Helper
{
    public interface IImageHelper
    {
        Task<string> UploadImage(IFormFile imageFile, string ruta, string guid);

        Task<string> UploadImage(byte[] imageFile, string ruta, string guid);

        bool DeleteImage(string ruta, string guid);
    }
}
