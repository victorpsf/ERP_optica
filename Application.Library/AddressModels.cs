namespace Application.Library
{
    public static class AddressModels
    {
        public enum AddressType
        {
            Country = 1,
            State = 2,
            City = 3,
            PostalCode = 4,
            Street = 5,
            HouseNumber = 6,
            Complement = 7
        }

        public class AddressDto
        {
            public int Id { get; set; }
            public AddressType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }
    }
}
