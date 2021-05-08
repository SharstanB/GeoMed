using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Base
{
     public class OperationResult<T>
    {
        public T Result { get; set; }

        public IEnumerable<T> IEnumerableResult { get; set; }

        public OperationResultTypes OperationResultType { get; set; }

        public Exception Exception { get; set; }

        public string OperationResultMessage { get; set; }

        public bool Issuccess => OperationResultType == OperationResultTypes.Success;
    }
}
