namespace WebApplication2.Cnfigurations;

public class JwtSettings
{
    public string Audience { get; set; }=string.Empty;
    public string Issuer { get; set; }=string.Empty;
    public string Key { get; set; }=string.Empty;
}