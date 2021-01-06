using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Services;
using GreenDonut;
using HotChocolate.DataLoader;

namespace CosmoResearch.GraphQL.Data
{    
    public class DataByKeyDataLoader : BatchDataLoader<DataKey, DataEntity>
    {
        private readonly DataService _tableService;

        public DataByKeyDataLoader(
            IBatchScheduler batchScheduler,
            DataService tableService)
            : base(batchScheduler)
        {
            _tableService = tableService;
        }

        protected override async Task<IReadOnlyDictionary<DataKey, DataEntity>> LoadBatchAsync(
            IReadOnlyList<DataKey> keys,
            CancellationToken cancellationToken)
        {
            var nodes = await _tableService.RetrieveAsync(keys);
            return nodes.ToDictionary(node => new DataKey(node.PartitionKey, node.RowKey));
        }
    }
}