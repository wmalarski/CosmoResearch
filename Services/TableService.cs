using System;
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

        public async Task<NodeEntity> RetrieveEntityUsingPointQueryAsync(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<NodeEntity>(partitionKey, rowKey);
            TableResult result = await _cloudTable.ExecuteAsync(retrieveOperation);
            NodeEntity node = result.Result as NodeEntity;

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
            }

            return node;
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