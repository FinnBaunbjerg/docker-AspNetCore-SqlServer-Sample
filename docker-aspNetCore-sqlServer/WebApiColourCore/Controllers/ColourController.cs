using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiColourData;

namespace WebApiColourCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColourController : ControllerBase
    {
        private readonly ColourContext _context;

        public ColourController( ColourContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Colours()
        {
            return Ok(_context.Colours);
        }
    }
}
