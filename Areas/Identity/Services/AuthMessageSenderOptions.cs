namespace EnterpriseWeb.Areas.Identity.Services;

public class AuthMessageSenderOptions
{
    public string? SendGridKey { get; set; } = 
            Environment.GetEnvironmentVariable("SendGridKey");
}