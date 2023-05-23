using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDataWarehouse
{
    public class Document
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("numberReturned")]
        public int NumberReturned { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        [JsonProperty("features.properties.stationId")]
        public string FeaturesPropertiesStationId { get; set; }

        [JsonProperty("features.properties.observed")]
        public DateTime? FeaturesPropertiesObserved { get; set; }

        [JsonProperty("features.properties.parameterId")]
        public string FeaturesPropertiesParameterId { get; set; }
    }

    public class Feature
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class Properties
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("observed")]
        public DateTime Observed { get; set; }

        [JsonProperty("parameterId")]
        public string ParameterId { get; set; }

        [JsonProperty("stationId")]
        public string StationId { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class Root
    {
        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }
    }
}
