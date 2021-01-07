using CosmoResearch.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace CosmoResearch.GraphQL.Data
{
    [ExtendObjectType(Name = "Query")]
    public class DataQuery
    {
        public Task<DataEntity> GetNodeAsync(
            string path,
            string key,
            CancellationToken cancellationToken,
            [DataLoader] DataByKeyDataLoader nodeDataLoader
        ) =>
            nodeDataLoader.LoadAsync(new DataKeyPair(path, key), cancellationToken);
    }
}