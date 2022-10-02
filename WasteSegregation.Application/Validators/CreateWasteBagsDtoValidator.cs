namespace WasteSegregation.Application.Validators;

public class CreateWasteBagsDtoValidator : AbstractValidator<CreateWasteBagsDto>
{
	public CreateWasteBagsDtoValidator()
	{
		#region Waste bags
		RuleFor(x => x.BlueBag).LessThan(20).WithMessage("Value must be less than 20");
		RuleFor(x => x.GreenBag).LessThan(20).WithMessage("Value must be less than 20");
        RuleFor(x => x.YellowBag).LessThan(20).WithMessage("Value must be less than 20");
        RuleFor(x => x.BrownBag).LessThan(20).WithMessage("Value must be less than 20");
        #endregion
    }
}
