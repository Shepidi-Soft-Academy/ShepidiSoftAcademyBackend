using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain;

public sealed class DailyApplicationStat:BaseEntity<int>
{
    public DateTime Date { get; set; }  
    public int ApplicationCount { get; set; }
}
