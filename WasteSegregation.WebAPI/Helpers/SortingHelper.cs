namespace WasteSegregation.WebAPI.Helpers;

public class SortingHelper
{
    public static KeyValuePair<string, string>[] GetSortFields()
    {
        return new[] { SortFields.Street }; 
    }
}

public class SortFields
{
    public static KeyValuePair<string, string> Street { get; } = new KeyValuePair<string, string>("street", "Street");
}