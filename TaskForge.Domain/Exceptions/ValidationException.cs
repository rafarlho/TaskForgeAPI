using System;
using System.Collections.Generic;
using System.Text;

namespace TaskForge.Domain.Exceptions;

public class ValidationException : DomainException
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base(
            errorCode: "VALIDATION_ERROR",
            message: "One or more validation errors occurred.",
            parameters: Array.Empty<object>())
    {
        Errors = errors;
    }

    public ValidationException(string field, string error)
        : base(
            errorCode: "VALIDATION_ERROR",
            message: error,
            parameters: new object[] { field })
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, new[] { error } }
        };
    }
}
