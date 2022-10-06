namespace WasteSegregation.WebAPI.Wrappers;

public class PageResponse<T> : Response<T>
{
    public T Data { get; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool NextPage { get; set; }
    public bool PreviousPage { get; set; }

    public PageResponse(T data, int pageNumber, int pageSize)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Succeeded = true;
    }
}
