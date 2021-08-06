#if !NETFRAMEWORK
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Service1
{
    // ASP.NET Core does not have a type called ApiController out of the box
    public abstract class ApiController : ControllerBase { }
}
#endif