using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string LocationToNorth { get; set; }
        public string LocationToEast { get; set; }
        public string LocationToSouth { get; set; }
        public string LocationToWest { get; set; }
        public Location(string id, string name, string description, string locationtonorth, string locationtoeast, string locationtosouth, string locationtowest)
        {
            LocationID = id;
            LocationName = name;
            LocationDescription = description;
            LocationToNorth = locationtonorth;
            LocationToEast = locationtoeast;
            LocationToSouth = locationtosouth;
            LocationToWest = locationtowest;
        }
    }
}