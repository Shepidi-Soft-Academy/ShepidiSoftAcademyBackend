namespace ShepidiSoft.Persistence.Options;

public sealed class ConnectionStringOption
{
    public const string Key = "ConnectionStrings";
    public string Npgsql { get; set; } = null!; 
}