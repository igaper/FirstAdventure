using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public Item(int id, string name, string nameplural, int minvalue, int maxvalue)
        {
            ItemID = id;
            Name = name;
            NamePlural = nameplural;
            MinValue = minvalue;
            MaxValue = maxvalue;
        }
    }
}