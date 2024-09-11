using FluentValidation;

public class SegmentValidator : AbstractValidator<Segment>
{
    public SegmentValidator()
    {
        RuleFor(x => x.SegmentId)
            .NotEmpty()
            .Length(1, 10); 

        RuleFor(x => x.DepartureCode)
            .NotEmpty()
            .Length(1, 10); 

        RuleFor(x => x.DepartureTime)
            .NotEmpty()
            .Length(1, 10); 

        RuleFor(x => x.ArrivalCode)
            .NotEmpty()
            .Length(1, 10); // Required with length 1 to 10

        RuleFor(x => x.ArrivalDate)
            .NotEmpty()
            .Must(BeAValidDate).WithMessage("ArrivalDate must be in a valid format");

        RuleFor(x => x.ArrivalTime)
            .NotEmpty()
            .Length(1, 10); // Required with length 1 to 10

        RuleFor(x => x.TicketingCarrierCode)
            .NotEmpty()
            .Length(1, 10); // Required with length 1 to 10

        RuleFor(x => x.OperatingCarrierCode)
            .NotEmpty()
            .Length(1, 10); // Required with length 1 to 10

        RuleFor(x => x.FlightNo)
            .NotEmpty()
            .Length(1, 10); // Required with length 1 to 10

        // Optional fields
        RuleFor(x => x.DepartureTerminal)
            .MinimumLength(1).When(x => x.DepartureTerminal != null)
            .MaximumLength(100).When(x => x.DepartureTerminal != null);
       
        RuleFor(x => x.ArrivalTerminal)
            .MinimumLength(1).When(x => x.ArrivalTerminal != null)
            .MaximumLength(100).When(x => x.ArrivalTerminal != null);
        
        RuleFor(x => x.Duration)
            .MinimumLength(1).When(x => x.Duration != null)
            .MaximumLength(20).When(x => x.Duration != null);
        
        RuleFor(x => x.Aircraft)
             .MinimumLength(1).When(x => x.Aircraft != null)
            .MaximumLength(50).When(x => x.Aircraft != null);
        
        RuleFor(x => x.NumberOfStops)
            .InclusiveBetween(0, 9).When(x => x.NumberOfStops.HasValue);
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }

}

// Validator for Itinerary
public class ItineraryValidator : AbstractValidator<Itinerary>
{
    public ItineraryValidator()
    {
        
        // Optional duration
        RuleFor(x => x.Duration)
            .MaximumLength(20).When(x => x.Duration != null);
    }
}

// Validator for Extra
public class ExtraValidator : AbstractValidator<Extra>
{
    public ExtraValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .IsInEnum(); // Required enum

        RuleFor(x => x.DepartureDate)
            .NotEmpty()
            .Must(BeAValidDate).WithMessage("DepartureDate must be in a valid format");

        RuleFor(x => x.Code)
            .MinimumLength(1).When(x => x.Code != null)
            .MaximumLength(20).When(x => x.Code != null);

        RuleFor(x => x.Description)
            .MinimumLength(1).When(x => x.Description != null)
            .MaximumLength(200).When(x => x.Description != null);

        RuleFor(x => x.Frequency)
            .IsInEnum().When(x => x.Frequency.HasValue);

        RuleFor(x => x.Mode)
            .IsInEnum().When(x => x.Mode.HasValue);

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).When(x => x.Amount.HasValue);

        RuleFor(x => x.AmountUnit)
            .IsInEnum().When(x => x.AmountUnit.HasValue);

        RuleFor(x => x.CurrencyCode)
             .MinimumLength(1).When(x => x.CurrencyCode != null)
            .MaximumLength(10).When(x => x.CurrencyCode != null);

        RuleFor(x => x.CountryCode)
            .Length(2).When(x => x.CountryCode != null); // CountryCode with 2 characters
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}

// Validator for PriceDetail
public class PriceDetailValidator : AbstractValidator<PriceDetail>
{
    public PriceDetailValidator()
    {
        RuleFor(x => x.CurrencyCode)
            .MinimumLength(1).When(x => x.CurrencyCode != null)
            .MaximumLength(10).When(x => x.CurrencyCode != null);
        
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
        
        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
        
        RuleFor(x => x.CommissionAmount)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.CommissionPercentage)
            .InclusiveBetween(0, 100).When(x => x.CommissionPercentage.HasValue);

        RuleForEach(x => x.Extras)
            .SetValidator(new ExtraValidator()); // Validate each segment

    }
}

