namespace Application.Dtos;

public static class PaginationDtos
{
    public class PaginationDto
    {
        public long PerPage { get; set; }
        public long TotalPages { get; set; }
        public long Page { get; set; }
        public long Total { get; set; }
    }
}
