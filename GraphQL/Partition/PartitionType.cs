using System;
using System.Threading;
using System.Threading.Tasks;
using CosmoResearch.GraphQL.Data;
using CosmoResearch.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CosmoResearch.GraphQL.Partition
{
    public class PartitionType : ObjectType<PartitionEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<PartitionEntity> descriptor)
        {
            descriptor.Field("path").ResolveWith<PartitionResolvers>(t => t.GetPath(default!));
            descriptor.Field("timestamp").ResolveWith<PartitionResolvers>(t => t.GetTimestamp(default!));
            descriptor.Field("data").Argument("key", d => d.Type<StringType>()).ResolveWith<PartitionResolvers>(t => t.GetData(default!, default!, default!, default!));
        }

        private class PartitionResolvers
        {
            public string GetPath(PartitionEntity partition) => partition.RowKey;

            public DateTimeOffset GetTimestamp(PartitionEntity partition) => partition.Timestamp;

            public async Task<DataEntity> GetData(
                PartitionEntity data, 
                string key,
                [DataLoader] DataByKeyDataLoader dataLoader,
                CancellationToken cancellationToken
            ) => await dataLoader.LoadAsync(new DataKeyPair(data.RowKey, key), cancellationToken);
        }
    }
}