using System;
using System.Linq; 
using System.Collections.Generic; 

namespace Advantage.API
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int i, int len) // i for index and len for pageSize
        {
            // [2] page, 100 results
            Data = data.Skip((i - 1) * len).Take(len).ToList();
        }

        public int Total {get; set;}
        public IEnumerable<T> Data {get; set;}
    }
}