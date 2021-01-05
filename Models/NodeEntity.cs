using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Models
{
    
    public enum NodeType 
    {
        String = 0,
        Int32 = 1,
        Int64 = 2,
        Double = 3,
    }

    public record NodeKey(
        string path, 
        string key
    );
    
    public class NodeEntity: TableEntity
    {

        public NodeEntity()
        {
        }

        public NodeEntity(string path, string key)
        {
            PartitionKey = path;
            RowKey = key;
        }

        public uint[] Size { get; set; }

        public NodeType Type { get; set; }

        public string[] StringData { get; set; }

        public int[] IntData { get; set; }

        public long[] LongData { get; set; }

        public double[] DoubleData { get; set; }        
    }
}
