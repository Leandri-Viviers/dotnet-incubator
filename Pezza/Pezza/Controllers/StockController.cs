namespace Pezza.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Pezza.Common.DTO;
    using Pezza.Common.Entities;
    using Pezza.Core.Contracts;

    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase {
        private readonly IStockCore StockCore;
        public StockController(IStockCore StockCore) => this.StockCore = StockCore;

        /// <summary>
        /// Get stock by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Get(int id)
        {
            var search = await this.StockCore.GetAsync(id);
            if (search == null)
            {
                return this.NotFound();
            }

            return this.Ok(search);
        }

        /// <summary>
        /// Get all stock.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Search()
        {
            var result = await this.StockCore.GetAllAsync();

            return this.Ok(result);
        }

        /// <summary>
        /// Create new stock.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Stock
        ///     {
        ///         "name": "Tomatoes",
        ///         "unitOfMeasure": "Kg",
        ///         "valueOfMeasure": "1",
        ///         "Quantity": "50"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Stock>> Create([FromBody] StockDTO model)
        {
            var result = await this.StockCore.SaveAsync(model);
            if (result == null)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Update stock.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Stock/1
        ///     {
        ///         "Quantity": "30"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Update(int id, [FromBody] StockDTO model)
        {
            var result = await this.StockCore.UpdateAsync(model);
            if (result == null)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Remove stock by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await this.StockCore.DeleteAsync(id);
            if (!result)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }
    }
}
