namespace DAL.Entities
{
    public class Property
    {
        public int PropertyId { get; set; }
        public DateTime Created { get; set; }

        public DateTime Observed { get; set; }

        public string ParameterId { get; set; }

        public string StationId { get; set; }

        public double Value { get; set; }
    }
}