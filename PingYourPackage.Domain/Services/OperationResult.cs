using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain.Services
{
    public class OperationResult
    {
        public OperationResult(bool isSuccsess)
        {
            IsSuccsess = isSuccsess;
        }
        public bool IsSuccsess { get; set; }
    }

    public class OperationResult<TEntyty> : OperationResult
    {
        public TEntyty Entity;
        public OperationResult(bool isSuccsess) : base(isSuccsess) { }
    }

}
