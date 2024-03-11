using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Results.Bases
{
    public abstract class Result
    {
        public bool IsSuccessful { get;}
        public string Message { get;}

        protected Result(bool isSuccesfull, string message) 
        {
            IsSuccessful = isSuccesfull;
            Message = message;
        }
    }
}
