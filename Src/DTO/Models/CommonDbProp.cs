using System;

namespace DTO.Models
{
    public class CommonDbProp
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
    public class FailureModel : CommonDbProp
    {
        public int RetryCount { get; set; } = 0;
        public string LogMessage { get; set; }
    }
    public class CommonDbPropWithFailureModel : FailureModel
    {
    }
}