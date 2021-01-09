using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Services;
using GreenDonut;
using HotChocolate.DataLoader;

namespace CosmoResearch.GraphQL.Partition
{    
    public class PartitionByKeyDataLoader : BatchDataLoader<string, PartitionEntity>
    {
        private readonly PartitionService _partitionService;

        public PartitionByKeyDataLoader(
            IBatchScheduler batchScheduler,
            PartitionService partitionService)
            : base(batchScheduler)
        {
            _partitionService = partitionService;
        }

        protected override async Task<IReadOnlyDictionary<string, PartitionEntity>> LoadBatchAsync(
            IReadOnlyList<string> paths,
            CancellationToken cancellationToken)
        {
            var nodes = await _partitionService.RetrieveAsync(paths);
            return nodes.ToDictionary(node => node.RowKey);
        }
    }
}