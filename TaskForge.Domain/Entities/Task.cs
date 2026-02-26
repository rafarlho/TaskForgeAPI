using System;
using System.Collections.Generic;
using System.Text;
using TaskForge.Domain.Enums;

namespace TaskForge.Domain.Entities
{
    public class Task : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? StationName { get; set; }
        public string? Assignee { get; set; }
        public Guid TaskGroupId { get; set; }
        public Status Status { get; set; } = Status.NOTSTARTED;
        public virtual TaskGroup TaskGroup { get; set; } = null!;
    }
}
