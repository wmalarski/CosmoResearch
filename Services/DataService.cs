using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Settings;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services
{
    public class DataService
    {

        private readonly CloudTable _cloudTable;

        public readonly string TableKey;

        public DataService(IDatabaseSettings databaseSettings)
        {
            TableKey = databaseSettings.DatabaseName;
            _cloudTable = DatabaseUtils.CreateTable(databaseSettings.ConnectionString, TableKey);
        }

        public async Task<DataEntity> InsertOrMergeEntityAsync(DataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
            TableResult result = await _cloudTable.ExecuteAsync(insertOrMergeOperation);
            return result.Result as DataEntity;
        }

        public async Task<DataEntity> RetrieveAsync(DataKeyPair pair)
        {
            var result = await _cloudTable.ExecuteAsync(
                TableOperation.Retrieve<DataEntity>(pair.path, pair.key)
            );
            
            return result.Result as DataEntity;
        }

        public async Task<IEnumerable<DataEntity>> RetrieveAsync(IReadOnlyList<DataKeyPair> pairs)
        {
            var operation = new TableBatchOperation();

            foreach (var pair in pairs)
            {
                operation.Retrieve<DataEntity>(pair.path, pair.key);
            };

            var results = await _cloudTable.ExecuteBatchAsync(operation);

            return results.Select(result => result.Result as DataEntity);
        }

        public async Task DeleteEntityAsync(DataEntity deleteEntity)
        {
            if (deleteEntity == null)
            {
                throw new ArgumentNullException("deleteEntity");
            }

            await _cloudTable.ExecuteAsync(TableOperation.Delete(deleteEntity));
        }
    }
}