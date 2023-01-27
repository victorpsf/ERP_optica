namespace Application.Library
{
    public static class ContactModels
    {
        public enum ContactType
        {
            Phone = 1,
            Email = 2,

            // redes sociais
            Twitter = 1001,
            Instagram = 1002,
            Facebook = 1003,
            Linkedin = 1004
        }

        public class ContactDto
        {
            public int Id { get; set; }
            public ContactType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }
    }
}
