using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException() : base("خطا در ثبت اطلاعات")
        {
        }
    }
}
