using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Common.EntityDTO
{
    public class CollectionResponse<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }

        public CollectionResponse() { }

        public CollectionResponse(List<T> data) : this(data, data.Count) { }

        public CollectionResponse(List<T> data, int count)
        {
            this.Data = data;
            this.Count = count;
        }
    }
}
