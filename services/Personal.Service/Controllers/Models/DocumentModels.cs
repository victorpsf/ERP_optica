using Application.Validations;
using static Application.Dtos.DocumentDtos;

namespace Personal.Service.Controllers.Models;

public static class DocumentModels
{
    public class DocumentInput
    {
        [EnumValidation(Required = true, Enumerable = typeof(DocumentType))]
        public DocumentType DocumentType { get; set; }
        [StringValidation(Required = true, MinLength = 4, MaxLength = 250)]
        public string Value { get; set; } = string.Empty;
    }
}
