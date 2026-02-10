using System.ComponentModel.DataAnnotations;

namespace TaskForge.Domain.Entities;

public class Organization : BaseEntity
{
    public required string Name {get; set;}
}
