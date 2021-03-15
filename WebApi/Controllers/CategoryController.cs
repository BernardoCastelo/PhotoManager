using BusinessLayer;
using DataLayer;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategories categories;

        public CategoryController(ILogger<CategoryController> logger, ICategories categories)
        {
            this.logger = logger;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            try
            {
                return Ok(categories.GetAll());
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                return Ok(categories.Get(id));
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }
    }
}
