namespace Application.Library
{
    public static class DocumentModels
    {
        public enum DocumentType
        {
            Undefined = 0,
            Cpf = 1,
            Rg = 2,
            Pis = 3,
            Cnpj = 4
        }

        public class DocumentDto
        {
            public int Id { get; set; }
            public DocumentType Type { get; set; }
            public string Value { get; set; } = string.Empty;
        }
    }
}
