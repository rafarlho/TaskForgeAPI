using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Domain.Entities
{
    public class TaskGroup : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; } = null!;
    }
}
