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
    public class PhotoController : ControllerBase
    {
        private readonly ILogger<PhotoController> logger;
        private readonly IPhotos photos;

        public PhotoController(ILogger<PhotoController> logger, IPhotos photos)
        {
            this.logger = logger;
            this.photos = photos;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Photo>> Get(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            try
            {
                return Ok(photos.Get(skip, take, orderBy, orderByDescending));
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<Photo>> Get(int skip, int take, string orderBy = null, bool orderByDescending = false, [FromBody] List<Filter> filters = null)
        {
            try
            {
                return Ok(photos.Get(skip, take, orderBy, orderByDescending, filters));
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Photo>> GetCategories(int id)
        {
            try
            {
                return Ok(photos.GetCategories(id));
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<string> GetBytes(int id)
        {
            try
            {
                return Ok(photos.GetBytes(id));
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return StatusCode(500);
            }
        }
    }
}
