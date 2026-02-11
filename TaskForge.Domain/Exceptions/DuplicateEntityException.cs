using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Domain.Exceptions
{
    public class DuplicateEntityException : DomainException
    {
        public DuplicateEntityException(
            string entityName, string fieldName, object fieldValue
        ) : base(
            errorCode: "DUPLICATE_ENTITY",
            message: $"A {entityName} with {fieldName} '{fieldValue}' already exists.",
            parameters: [entityName, fieldName, fieldValue]
        )
        {}
    }
}
