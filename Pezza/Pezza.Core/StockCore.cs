namespace Pezza.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Pezza.Common.DTO;
    using Pezza.Common.Mapping;
    using Pezza.Core.Contracts;
    using Pezza.DataAccess.Contracts;

    public class StockCore : IStockCore
    {
        private readonly IStockDataAccess dataAccess;

        public StockCore(IStockDataAccess dataAccess) => this.dataAccess = dataAccess;

        public async Task<StockDTO> GetAsync(int id)
        {
            var search = await this.dataAccess.GetAsync(id);
            return search.Map();
        }

        public async Task<IEnumerable<StockDTO>> GetAllAsync()
        {
            var search = await dataAccess.GetAllAsync();
            return search.Map();
        }

        public async Task<StockDTO> UpdateAsync(StockDTO model)
        {
            var outcome = await dataAccess.UpdateAsync(model.Map());
            return outcome.Map();
        }

        public async Task<StockDTO> SaveAsync(StockDTO model)
        {
            var entity = await this.dataAccess.GetAsync(model.Id);

            if (!string.IsNullOrEmpty(model.Name))
            {
                entity.Name = model.Name;
            }

            if (!string.IsNullOrEmpty(model.UnitOfMeasure))
            {
                entity.UnitOfMeasure = model.UnitOfMeasure;
            }

            if (model.ValueOfMeasure.HasValue)
            {
                entity.ValueOfMeasure = model.ValueOfMeasure;
            }

            if (model.Quantity.HasValue)
            {
                entity.Quantity = model.Quantity.Value;
            }

            if (model.ExpiryDate.HasValue)
            {
                entity.ExpiryDate = model.ExpiryDate;
            }

            if (!string.IsNullOrEmpty(model.Comment))
            {
                entity.Comment = model.Comment;
            }

            var outcome = await this.dataAccess.UpdateAsync(entity);
            return outcome.Map();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var outcome = await this.dataAccess.DeleteAsync(id);
            return outcome;
        }
    }
}
