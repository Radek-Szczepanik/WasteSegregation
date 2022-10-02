namespace WasteSegregation.Application.Validators;

public class CreateRealEstateDtoValidator : AbstractValidator<CreateRealEstateDto>
{
	public CreateRealEstateDtoValidator()
	{
        #region Street
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
		RuleFor(x => x.Street).Length(3, 30).WithMessage("Length must be between 3 and 30 characters.");
        #endregion

        #region StreetNumber
        RuleFor(x => x.StreetNumber).NotEmpty().WithMessage("Street number is required.");
        RuleFor(x => x.StreetNumber).MaximumLength(10).WithMessage("Maximum length must be 10 characters.");
        #endregion

        #region PostCode
        RuleFor(x => x.PostCode).NotEmpty().WithMessage("Postcode is required.");
        RuleFor(x => x.PostCode).Length(6).WithMessage("Length must be 6 characters.");
        #endregion

        #region
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.City).Length(3, 20).WithMessage("Length must be between 3 and 20 characters.");
        #endregion
    }
}
