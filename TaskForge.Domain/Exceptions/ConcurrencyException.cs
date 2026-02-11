using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Domain.Exceptions;

public class ConcurrencyException : DomainException
{
    public ConcurrencyException(string entityName, Guid id)
        : base(
            errorCode: "CONCURRENCY_CONFLICT",
            message: $"The {entityName} with id '{id}' was modified by another user. Please refresh and try again.",
            parameters: [entityName, id])
    {
    }
}