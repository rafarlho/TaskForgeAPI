using System;
using System.Collections.Generic;
using System.Text;
using TaskForge.Domain.Enums;

namespace TaskForge.Domain.Entities
{
    public class TaskGroup : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid OrganizationId { get; set; }
        public Status Status { get; set; } = TaskForge.Domain.Enums.Status.NOTSTARTED;
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
        public virtual Organization Organization { get; set; } = null;
    }
}
