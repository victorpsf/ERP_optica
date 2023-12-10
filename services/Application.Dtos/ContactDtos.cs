namespace Application.Dtos;

public class ContactDtos
{
    public enum ContactType
    {
        PHONE = 1,
        EMAIL = 2
    }

    public class ContactDto
    {
        public int Id { get; set; }
        public ContactType ContactType { get; set; }
        public int PersonId { get; set; }
        public string Value { get; set; } = string.Empty;
        public int Version { get; set; }
    }
}
