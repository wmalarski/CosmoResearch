using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmoResearch.Models;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services
{
    public class DataService
    {

        private readonly CloudTable _cloudTable;

        public DataService(CloudTable cloudTable)
        {
            _cloudTable = cloudTable;
        }

        public async Task<DataEntity> InsertOrMergeEntityAsync(DataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Create the InsertOrReplace table operation
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

            // Execute the operation.
            TableResult result = await _cloudTable.ExecuteAsync(insertOrMergeOperation);
            DataEntity insertedNode = result.Result as DataEntity;

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
            }

            return insertedNode;

        }

        public async Task<DataEntity> RetrieveAsync(DataKey key)
        {
            var result = await _cloudTable.ExecuteAsync(
                TableOperation.Retrieve<DataEntity>(key.path, key.key)
            );
            
            return result.Result as DataEntity;
        }

        public async Task<IEnumerable<DataEntity>> RetrieveAsync(IReadOnlyList<DataKey> keys)
        {
            var batchOperation = new TableBatchOperation();

            foreach (var key in keys)
            {
                batchOperation.Retrieve<DataEntity>(key.path, key.key);
            };

            var results = await _cloudTable.ExecuteBatchAsync(batchOperation);

            return results.Select(result => result.Result as DataEntity);
        }

        public async Task DeleteEntityAsync(DataEntity deleteEntity)
        {
            if (deleteEntity == null)
            {
                throw new ArgumentNullException("deleteEntity");
            }

            TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
            TableResult result = await _cloudTable.ExecuteAsync(deleteOperation);

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of Delete Operation: " + result.RequestCharge);
            }
        }
    }
}