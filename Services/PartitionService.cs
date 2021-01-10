using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Settings;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services
{
    public class PartitionService
    {

        private readonly CloudTable _partitionTable;
        private readonly IDatabaseSettings _databaseSettings;

        public PartitionService(IDatabaseSettings databaseSettings)
        {
            _partitionTable = DatabaseUtils.CreateTable(databaseSettings.ConnectionString, databaseSettings.PartitionDatabaseName);
            _databaseSettings = databaseSettings;
        }

        public async Task<PartitionEntity> InsertOrMergeAsync(PartitionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.PartitionKey = _databaseSettings.DatabaseName;
            TableOperation operation = TableOperation.InsertOrMerge(entity);
            TableResult result = await _partitionTable.ExecuteAsync(operation);
            return result.Result as PartitionEntity;
        }

        public async Task<PartitionEntity> RetrieveAsync(string path)
        {
            var result = await _partitionTable.ExecuteAsync(
                TableOperation.Retrieve<PartitionEntity>(_databaseSettings.DatabaseName, path)
            );
            return result.Result as PartitionEntity;
        }

        public async Task<IEnumerable<PartitionEntity>> RetrieveAsync(IReadOnlyList<string> paths)
        {
            var operation = new TableBatchOperation();

            foreach (var path in paths)
            {
                operation.Retrieve<PartitionEntity>(_databaseSettings.DatabaseName, path);
            };

            var results = await _partitionTable.ExecuteBatchAsync(operation);

            return results.Select(result => result.Result as PartitionEntity);
        }

        public async Task<IEnumerable<PartitionEntity>> SearchAsync(string path, CancellationToken cancellationToken)
        {
            // https://github.com/Azure-Samples/azure-cosmos-table-dotnet-core-getting-started/blob/main/CosmosTableSamples/AdvancedSamples.cs
            var condition = DatabaseUtils.GenerateStartsWithFilterCondition("RowKey", path);
            var query = new TableQuery<PartitionEntity>().Where(condition);
            query.TakeCount = 50;

            var results = new List<PartitionEntity>();
            TableContinuationToken token = null;

            do
            {
                var segment = await _partitionTable.ExecuteQuerySegmentedAsync(query, token, cancellationToken);
                results.AddRange(segment.Results);

                token = segment.ContinuationToken;
            }
            while(token != null);

            return results;
        }

        public async Task DeleteAsync(PartitionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("deleteEntity");
            }

            await _partitionTable.ExecuteAsync(TableOperation.Delete(entity));
        }
    }
}