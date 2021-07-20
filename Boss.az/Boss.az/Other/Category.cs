using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss.az.CategoryNS
{
    class Category
    {
        public uint Id { get; set; }
        public static uint ID { get; set; }
        public string Name { get; set; }
        public Category(string name)
        {
            Id = ++ID;
            Name = name;
        }
    }
}
