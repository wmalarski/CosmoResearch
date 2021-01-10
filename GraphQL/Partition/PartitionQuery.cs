using CosmoResearch.Models;
using CosmoResearch.Services;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CosmoResearch.GraphQL.Partition
{
    [ExtendObjectType(Name = "Query")]
    public class PartitionQuery
    {
        public Task<PartitionEntity> GetPartitionAsync(
            string path,
            CancellationToken cancellationToken,
            [DataLoader] PartitionByKeyDataLoader partitionLoader
        ) =>
            partitionLoader.LoadAsync(path, cancellationToken);

        public Task<IEnumerable<PartitionEntity>> GetPartitionsAsync(
            string like,
            CancellationToken cancellationToken,
            [Service] PartitionService partitionService
        ) =>
            partitionService.SearchAsync(like, cancellationToken);
    }

    
}