using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
