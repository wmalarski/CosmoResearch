using CosmoResearch.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace CosmoResearch.GraphQL.Partition
{
    [ExtendObjectType(Name = "Query")]
    public class PartitionQuery
    {
        public Task<PartitionEntity> GetNodeAsync(
            string path,
            CancellationToken cancellationToken,
            [DataLoader] PartitionByKeyDataLoader partitionLoader
        ) =>
            partitionLoader.LoadAsync(path, cancellationToken);
    }
}