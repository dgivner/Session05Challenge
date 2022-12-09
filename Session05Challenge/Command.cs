#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.DB.Structure;


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

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);

            List<Utils.furnitureSetStruct> returnList = new List<Utils.furnitureSetStruct>();

            //todo Create structs for families

            //todo Create symbols from structs

            //todo List of family instance creation data 

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Insert Family");
                foreach (SpatialElement room in collector)
                {
                    SpatialElementBoundaryOptions options = new SpatialElementBoundaryOptions();

                    Location loc = room.Location;
                    LocationPoint locPoint = loc as LocationPoint;
                    XYZ roomPoint = locPoint.Point;

                    FamilySymbol myFS = Utils.GetFamilySymbolByName(doc, "", "");
                    FamilyInstance myInstance =
                        doc.Create.NewFamilyInstance(roomPoint, myFS, StructuralType.NonStructural);

                    
                    string furnitureSet = Utils.GetParameterValueAsString(room, "Furniture Sets");
                    string roomName = room.Name;

                }

                tx.Commit();
            }

            return Result.Succeeded;
        }

    }
}
