using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hsec.Application.Common.Models
{
    public class GeneralCollection<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
        public GeneralCollection() { }
        public GeneralCollection(IEnumerable<T> data) : this(data, data.Count()) { }
        public GeneralCollection(IEnumerable<T> data, int count)
        {
            this.Data = data;
            this.Count = count;
        }
    }
}
