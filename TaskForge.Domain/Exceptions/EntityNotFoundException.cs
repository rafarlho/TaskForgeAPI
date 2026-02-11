using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName, Guid id) : 
            base(errorCode:"ENTITY_NOT_FOUND", 
                message: $"{entityName} with id '{id}' was not found.",
                parameters: [entityName, id ]
            )
        {
        }
    }
}
