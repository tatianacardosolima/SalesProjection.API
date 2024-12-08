using SalesProjection.Domain.Abstractions;
using System.Reflection;

namespace SalesProjection.Domain.Entities
{
    public enum LotStatus
    { 
        Processing = 0,
        Success = 1,
        Error = 2,
    }

    public class ProcessLot: EntityBase
    {
        public ProcessLot(List<SaleMadeItem> saleMadeItem, string region, string branchName, string period)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Status = (int)LotStatus.Processing;
            SaleMadeItem = saleMadeItem;
            Region = region;
            BranchName = branchName;
            Period = period;
        }        
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public List<SaleMadeItem> SaleMadeItem { get; set; }
        public string Region { get; set; }
        public string BranchName { get; set; }
        public string  Period { get; set; }

    }

    public class SaleMadeItem : EntityBase
    {
        public SaleMadeItem()
        {
            Id = Guid.NewGuid();
        }

        public SaleMadeItem(DateTime period, int hour, double sale)
        {
            Id = Guid.NewGuid();
            Period = period;
            Hour = hour;
            Sale = sale;
        }

        public DateTime Period { get; set; }
        public int Hour { get; set; }
        public double Sale { get; set; }
    }
}
