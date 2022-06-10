using OnTen.Common.Responses;

namespace OnTen.Web.Helper
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
