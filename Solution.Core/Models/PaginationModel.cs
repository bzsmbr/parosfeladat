namespace Solution.Core.Models;

public class PaginationModel<T>
{
    public List<T> Items { get; set; }
    public int Count { get; set; }
}
