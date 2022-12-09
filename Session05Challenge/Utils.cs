using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Document = Autodesk.Revit.DB.Document;

namespace Session05Challenge
{
    internal static class Utils
    {
        
        public static FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string familySymbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Family));

            foreach (Family family in collector)
            {
                if (family.Name == familyName)
                {
                    foreach (ElementId id in family.GetFamilySymbolIds())
                    {
                        FamilySymbol curFS = doc.GetElement(id) as FamilySymbol;

                        if (curFS.Name == familySymbolName)
                            return curFS;
                    }
                }
            }
            return null;
        }

        public static void SetParameterValue(Element currentElement, string paraName, string paramValue)
        {
            IList<Parameter> paramList = currentElement.GetParameters(paraName);

            foreach (Parameter param in paramList)
            {
                param.Set(paramValue);
            }
        }

        public static string GetParameterValueAsString(Element currentElement, string paramName)
        {
            string returnValue = "";

            IList<Parameter> paramList = currentElement.GetParameters(paramName);

            Parameter myParameter = paramList.First();

            returnValue = myParameter.AsString();

            return returnValue;
            
        }

        public static List<string[]> GetFurnitureTypes()
        {

            List<string[]> returnList = new List<string[]>();
            returnList.Add(new string[] { "Furniture Name", "Revit Family Name", "Revit Family Type" });
            returnList.Add(new string[] { "desk", "Desk", "60in x 30in" });
            returnList.Add(new string[] { "task chair", "Chair-Task", "Chair-Task" });
            returnList.Add(new string[] { "side chair", "Chair-Breuer", "Chair-Breuer" });
            returnList.Add(new string[] { "bookcase", "Shelving", "96in x 12in x 84in" });
            returnList.Add(new string[] { "loveseat", "Sofa", "54in" });
            returnList.Add(new string[] { "teacher desk", "Table-Rectangular", "48in x 30in" });
            returnList.Add(new string[] { "student desk", "Desk", "60in x 30in Student" });
            returnList.Add(new string[] { "computer desk", "Table-Rectangular", "48in x 30in" });
            returnList.Add(new string[] { "lab desk", "Table-Rectangular", "72in x 30in" });
            returnList.Add(new string[] { "lounge chair", "Chair-Corbu", "Chair-Corbu" });
            returnList.Add(new string[] { "coffee table", "Table-Coffee", "30in x 60in x 18in" });
            returnList.Add(new string[] { "sofa", "Sofa-Corbu", "Sofa-Corbu" });
            returnList.Add(new string[] { "dining table", "Table-Dining", "30in x 84in x 22in" });
            returnList.Add(new string[] { "dining chair", "Chair-Breuer", "Chair-Breuer" });
            returnList.Add(new string[] { "stool", "Chair-Task", "Chair-Task" });

            return returnList;
        }

        public static List<furnitureSetStruct> GetFurnitureSets()
        {
            List<furnitureSetStruct> returnList = new List<furnitureSetStruct>();

            //returnList.Add(new furnitureSetStruct() {= "Furniture Set", "Room Type", "Included Furniture" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet= "A", RoomType = "Office", furnitureArray = "desk, task chair, side chair, bookcase" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "A2", RoomType = "Office", furnitureArray = "desk, task chair, side chair, bookcase, loveseat" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "B", RoomType = "Classroom - Large", furnitureArray = "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "B2", RoomType = "Classroom - Medium", furnitureArray = "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "C", RoomType = "Computer Lab", furnitureArray = "computer desk, computer desk, computer desk, computer desk, computer desk, computer desk, task chair, task chair, task chair, task chair, task chair, task chair" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "D", RoomType = "Lab",furnitureArray = "teacher desk, task chair, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, stool, stool, stool, stool, stool, stool, stool" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "E",RoomType = "Student Lounge", furnitureArray = "lounge chair, lounge chair, lounge chair, sofa, coffee table, bookcase" });
            returnList.Add(new furnitureSetStruct() {FurnitureSet = "F", RoomType = "Teacher's Lounge",furnitureArray = "lounge chair, lounge chair, sofa, coffee table, dining table, dining chair, dining chair, dining chair, dining chair, bookcase" });
            returnList.Add(new furnitureSetStruct() { FurnitureSet = "G", RoomType = "Waiting Room", furnitureArray = "lounge chair, lounge chair, sofa, coffee table" });

            return returnList;
        }
        
        public struct furnitureSetStruct
        {
            public string FurnitureSet;
            public string RoomType;
            public string furnitureArray;

            public List<string> IncludedFurniture
            {
                get 
                { 
                    var list = new List<string>();
                    foreach (string item in furnitureArray.Split(new char[] {','}))
                    {
                        list.Add(item); 

                    } 
                    return list;
                }
            }

        }


        }
        
        public struct furnitureTypesStruct
        {
            public string FurnitureName;
            public string FamilyName;
            public string FamilyTipe;
        }
        public List<FamilyInstanceCreationData> FamilyInstances(XYZ roomPoint, FamilySymbol familySymbol, Level level, StructuralType nonStructural = StructuralType.NonStructural)
        {
            var list = new List<FamilyInstanceCreationData>();
            list.Add(new FamilyInstanceCreationData());

            //todo add create family instances

            return list;
        }


        public static void NewFamilyInstances2(Document  doc, List<FamilyInstanceCreationData> dataList, Level level)
        {
            {
                List<FamilyInstanceCreationData> familyCreationData = new List<FamilyInstanceCreationData>();

                ICollection<ElementId> elementSet = null;

                FamilySymbol familySymbol = null;
                FilteredElementCollector collector = new FilteredElementCollector(doc, elementSet);
                ICollection<Element> collection = collector.OfClass(typeof(FamilySymbol)).ToElements();
                foreach (Element element in collection)
                {
                    familySymbol = element as FamilySymbol;
                    elementSet.Add(element.Id);
                    
                }

                if (null != familySymbol)
                {
                    for (int i = 1; i < 11; i++)
                        XYZ location = new XYZ(i * 10, 100, 0);
                    FamilyInstanceCreationData fiCreationData =
                        new FamilyInstanceCreationData(location, familySymbol, level, StructuralType.NonStructural);

                    if (null != familyCreationData)
                    {
                        familyCreationData.Add(fiCreationData);
                    }
                }

                if (familyCreationData.Count > 0)
                {
                    elementSet = doc.Create.NewFamilyInstances2(familyCreationData);
                }
                else
                {
                    throw new Exception("Batch creation failed.");
                }
            }
            else
            {
                throw new Exception("No family types found.")
            }
            return elementSet;
            
        }
        
    }
}

