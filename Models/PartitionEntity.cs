using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Models
{

    public class PartitionEntity: TableEntity
    {

        public PartitionEntity()
        {
        }

        public PartitionEntity(string path)
        {
            RowKey = path;
        }

        public string[] VideoUrls { get; set; }

        public string[] DataUrls { get; set; }

    }
}
