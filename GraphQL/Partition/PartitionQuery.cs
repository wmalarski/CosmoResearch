using CosmoResearch.Models;
using CosmoResearch.Services;
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
            [DataLoader] PartitionByKeyDataLoader partitionLoader,
            [Service] DataService dataService
        ) =>
            partitionLoader.LoadAsync(new PartitionKeyPair(dataService.TableKey, path), cancellationToken);
    }
}