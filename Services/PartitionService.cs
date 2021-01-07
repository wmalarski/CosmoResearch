using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Settings;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services
{
    public class PartitionService
    {


        private readonly CloudTable _partitionTable;

        public PartitionService(IDatabaseSettings databaseSettings)
        {
            _partitionTable = DatabaseUtils.CreateTable(databaseSettings.ConnectionString, databaseSettings.PartitionDatabaseName);
        }

        public async Task<PartitionEntity> InsertOrMergeAsync(PartitionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            TableOperation operation = TableOperation.InsertOrMerge(entity);
            TableResult result = await _partitionTable.ExecuteAsync(operation);
            return result.Result as PartitionEntity;
        }

        public async Task<PartitionEntity> RetrieveAsync(PartitionKeyPair pair)
        {
            var result = await _partitionTable.ExecuteAsync(
                TableOperation.Retrieve<PartitionEntity>(pair.table, pair.path)
            );
            return result.Result as PartitionEntity;
        }

        public async Task<IEnumerable<PartitionEntity>> RetrieveAsync(IReadOnlyList<PartitionKeyPair> pairs)
        {
            var operation = new TableBatchOperation();

            foreach (var pair in pairs)
            {
                operation.Retrieve<PartitionEntity>(pair.table, pair.path);
            };

            var results = await _partitionTable.ExecuteBatchAsync(operation);

            return results.Select(result => result.Result as PartitionEntity);
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