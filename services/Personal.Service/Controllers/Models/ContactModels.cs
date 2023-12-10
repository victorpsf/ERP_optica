using Application.Validations;
using static Application.Dtos.ContactDtos;

namespace Personal.Service.Controllers.Models;

public static class ContactModels
{
    public class ContactInput
    {
        [EnumValidation(Enumerable = typeof(ContactType), Required = true)]
        public ContactType ContactType { get; set; }

        [StringValidation(Required = true)]
        public string Value { get; set; } = string.Empty;
    }
}
