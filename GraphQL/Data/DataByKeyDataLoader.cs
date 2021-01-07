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
    public class DataByKeyDataLoader : BatchDataLoader<DataKeyPair, DataEntity>
    {
        private readonly DataService _tableService;

        public DataByKeyDataLoader(
            IBatchScheduler batchScheduler,
            DataService tableService)
            : base(batchScheduler)
        {
            _tableService = tableService;
        }

        protected override async Task<IReadOnlyDictionary<DataKeyPair, DataEntity>> LoadBatchAsync(
            IReadOnlyList<DataKeyPair> pairs,
            CancellationToken cancellationToken)
        {
            var nodes = await _tableService.RetrieveAsync(pairs);
            return nodes.ToDictionary(node => new DataKeyPair(node.PartitionKey, node.RowKey));
        }
    }
}