public class PaxSegmentDetailsValidator : AbstractValidator<PaxSegmentDetails>
{
    public PaxSegmentDetailsValidator()
    {
        // Required fields
        RuleFor(x => x.SegmentId)
            .NotEmpty()
            .Length(1, 10);

        RuleFor(x => x.CabinClass)
            .NotEmpty()
            .IsInEnum();

        // Optional fields
        RuleFor(x => x.FareBasisCode)
            .MinimumLength(1).When(x => x.FareBasisCode != null)
            .MaximumLength(20).When(x => x.FareBasisCode != null);

        RuleFor(x => x.BrandedFareCode)
            .MinimumLength(1).When(x => x.BrandedFareCode != null)
            .MaximumLength(20).When(x => x.BrandedFareCode != null);

        RuleFor(x => x.BrandedFareName)
            .MinimumLength(1).When(x => x.BrandedFareName != null)
            .MaximumLength(100).When(x => x.BrandedFareName != null);

        RuleFor(x => x.BookingClass)
            .MinimumLength(1).When(x => x.BookingClass != null)
            .MaximumLength(10).When(x => x.BookingClass != null);

        RuleFor(x => x.SeatNo)
            .MinimumLength(1).When(x => x.SeatNo != null)
            .MaximumLength(20).When(x => x.SeatNo != null);
    }
}
    public class BaggageAllowanceValidator : AbstractValidator<BaggageAllowance>
    {
        public BaggageAllowanceValidator()
        {
            // Optional fields
            RuleFor(x => x.Type)
                .IsInEnum().When(x => x.Type.HasValue);

            RuleFor(x => x.Quantity)
                .InclusiveBetween(0, 999).When(x => x.Quantity.HasValue);

            RuleFor(x => x.Weight)
                .InclusiveBetween(0, 999).When(x => x.Weight.HasValue);

            RuleFor(x => x.WeightUnit)
                 .MinimumLength(1).When(x => x.WeightUnit != null)
                .MaximumLength(10).When(x => x.WeightUnit != null);
        }
    }

public class AmenitiesValidator : AbstractValidator<Amenities>
{
    public AmenitiesValidator()
    {
        // Optional fields
        RuleFor(x => x.Type)
            .MinimumLength(1).When(x => x.Type != null)
            .MaximumLength(20).When(x => x.Type != null);  

        RuleFor(x => x.Code)
            .MinimumLength(1).When(x => x.Code != null)
            .MaximumLength(20).When(x => x.Code != null);  

        RuleFor(x => x.Description)
            .MinimumLength(1).When(x => x.Description != null)
            .MaximumLength(200).When(x => x.Description != null);  

        RuleFor(x => x.Included)
            .NotNull().When(x => x.Included.HasValue);  
    }
}

// Validator for PaxAllocation
public class PaxAllocationValidator : AbstractValidator<PaxAllocation>
{
    public PaxAllocationValidator()
    {
        RuleFor(x => x.PaxNo)
            .InclusiveBetween(1, 999); // Required field
        
        RuleFor(x => x.PaxType)
            .IsInEnum().When(x => x.PaxType.HasValue);

        RuleFor(x => x.CurrencyCode)
            .MinimumLength(1).When(x => x.CurrencyCode != null)
            .MaximumLength(10).When(x => x.CurrencyCode != null);
        
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
        
        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
        
        RuleFor(x => x.CommissionAmount)
            .GreaterThanOrEqualTo(0).When(x => x.CommissionAmount.HasValue);
        
        RuleFor(x => x.CommissionPercentage)
            .InclusiveBetween(0, 100).When(x => x.CommissionPercentage.HasValue);

        RuleForEach(x => x.SegmentDetails)
            .SetValidator(new PaxSegmentDetailsValidator());
        
        RuleForEach(x => x.BaggageAllowance)
            .SetValidator(new BaggageAllowanceValidator());
        
        RuleForEach(x => x.Amenities)
            .SetValidator(new AmenitiesValidator());
    }
}

// Validator for Offer
public class OfferValidator : AbstractValidator<Offer>
{
    public OfferValidator()
    {
        RuleFor(x => x.CarrierCode)
            .NotEmpty()
            .Length(1, 10); // Required carrier code

        RuleFor(x => x.CabinClass)
            .IsInEnum(); // Required enum

        RuleFor(x => x.JourneyType)
            .IsInEnum(); // Required enum

        // Optional fields
        RuleFor(x => x.OfferId).MaximumLength(10).When(x => x.OfferId != null);
        RuleFor(x => x.Source).MaximumLength(10).When(x => x.Source != null);
        RuleFor(x => x.BookingToken).MaximumLength(250).When(x => x.BookingToken != null);
        RuleFor(x => x.FlightOffer).MaximumLength(100000).When(x => x.FlightOffer != null);
        RuleFor(x => x.IsUpsellOffer).NotNull().When(x => x.IsUpsellOffer.HasValue);
        RuleFor(x => x.LastTicketingDate).Must(BeAValidDate).When(x => x.LastTicketingDate.HasValue);
        RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue);
        RuleFor(x => x.Availability).InclusiveBetween(0, 999).When(x => x.Availability.HasValue);

