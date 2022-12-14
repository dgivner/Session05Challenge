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

        public static FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string typeName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol familySymbolName in collector)
            {
                if (familySymbolName.Name == typeName && familySymbolName.FamilyName == familyName)
                    return familySymbolName;
            }

            return null;
        }


        public static string GetParameterValueAsString(Element currentElement, string paramName)
        {
            string returnValue = "";

            IList<Parameter> paramList = currentElement.GetParameters(paramName);

            //paramList coming up null
            Parameter myParameter = paramList.First();

            returnValue = myParameter.AsString();

            return returnValue;

        }
        public static string GetParameterValueByName(Element element, string paramName)
        {
            IList<Parameter> paramList = element.GetParameters(paramName);

            if (paramList != null)
            {
                Parameter param = paramList[0];
                string paramValue = param.AsString();
                return paramValue;
            }

            return "";
        }

        public static void SetParameterByName(Element element, string paramName, int value)
        {
            IList<Parameter> paramList = element.GetParameters(paramName);

            if (paramList != null)
            {
                Parameter param = paramList[0];
                param.Set(value);
            }
        }

        public static FamilySymbol GetFurnitureTypes(Document doc, object furniture)
        {

            //todo create method to get family types

            FilteredElementCollector familySymbolCollector = new FilteredElementCollector(doc);
            familySymbolCollector.OfClass(typeof(FamilySymbol));
            ICollection<ElementId> furnitureId = familySymbolCollector.ToElementIds();

            foreach (FamilySymbol currentSymbol in familySymbolCollector)
            {
                if (currentSymbol.Name == furniture)
                    return currentSymbol;
            }

            return null;
            
        }

       

        public static List<FamilyParameter> GetParameters()
        {
            List<FamilyParameter> returnList = new List<FamilyParameter>();
            return returnList;
        }

        public static List<FamilyInstanceCreationData> FamilyInstances(XYZ roomPoint, FamilySymbol familySymbol,
            Level level, StructuralType nonStructural = StructuralType.NonStructural)
        {
            var list = new List<FamilyInstanceCreationData>();
            list.Add(new FamilyInstanceCreationData(roomPoint, familySymbol, level, nonStructural));

            //todo add create family instances
            /*FamilyInstance newFamilyA = FamilyInstances(roomPoint, familySymbol, level, nonStructural)*/
            ;

            return list;
        }

    }

}

