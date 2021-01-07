using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Models
{

    public record PartitionKeyPair(
        string table, 
        string path
    );

    public class PartitionEntity: TableEntity
    {

        public PartitionEntity()
        {
        }

        public PartitionEntity(PartitionKeyPair pair)
        {
            PartitionKey = pair.table;
            RowKey = pair.path;
        }

        public string[] VideoUrls { get; set; }

        public string[] DataUrls { get; set; }

    }
}
