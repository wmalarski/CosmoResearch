using System;
using CosmoResearch.Models;
using HotChocolate.Types;

namespace CosmoResearch.GraphQL.Data
{
    public class DataType : ObjectType<DataEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<DataEntity> descriptor)
        {
            descriptor.Field("path").ResolveWith<DataResolvers>(t => t.GetPath(default!));
            descriptor.Field("key").ResolveWith<DataResolvers>(t => t.GetKey(default!));
            descriptor.Field("timestamp").ResolveWith<DataResolvers>(t => t.GetTimestamp(default!));
        }

        private class DataResolvers
        {
            public string GetPath(DataEntity data) => data.PartitionKey;

            public string GetKey(DataEntity data) => data.RowKey;

            public DateTimeOffset GetTimestamp(DataEntity data) => data.Timestamp;
        }
    }
}