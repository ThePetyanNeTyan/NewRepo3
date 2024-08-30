using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTrain.Domain.Result
{
    public class CollectionResuslt<T>: BaseResult<IEnumerable<T>>
    {
        public int Count { get; set; }
    }
}
