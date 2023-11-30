namespace PocketBook.Domain;

public class PageInfo
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; } 
    public int TotalPages => (int)Math.Ceiling(TotalItems * 1.0 / PageSize); 
}