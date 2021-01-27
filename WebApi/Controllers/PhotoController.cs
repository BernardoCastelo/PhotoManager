using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using BusinessLayer;
using DataLayer;

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
        public IEnumerable<Photo> Get(int skip, int take, string orderBy = null, bool orderByDescending = false)
        {
            try
            {
                return photos.Get(skip, take, orderBy, orderByDescending);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string GetBytes(int id)
        {
            try
            {
                return photos.GetBytes(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
