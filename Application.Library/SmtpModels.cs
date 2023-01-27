namespace Application.Library
{
  public static class SmtpModels
  {
    public class SendModel
    {
      public string Subject { get; set; } = string.Empty;
      public bool IsHtml { get; set; }
      public string Body { get; set; } = string.Empty;

      public string[] To { get; set; } = Array.Empty<string>();
    }
  }
}