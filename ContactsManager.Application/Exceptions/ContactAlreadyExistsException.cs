using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application.Exceptions
{
    public class ContactAlreadyExistsException : Exception
    {
        public ContactAlreadyExistsException(string error) : base(error) 
        {

        }
    }
}
