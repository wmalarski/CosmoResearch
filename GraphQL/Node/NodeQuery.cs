using CosmoResearch.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace CosmoResearch.GraphQL.Node
{
    [ExtendObjectType(Name = "Query")]
    public class NodeQuery
    {
        public Task<NodeEntity> GetNodeAsync(
            string path,
            string key,
            CancellationToken cancellationToken,
            [DataLoader] NodeByKeyDataLoader nodeDataLoader
        ) =>
            nodeDataLoader.LoadAsync(new NodeKey(path, key), cancellationToken);
    }
}