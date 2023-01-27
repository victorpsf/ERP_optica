namespace Web.ApiM2.Controller.Models
{
    public static class PersonPhysicalModels
    {
        public class CreatePersonPhysical
        {
            public string Name { get; set; } = string.Empty;
            public DateTime? BirthDate { get; set; }
        }
    }
}
