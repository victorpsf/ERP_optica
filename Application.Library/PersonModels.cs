namespace Application.Library
{
    public static class PersonModels
    {
        public enum PersonType
        {
            Physical = 1,
            Juridical = 2
        }

        public class PersonDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public List<DocumentModels.DocumentDto> Documents { get; set; } = new List<DocumentModels.DocumentDto>();
            public List<ContactModels.ContactDto> Contacts { get; set; } = new List<ContactModels.ContactDto>();
            public List<AddressModels.AddressDto> Addresses { get; set; } = new List<AddressModels.AddressDto>();
        }

        public class PersonPhysicalDto : PersonDto
        {
            public DateTime BirthDate { get; set; }
        }

        public class PersonJuridicalDto : PersonDto
        { }
    }
}
