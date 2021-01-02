using System;
using System.Threading.Tasks;
using CosmoResearch.Models;
using CosmoResearch.Settings;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services 
{
    class TableService {

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

            try
            {
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
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task<NodeEntity> RetrieveEntityUsingPointQueryAsync(string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<NodeEntity>(partitionKey, rowKey);
                TableResult result = await _cloudTable.ExecuteAsync(retrieveOperation);
                NodeEntity node = result.Result as NodeEntity;
                if (node != null)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", node.PartitionKey, node.RowKey, node.Size, node.Type, node.StringData, node.IntData, node.DoubleData);
                }

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
                }

                return node;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task DeleteEntityAsync(NodeEntity deleteEntity)
        {
            try
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
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}