#region Namespaces

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Document = Autodesk.Revit.DB.Document;

#endregion

namespace Session05Challenge
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            
            List<string[]> furnitureTypes = GetFurnitureTypes();
            List<string[]> furnitureSets = GetFurnitureSets();

            List<FurnitureType> furnitureTypeList = new List<FurnitureType>();
            List<FurnitureSet> furnitureSetList = new List<FurnitureSet>();

            foreach (string[] furnitureType in furnitureTypes)
            {
                FurnitureType currentType = new FurnitureType(furnitureType[0], furnitureType[1], furnitureType[2]);
                furnitureTypeList.Add(currentType);
            }

            foreach (string[] furnitureSet in furnitureSets)
            {
                FurnitureSet currentSet = new FurnitureSet(furnitureSet[0], furnitureSet[1], furnitureSet[2]);
                furnitureSetList.Add(currentSet);
            }

            furnitureTypeList.RemoveAt(0);
            furnitureSetList.RemoveAt(0);

            int totalCount = 0;

            FilteredElementCollector roomCollector = new FilteredElementCollector(doc);
            roomCollector.OfCategory(BuiltInCategory.OST_Rooms);

            
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Insert Families");
                
                foreach (SpatialElement room in roomCollector)
                {
                    int counter = 0;
                    string furnitureSet = Utils.GetParameterValueByName(room, "Furniture Set");
                    Debug.Print(furnitureSet);

                    Location loc = room.Location;
                    LocationPoint locPoint = loc as LocationPoint;
                    XYZ roomPoint = locPoint.Point;

                    foreach (FurnitureSet curSet in furnitureSetList)
                    {
                        if (curSet.Set == furnitureSet)
                        {
                            foreach (string furniture in curSet.Furniture)
                            {
                                foreach (FurnitureType curType in furnitureTypeList)
                                {
                                    
                                    if (curType.Name == furniture.Trim())
                                    {
                                        FamilySymbol curFamilySymbol =
                                            Utils.GetFamilySymbolByName(doc, curType.Family, curType.Type);

                                        if (curFamilySymbol.IsActive == false)
                                            curFamilySymbol.Activate();

                                        FamilyInstance instance = doc.Create.NewFamilyInstance(roomPoint,
                                            curFamilySymbol, StructuralType.NonStructural);
                                        counter++;
                                        totalCount++;
                                    }
                                }
                            }
                        }
                    }
                    Utils.SetParameterByName(room, "Furniture Count", counter);
                }
                tx.Commit();
            }

            TaskDialog.Show("Complete", "Inserted " + totalCount.ToString() + " pieces of furniture.");
            return Result.Succeeded;
        }
        public List<string[]> GetFurnitureSets()
        {
            List<string[]> returnList = new List<string[]>();
            returnList.Add(new string[] { "Furniture Set", "Room Type", "Included Furniture" });
            returnList.Add(new string[] { "A", "Office", "desk, task chair, side chair, bookcase" });
            returnList.Add(new string[] { "A2", "Office", "desk, task chair, side chair, bookcase, loveseat" });
            returnList.Add(new string[] { "B", "Classroom - Large", "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
            returnList.Add(new string[] { "B2", "Classroom - Medium", "teacher desk, task chair, student desk, student desk, student desk, student desk, student desk, student desk, student desk, student desk" });
            returnList.Add(new string[] { "C", "Computer Lab", "computer desk, computer desk, computer desk, computer desk, computer desk, computer desk, task chair, task chair, task chair, task chair, task chair, task chair" });
            returnList.Add(new string[] { "D", "Lab", "teacher desk, task chair, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, lab desk, stool, stool, stool, stool, stool, stool, stool" });
            returnList.Add(new string[] { "E", "Student Lounge", "lounge chair, lounge chair, lounge chair, sofa, coffee table, bookcase" });
            returnList.Add(new string[] { "F", "Teacher's Lounge", "lounge chair, lounge chair, sofa, coffee table, dining table, dining chair, dining chair, dining chair, dining chair, bookcase" });
            returnList.Add(new string[] { "G", "Waiting Room", "lounge chair, lounge chair, sofa, coffee table" });

            return returnList;
        }
        public List<string[]> GetFurnitureTypes()
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
    }
}
