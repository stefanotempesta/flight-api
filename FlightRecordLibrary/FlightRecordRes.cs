using System.Runtime.ExceptionServices;
using System.Text.Json.Serialization; //converts c# objects into JSON


//Enums
public enum Status {
    Available,
    Unavailable,
    Request
}
public enum ExtraType
{
    Extra,
    Offer,
    Discount,
    Surcharge,
    Tax,
    Fee
}
public enum Frequency
{
    PerNight,
    PerStay
}
public enum Mode {
    PerPerson,
    PerService
}
public enum AmountUnit
{
    Percentage,
    Currency
}
public enum BaggageType {
    Checked,
    CarryOn
}
public enum PolicyType
{
    BookingTerms,
    BookingPolicy,
    AmendmentPolicy,
    CancellationPolicy,
    BaggageAllowance
}
public enum TimeType
{
    CheckIn,
    CheckOut,
    EarlyCheckIn,
    LateCheckOut,
    DayUse,
    Open,
    Closed
}
public enum CusDaysOfWeek
{
    Mon,
    Tue,
    Wed,
    Thu,
    Fri,
    Sat,
    Sun
}
public enum PaymentType
{
    Deposit,
    SecondDeposit,
    ThirdDeposit,
    FourthDeposit,
    FifthDeposit,
    BalanceDue,
    CommissionDue
}
public enum AmendmentType
{
    FirstAmendment,
    SecondAmendment,
    ThirdAmendment,
    FourthAmendment,
    FifthAmendment,
    SubsequentAmendment,
    AllAmendments,
}
public enum AmountType
{
    Percentage,
    PercentageOfPrice,
    PercentageOfNetPrice,
    PercentageOfCost,
    PerAdult,
    PerChild,
    PerAdultAndChild,
    PerPerson,
    PerHour,
    PerRoomUnit,
    PerService,
    DaysTariff,
    NightsTariff
}

public enum Period
{
    AfterBooking,
    AfterConfirmation,
    BeforeUsageTravel,
    AfterUsageTravel
}
public enum Restrictions
{
    CheckInOnly,
    CheckOutOnly,
    PickUpOnly,
    DropOffOnly,
    Temporary,
    Permanent,
    NotAllowed
}
public record Segment(
    [property: JsonPropertyName("segmentId")] string SegmentId, //required
    [property: JsonPropertyName("departureCode")] string DepartureCode, //required
    [property: JsonPropertyName("departureTime")] string DepartureTime, //required
    [property: JsonPropertyName("arrivalCode")] string ArrivalCode, //required
    [property: JsonPropertyName("arrivalDate")] string ArrivalDate, //required
    [property: JsonPropertyName("arrivalTime")] string ArrivalTime, //required
    [property: JsonPropertyName("ticketingCarrierCode")] string TicketingCarrierCode, //required
    [property: JsonPropertyName("operatingCarrierCode")] string OperatingCarrierCode, //required
    [property: JsonPropertyName("flightNo")] string FlightNo)//required
{ 
    [JsonPropertyName("departureTerminal")] public string? DepartureTerminal { get; set; } //optional
    [JsonPropertyName("arrivalTerminal")] public string? ArrivalTerminal { get; set; } //optional
    [JsonPropertyName("duration")] public string? Duration { get; set; } //optional
    [JsonPropertyName("aircraft")] public string? Aircraft { get; set; } //optional
    [JsonPropertyName("numberOfStops")] public int? NumberOfStops { get; set; } //optional
}

public record Itinerary(
    [property: JsonPropertyName("segments")] List<Segment> Segments) //required
{
    [JsonPropertyName("duration")] public string? Duration { get; set; } //optional
}

public record Extra (
    [property:JsonPropertyName("type")] ExtraType Type, //required
    [property:JsonPropertyName("departureDate")] string DepartureDate) //required
{
    [JsonPropertyName("code")] public string? Code { get; set; } //optional
    [JsonPropertyName("description")] public string? Description { get; set; } //optional
    [JsonPropertyName("included")] public bool? Included { get; set; } //optional
    [JsonPropertyName("frequency")] public Frequency? Frequency { get; set; } //optional 
    [JsonPropertyName("mode")] public Mode? Mode { get; set; }
    [JsonPropertyName("amount")] public decimal? Amount { get; set; }
    [JsonPropertyName("amountUnit")] public AmountUnit? AmountUnit { get; set; }
    [JsonPropertyName("extraCurrencyCode")] public string? CurrencyCode { get; set; }
    [JsonPropertyName("countryCode")] public string? CountryCode { get; set; }
}

public record PriceDetail() 
{
    [JsonPropertyName("currencyCode")] public string? CurrencyCode { get; set; }
    [JsonPropertyName("price")] public decimal? Price { get; set; }
    [JsonPropertyName("cost")] public decimal? Cost { get; set; }
    [JsonPropertyName("commissionAmount")] public decimal CommissionAmount { get; set; }
    [JsonPropertyName("commissionPercentage")] public decimal? CommissionPercentage { get; set; }
    [JsonPropertyName("paxDetails")] public List<Extra>? Extras { get; set; }
}
public record PaxSegmentDetails(
    [property: JsonPropertyName("segmentId")] string SegmentId,  // Required
    [property: JsonPropertyName("cabinClass")] CabinClass CabinClass )  // Required
{
    [JsonPropertyName("fareBasisCode")] public string? FareBasisCode { get; set; }   // Optional with get/set
    [JsonPropertyName("brandedFareCode")] public string? BrandedFareCode { get; set; } // Optional with get/set
    [JsonPropertyName("brandedFareName")] public string? BrandedFareName { get; set; } // Optional with get/set
    [JsonPropertyName("bookingClass")] public string? BookingClass { get; set; }  // Optional with get/set
    [JsonPropertyName("seatNo")] public string? SeatNo { get; set; }  // Optional with get/set

}
public record BaggageAllowance()
{
    [JsonPropertyName("baggageType")] public BaggageType? Type { get; set; }  
    [JsonPropertyName("quantity")] public int? Quantity { get; set; }  
    [JsonPropertyName("weight")] public int? Weight { get; set; }
    [JsonPropertyName("weightUnit")] public string? WeightUnit { get; set; }
}

