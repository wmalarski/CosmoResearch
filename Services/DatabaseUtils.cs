using System;
using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Services 
{
    public static class DatabaseUtils 
    {
        public static CloudStorageAccount CreateAzureStorageAccount(string connectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public static CloudTable CreateTable(string connectionString, string databaseName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateAzureStorageAccount(connectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(databaseName);
            if (table.CreateIfNotExists())
            {
                Console.WriteLine("Created Table named: {0}", databaseName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", databaseName);
            }

            Console.WriteLine();
            return table;
        }

        public static string GenerateStartsWithFilterCondition(string propertyName, string prefix) 
        {
            return TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(propertyName, QueryComparisons.GreaterThanOrEqual, prefix),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(propertyName, QueryComparisons.LessThan, $"{prefix}~")
            );
        }
    }
}