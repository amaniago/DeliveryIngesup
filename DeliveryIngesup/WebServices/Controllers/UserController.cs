using System.Web.Http;

namespace WebServices.Controllers
{
    public class UserController : ApiController
    {
        public string GetTest()
        {
            return "Hello";
        }
    }
}
