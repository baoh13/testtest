using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class InvalidUser: Exception
    {
        public InvalidUser(): base("User does not match with CustomerId")
        {

        }
    }
}
