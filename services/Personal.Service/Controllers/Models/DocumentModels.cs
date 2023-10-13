using Application.Validations;
using static Application.Dtos.DocumentDtos;

namespace Personal.Service.Controllers.Models;

public static class DocumentModels
{
    public class DocumentInput
    {
        [EnumValidation(Enumerable = typeof(DocumentType), Required = true)]
        public DocumentType DocumentType { get; set; }

        [StringValidation(Required = true)]
        public string Value { get; set; } = string.Empty;
    }
}
