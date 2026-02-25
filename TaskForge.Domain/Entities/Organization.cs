using System.ComponentModel.DataAnnotations;

namespace TaskForge.Domain.Entities;

public class Organization : BaseEntity
{
    public required string Name {get; set;}
    public virtual ICollection<TaskGroup> TaskGroups { get; set; } = new List<TaskGroup>();
}
