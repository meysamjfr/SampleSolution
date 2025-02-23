﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }

        public ValidationException(List<string> errors)
        {
            Errors = errors;
        }
    }
}
