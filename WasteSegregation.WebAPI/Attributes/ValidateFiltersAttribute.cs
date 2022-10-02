namespace WasteSegregation.WebAPI.Attributes;

public class ValidateFiltersAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        base.OnResultExecuting(context);

        if(!context.ModelState.IsValid)
        {
            var entry = context.ModelState.Values.FirstOrDefault();
            context.Result = new BadRequestObjectResult(new Response<bool>
            {
                Message = "Something went wrong.",
                Errors = entry.Errors.Select(x => x.ErrorMessage)
            });
        }
    }
}
