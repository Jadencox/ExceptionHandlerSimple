using ExceptionHandlerSimple.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlerSimple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IServiceProvider serviceProvider { get; set; }
        public BaseController(IServiceProvider _serviceProvider) 
        {
            serviceProvider = _serviceProvider;
        }

        public ILoggerHandler Logger => serviceProvider.GetService<ILoggerHandler>();
    }
}
