using Newtonsoft.Json;
using System;

namespace Entity.Service.Models
{
    public class GeoServiceResponse
    {
        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("entityType", NullValueHandling = NullValueHandling.Ignore)]
        public string EntityType { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("viewport")]
        public BoundingBox Viewport { get; set; }

        [JsonProperty("boundingBox", NullValueHandling = NullValueHandling.Ignore)]
        public BoundingBox BoundingBox { get; set; }

        [JsonProperty("dataSources", NullValueHandling = NullValueHandling.Ignore)]
        public DataSources DataSources { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("municipality", NullValueHandling = NullValueHandling.Ignore)]
        public string Municipality { get; set; }

        [JsonProperty("countrySecondarySubdivision")]
        public string CountrySecondarySubdivision { get; set; }

        [JsonProperty("countryTertiarySubdivision", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryTertiarySubdivision { get; set; }

        [JsonProperty("countrySubdivision")]
        public string CountrySubdivision { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCodeISO3")]
        public string CountryCodeIso3 { get; set; }

        [JsonProperty("freeformAddress")]
        public string FreeformAddress { get; set; }

        [JsonProperty("municipalitySubdivision", NullValueHandling = NullValueHandling.Ignore)]
        public string MunicipalitySubdivision { get; set; }

        [JsonProperty("streetName", NullValueHandling = NullValueHandling.Ignore)]
        public string StreetName { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }

        [JsonProperty("localName", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalName { get; set; }
    }

    public partial class BoundingBox
    {
        [JsonProperty("topLeftPoint")]
        public Position TopLeftPoint { get; set; }

        [JsonProperty("btmRightPoint")]
        public Position BtmRightPoint { get; set; }
    }

    public partial class Position
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }

    public partial class DataSources
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public partial class Summary
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        [JsonProperty("queryTime")]
        public long QueryTime { get; set; }

        [JsonProperty("numResults")]
        public long NumResults { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("fuzzyLevel")]
        public long FuzzyLevel { get; set; }
    }
}
