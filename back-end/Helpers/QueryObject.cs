namespace back_end.Helpers;

// Essa classe serve pra paginação das movimentações
public class QueryObject
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}