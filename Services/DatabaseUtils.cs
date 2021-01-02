using System;
using System.Threading.Tasks;
using CosmoResearch.Settings;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.DependencyInjection;

namespace CosmoResearch.Services 
{
    public static class DatabaseUtils 
    {
        public static CloudStorageAccount CreateAzureStorageAccount(IDatabaseSettings databaseSettings)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(databaseSettings.ConnectionString);
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

        public static CloudTable CreateTable(IDatabaseSettings databaseSettings)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateAzureStorageAccount(databaseSettings);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(databaseSettings.DatabaseName);
            if (table.CreateIfNotExists())
            {
                Console.WriteLine("Created Table named: {0}", databaseSettings.DatabaseName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", databaseSettings.DatabaseName);
            }

            Console.WriteLine();
            return table;
        }
    }
}