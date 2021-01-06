using Microsoft.Azure.Cosmos.Table;

namespace CosmoResearch.Models
{
    
    public enum DataType 
    {
        String = 0,
        Int32 = 1,
        Int64 = 2,
        Double = 3,
    }

    public record DataKey(
        string path, 
        string key
    );
    
    public class DataEntity: TableEntity
    {

        public DataEntity()
        {
        }

        public DataEntity(string path, string key)
        {
            PartitionKey = path;
            RowKey = key;
        }

        public uint[] Size { get; set; }

        public DataType Type { get; set; }

        public string[] StringData { get; set; }

        public int[] IntData { get; set; }

        public long[] LongData { get; set; }

        public double[] DoubleData { get; set; }        
    }
}