        // Nested lists
        RuleForEach(x => x.Itineraries).SetValidator(new ItineraryValidator());
        RuleForEach(x => x.PriceDetails).SetValidator(new PriceDetailValidator());
        RuleForEach(x => x.Extras).SetValidator(new ExtraValidator());
        RuleForEach(x => x.PaxAllocation).SetValidator(new PaxAllocationValidator());
        RuleForEach(x => x.Policies).SetValidator(new PolicyValidator());
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date == null || date > DateTime.MinValue;
    }
}

public class PolicyDetailValidator : AbstractValidator<PolicyDetail>
{
    public PolicyDetailValidator()
    {
        // Optional fields
        RuleFor(x => x.PaxType)
            .IsInEnum().When(x => x.PaxType.HasValue);  

        RuleFor(x => x.MinimumAge)
            .InclusiveBetween(0, 999).When(x => x.MinimumAge.HasValue);  

        RuleFor(x => x.MaximumAge)
            .InclusiveBetween(0, 999).When(x => x.MaximumAge.HasValue);  

        RuleFor(x => x.TimeType)
            .IsInEnum().When(x => x.TimeType.HasValue);  

        RuleFor(x => x.StartTime)
            .MinimumLength(1).When(x => x.StartTime != null)
            .MaximumLength(10).When(x => x.StartTime != null);

        RuleFor(x => x.EndTime)
            .MinimumLength(1).When(x => x.EndTime != null)
            .MaximumLength(10).When(x => x.EndTime != null);  

        RuleFor(x => x.DaysOfWeek)
            .IsInEnum().When(x => x.DaysOfWeek.HasValue);  

        RuleFor(x => x.PaymentType)
            .IsInEnum().When(x => x.PaymentType.HasValue); 

        RuleFor(x => x.AmendmentType)
            .IsInEnum().When(x => x.AmendmentType.HasValue);  

        RuleFor(x => x.CurrencyCode)
            .MinimumLength(1).When(x => x.CurrencyCode != null)
            .MaximumLength(10).When(x => x.CurrencyCode != null);  

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).When(x => x.Amount.HasValue);  

        RuleFor(x => x.AmountType)
            .IsInEnum().When(x => x.AmountType.HasValue); 

        RuleFor(x => x.Days)
            .InclusiveBetween(0, 999).When(x => x.Days.HasValue);  

        RuleFor(x => x.Period)
            .IsInEnum().When(x => x.Period.HasValue);  

        RuleFor(x => x.Date)
            .Must(BeAValidDate).When(x => x.Date.HasValue).WithMessage("Date must be in a valid format");  

        RuleFor(x => x.Restrictions)
            .IsInEnum().When(x => x.Restrictions.HasValue);  
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date == null || date > DateTime.MinValue;
    }
}

// Validator for Policy
public class PolicyValidator : AbstractValidator<Policy>
{
    public PolicyValidator()
    {
        RuleFor(x => x.PolicyType)
                    .NotEmpty()
                    .IsInEnum(); 
        RuleFor(x => x.PolicyName)
                    .NotEmpty()
                    .Length(1, 100); 

        RuleFor(x => x.PolicyDescription)
                    .MinimumLength(1).When(x => x.PolicyDescription != null)
                    .MaximumLength(200).When(x => x.PolicyDescription != null);

        RuleFor(x => x.TermsAndConditions)
                    .MinimumLength(1).When(x => x.TermsAndConditions != null)
                    .MaximumLength(2000).When(x => x.TermsAndConditions != null);
        
        RuleFor(x => x.Refundable)
                    .NotNull().When(x => x.Refundable.HasValue);

        RuleForEach(x => x.PolicyDetails)
                    .SetValidator(new PolicyDetailValidator());
    }
}

// Validator for FlightSearchResponse
public class FlightSearchResponseValidator : AbstractValidator<FlightSearchResponse>
{
    public FlightSearchResponseValidator()
    {
                // Validate that the response contains offers
                RuleForEach(x => x.Offers).SetValidator(new OfferValidator());
    }
}
