namespace Application.Dtos;

public static class AddressDtos
{
    public enum AddressType
    {
        PostalCode = 1,
        Nation = 2,
        State = 3,
        City = 4,
        Streat = 5,
        HouseNumber = 6,
        Complement = 7
    }

    public class AddressDto
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        public int PersonId { get; set; }
        public string Value { get; set; } = string.Empty;
        public int Version { get; set; }
    }
}
