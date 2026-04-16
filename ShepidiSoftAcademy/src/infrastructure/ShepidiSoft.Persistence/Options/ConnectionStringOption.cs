namespace ShepidiSoft.Persistence.Options;

public sealed class ConnectionStringOption
{
    public const string Key = "ConnectionStrings";
    public string SqlServer { get; set; } = null!; 
}