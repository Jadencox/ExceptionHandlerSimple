using ExceptionHandlerSimple.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlerSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : BaseController
    {
        public DemoController(IServiceProvider serviceProvider) : base(serviceProvider)
        { 
        
        }

        [Route("Test")]
        [HttpGet]
        public string Test() 
        {
            //var a = "wo";
            //var b = Convert.ToInt32(a);

            Logger.LogInfo("我是第一条日志");
            //throw new NotImplementedException("123456");
            return "Test";
        }
    }
}
