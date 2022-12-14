using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Architecture;

namespace Session05Challenge
{
    internal class FurnitureSet
    {
        public string Set { get; set; }
        public string RoomType { get; set; }
        public string[] Furniture { get; set; }

        public FurnitureSet(string furnitureSet, string roomType, string furnitureList)
        {
            Set = furnitureSet;
            RoomType = roomType;
            Furniture = furnitureList.Split(',');

        }
    }
    
    internal class FurnitureType
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Type { get; set; }

        public FurnitureType(string name, string family, string type)
        {
            Name = name;
            Family = family;
            Type = type;
        }
    }
}
