using static Application.Base.Models.BasePatternsModels;

namespace Application.Dtos;

public static class DocumentDtos
{
    public enum DocumentType
    {
        CPF = 1,
        RG = 2,
        CNH = 3,
        PIS = 4,
        CTPS = 5,
        CNPJ = 6,
        MEI = 7
    }

    public class Document: BaseDto
    {
        public int Id { get; set; }
        public DocumentType DocumentType { get; set; }
        public int PersonId { get; set; }
        public string Value { get; set; } = string.Empty;
        public int Version { get; set; }
    }
}
