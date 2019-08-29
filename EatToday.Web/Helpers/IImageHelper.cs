using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EatToday.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}