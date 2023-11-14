using AutoMapper;
using CatalogService.API.Data.Interfaces;
using CatalogService.API.Data.Repositories;
using CatalogService.API.Events;
using CatalogService.API.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly ILogger<CatalogController> logger;
        private readonly ICatalogRepository catalogRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CatalogController(ICatalogRepository catalogRepository, ILogger<CatalogController> logger
            , IPublishEndpoint publishEndpoint)
        {
            this.catalogRepository = catalogRepository;
            this.logger = logger;
            this._publishEndpoint = publishEndpoint;
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Catalog/Add")]
        public async Task<IActionResult> AddCategory([FromBody] Catalog entity)
        {
            try
            {
                await catalogRepository.AddCatalog(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Item/Add")]
        public async Task<IActionResult> AddItem([FromBody] Item entity)
        {
            try
            {
                await catalogRepository.AddItem(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<CatalogController>
        [HttpPost]
        [Route("Item/Update")]
        public async Task<IActionResult> UpdateItem([FromBody] Item entity)
        {
            try
            {
                await catalogRepository.UpdateItem(entity);

                // send checkout event to rabbitmq
                await _publishEndpoint.Publish<Item>(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
