using System.Text.Json.Serialization; //converts c# objects into JSON

//Enums
public enum PreferredTime
{
    Morning,
    Afternoon,
    Evening
}
public enum PaxType
{
    Adult,
    Child,
    Infant,
    Senior,
    Concession
}
public enum CabinClass
{
    Economy,
    PremiumEconomy,
    Business,
    First
}
public enum JourneyType
{
    OneWay,
    Return,
    MultiCity
}
//Records
public record Carrier( )
{ 
    [JsonPropertyName("carrierCode")] public string? CarrierCode { get; set; } //optional
}

public record Route([property:JsonPropertyName("originCode")] string OriginCode, //required
    [property:JsonPropertyName("destinationCode")] string DestinationCode,//required
    [property:JsonPropertyName("date")] string Date ) //required 
{
    [JsonPropertyName("preferredTime")] public PreferredTime? PreferredTime { get; set; } //optional
}

public record PaxDetail()
{
    [JsonPropertyName("dateOfBirth")] public string? DateOfBirth { get; set; }//optional
    [JsonPropertyName("paxNo")] public int? PaxNo { get; set; } //optional
    [JsonPropertyName("paxType")] public PaxType? PaxType { get; set; } //optional
    [JsonPropertyName("age")] public int? Age { get; set; } //optional
}

public record FlightSearchRequest(
    [property:JsonPropertyName("companyId")] string CompanyId, //required
    [property:JsonPropertyName("cabinClass")] CabinClass CabinClass, //required
    [property:JsonPropertyName("journeyType")] JourneyType JourneyType, //required
    [property:JsonPropertyName("route")] Route Route //required
    )
{
    //all remaining optional properties are set here
    [JsonPropertyName("partnerId")] public string? PartnerId { get; set; }
    [JsonPropertyName("searchId")] public string? SearchId { get; set; }
    [JsonPropertyName("carriers")] public List<Carrier> ? Carriers { get; set; }
    [JsonPropertyName("customerId")] public string? CustomerId { get; set; }
    [JsonPropertyName("campaignId")] public string? CampaignId { get; set; }
    [JsonPropertyName("currencyCode")] public string? CurrencyCode { get; set; }
    [JsonPropertyName("paxDetails")] public List<PaxDetail> ? PaxDetails {  get; set; }
    [JsonPropertyName("availableOnly")] public bool? AvailableOnly { get; set; }
}