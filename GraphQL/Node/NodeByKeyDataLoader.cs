using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Services;
using GreenDonut;
using HotChocolate.DataLoader;

namespace CosmoResearch.GraphQL.Node
{    
    public class NodeByKeyDataLoader : BatchDataLoader<NodeKey, NodeEntity>
    {
        private readonly TableService _tableService;

        public NodeByKeyDataLoader(
            IBatchScheduler batchScheduler,
            TableService tableService)
            : base(batchScheduler)
        {
            _tableService = tableService;
        }

        protected override async Task<IReadOnlyDictionary<NodeKey, NodeEntity>> LoadBatchAsync(
            IReadOnlyList<NodeKey> keys,
            CancellationToken cancellationToken)
        {
            var nodes = await _tableService.RetrieveAsync(keys);
            return nodes.ToDictionary(node => new NodeKey(node.PartitionKey, node.RowKey));
        }
    }
}