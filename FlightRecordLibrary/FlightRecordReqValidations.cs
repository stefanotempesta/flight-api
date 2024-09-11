using FluentValidation;
    public class CarrierValidator : AbstractValidator<Carrier>
{
    public CarrierValidator()
    {
        RuleFor(x => x.CarrierCode)
            .MinimumLength(1).When(x => x.CarrierCode != null)
            .MaximumLength(10).When(x => x.CarrierCode != null);  
    }
}

// Validator for Route
public class RouteValidator : AbstractValidator<Route>
{
    public RouteValidator()
    {
        RuleFor(x => x.OriginCode)
            .NotEmpty()
            .Length(1, 10);  

        RuleFor(x => x.DestinationCode)
            .NotEmpty()
            .Length(1, 10); 

        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(BeAValidDate).WithMessage("Date must be in a valid format")  
            .Must(BeFutureDate).WithMessage("Date must be in the future");  

        RuleFor(x => x.PreferredTime)
            .IsInEnum().When(x => x.PreferredTime.HasValue);  
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }

    private bool BeFutureDate(string date)
    {
        return DateTime.TryParse(date, out var parsedDate) && parsedDate >= DateTime.Today;
    }
}

// Validator for PaxDetail
public class PaxDetailValidator : AbstractValidator<PaxDetail>
{
    public PaxDetailValidator()
    {
        RuleFor(x => x.PaxNo)
            .GreaterThanOrEqualTo(0).When(x => x.PaxNo.HasValue)
            .LessThanOrEqualTo(999).When(x => x.PaxNo.HasValue);

        RuleFor(x => x.PaxType)
            .IsInEnum().When(x => x.PaxType.HasValue);

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).When(x => x.Age.HasValue)
            .LessThanOrEqualTo(20).When(x => x.Age.HasValue);

        RuleFor(x => x.DateOfBirth)
            .Must(BeAValidDate).When(x => !string.IsNullOrEmpty(x.DateOfBirth))  
            .Must(BePastDate).WithMessage("Date of Birth must be in the past");  
    }

    private bool BeAValidDate(string? date)
    {
        if (date == null) return true;
        return DateTime.TryParse(date, out _);
    }

    private bool BePastDate(string? date)
    {
        return DateTime.TryParse(date, out var parsedDate) && parsedDate <= DateTime.Today;
    }
}

// Validator for FlightSearchRequest
public class FlightSearchRequestValidator : AbstractValidator<FlightSearchRequest>
{
    public FlightSearchRequestValidator()
    {
        RuleFor(x => x.CompanyId)
            .NotEmpty()
            .Length(1, 36);

        RuleFor(x => x.CabinClass)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.JourneyType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Route)
            .SetValidator(new RouteValidator());

        RuleFor(x => x.PartnerId)
            .MinimumLength(1).When(x => x.PartnerId != null)
            .MaximumLength(20).When(x => x.PartnerId != null);

        RuleFor(x => x.SearchId)
            .MinimumLength(1).When(x => x.PartnerId != null)
            .MaximumLength(36).When(x => x.SearchId != null);

        RuleForEach(x => x.Carriers)
            .SetValidator(new CarrierValidator());

        RuleFor(x => x.CustomerId)
            .MinimumLength(1).When(x => x.CustomerId != null)
            .MaximumLength(20).When(x => x.CustomerId != null);


        RuleFor(x => x.CampaignId)
            .MinimumLength(1).When(x => x.CampaignId != null)
            .MaximumLength(20).When(x => x.CampaignId != null);


        RuleFor(x => x.CurrencyCode)
            .MinimumLength(1).When(x => x.CurrencyCode != null)
            .MaximumLength(10).When(x => x.CurrencyCode != null);


        RuleForEach(x => x.PaxDetails)
            .SetValidator(new PaxDetailValidator());

       
        RuleFor(x => x.AvailableOnly)
            .NotNull().When(x => x.AvailableOnly.HasValue);
    }
}