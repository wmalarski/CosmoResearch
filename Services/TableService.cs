using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmoResearch.Models;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services
{
    public class TableService
    {

        private readonly CloudTable _cloudTable;

        public TableService(CloudTable cloudTable)
        {
            _cloudTable = cloudTable;
        }

        public async Task<NodeEntity> InsertOrMergeEntityAsync(NodeEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Create the InsertOrReplace table operation
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

            // Execute the operation.
            TableResult result = await _cloudTable.ExecuteAsync(insertOrMergeOperation);
            NodeEntity insertedNode = result.Result as NodeEntity;

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
            }

            return insertedNode;

        }

        public async Task<NodeEntity> RetrieveAsync(NodeKey key)
        {
            var result = await _cloudTable.ExecuteAsync(
                TableOperation.Retrieve<NodeEntity>(key.path, key.key)
            );
            
            return result.Result as NodeEntity;
        }

        public async Task<IEnumerable<NodeEntity>> RetrieveAsync(IReadOnlyList<NodeKey> keys)
        {
            var batchOperation = new TableBatchOperation();

            foreach (var key in keys)
            {
                batchOperation.Retrieve<NodeEntity>(key.path, key.key);
            };

            var results = await _cloudTable.ExecuteBatchAsync(batchOperation);

            return results.Select(result => result.Result as NodeEntity);
        }

        public async Task DeleteEntityAsync(NodeEntity deleteEntity)
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