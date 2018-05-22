using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // /about for phone action
    [Route("[controller]")]
    //[Route("company/[controller]/[action]")]
    public class ServiceController
    {
        [Route("")]
        public string Phone()
        {
            return "1223123 AboutController!";
        }

        [Route("[action]")]
        public string Address()
        {
            return "USA address AboutController!";
        }
    }
}