public record Amenities()
{
    [JsonPropertyName("amenitiesType")] public string? Type {  get; set; }
    [JsonPropertyName("code")] public string? Code { get; set; }
    [JsonPropertyName("amenitiesDescription")] public string? Description {  get; set; }
    [JsonPropertyName("amenitiesIncluded")]public bool? Included { get; set; }
}


public record PaxAllocation()
{
    [JsonPropertyName("paxNo")] public int? PaxNo { get; set; }  
    [JsonPropertyName("paxType")] public PaxType? PaxType { get; set; } 
    [JsonPropertyName("currencyCode")] public string? CurrencyCode { get; set; } 
    [JsonPropertyName("price")] public decimal? Price { get; set; }
    [JsonPropertyName("cost")]  public decimal? Cost { get; set; }
    [JsonPropertyName("commissionAmount")] public decimal? CommissionAmount { get; set; }
    [JsonPropertyName("commissionPercentage")] public decimal? CommissionPercentage { get; set; }
    [JsonPropertyName("segmentDetails")] public List<PaxSegmentDetails>? SegmentDetails { get; set; }
    [JsonPropertyName("baggageAllowance")] public List<BaggageAllowance>? BaggageAllowance { get; set; }
    [JsonPropertyName("amenities")] public List<Amenities>? Amenities { get; set; }
}


public record PolicyDetail()
{
    [JsonPropertyName("paxType")] public PaxType? PaxType { get; set; } 
    [JsonPropertyName("minimumAge")] public int? MinimumAge { get; set; } 
    [JsonPropertyName("maximumAge")] public int? MaximumAge { get; set; }
    [JsonPropertyName("timeType")] public TimeType? TimeType { get; set; } 
    [JsonPropertyName("startTime")] public string? StartTime { get; set; } 
    [JsonPropertyName("endTime")] public string? EndTime { get; set; } 
    [JsonPropertyName("daysOfWeek")] public CusDaysOfWeek? DaysOfWeek { get; set; } 
    [JsonPropertyName("paymentType")] public PaymentType? PaymentType { get; set; }
    [JsonPropertyName("amendmentType")] public AmendmentType? AmendmentType { get; set; } 
    [JsonPropertyName("currencyCode")] public string? CurrencyCode { get; set; } 
    [JsonPropertyName("amount")] public decimal? Amount { get; set; } 
    [JsonPropertyName("amountType")] public AmountType? AmountType { get; set; }
    [JsonPropertyName("days")] public int? Days { get; set; } 
    [JsonPropertyName("period")] public Period? Period { get; set; }
    [JsonPropertyName("date")] public  DateTime? Date { get; set; }
    [JsonPropertyName("restrictions")] public Restrictions? Restrictions { get; set; }

}

public record Policy(
    [property: JsonPropertyName("policyType")] PolicyType PolicyType,  // Required
    [property: JsonPropertyName("policyName")] string PolicyName  // Required
)
{
    [JsonPropertyName("policyDescription")] public string? PolicyDescription { get; set; } 
    [JsonPropertyName("termsAndConditions")] public string? TermsAndConditions { get; set; } 
    [JsonPropertyName("refundable")] public bool? Refundable { get; set; }
    [JsonPropertyName("policyDetails")] public List<PolicyDetail>? PolicyDetails { get; set; }
}

public record Offer(
    [property:JsonPropertyName("carrierCode")] string CarrierCode,  // Required
    [property: JsonPropertyName("cabinClass")] CabinClass CabinClass,  // Required
    [property: JsonPropertyName("journeyType")] JourneyType JourneyType // Required  
)
{
    [JsonPropertyName("offerId")] public string? OfferId { get; set; } 
    [JsonPropertyName("source")] public string? Source { get; set; } 
    [JsonPropertyName("bookingToken")] public string? BookingToken { get; set; } 
    [JsonPropertyName("flightOffer")] public string? FlightOffer { get; set; } 
    [JsonPropertyName("isUpsellOffer")] public bool? IsUpsellOffer { get; set; } 
    [JsonPropertyName("lastTicketingDate")] public DateTime? LastTicketingDate { get; set; } 
    [JsonPropertyName("status")] public Status? Status { get; set; } 
    [JsonPropertyName("availability")] public int? Availability { get; set; } 
    [JsonPropertyName("itineraries")] public List<Itinerary>? Itineraries { get; set; } 
    [JsonPropertyName("priceDetails")] public List<PriceDetail>? PriceDetails { get; set; } 
    [JsonPropertyName("extras")] public List<Extra>? Extras { get; set; }
    [JsonPropertyName("paxAllocation")] public List<PaxAllocation>? PaxAllocation { get; set; } 
    [JsonPropertyName("policies")] public List<Policy>? Policies { get; set; }
}


public record FlightSearchResponse(
    [property: JsonPropertyName("offers")] List<Offer> Offers //required
 );


