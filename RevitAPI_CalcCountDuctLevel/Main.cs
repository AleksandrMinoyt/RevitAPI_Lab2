using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_CalcCountDuctLevel
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            var ducts = new FilteredElementCollector(doc)
            .OfClass(typeof(Duct))
            .Cast<Duct>()
            .ToList();

            List<ElementId> dutsId = new List<ElementId>();

            var ListLevel = new FilteredElementCollector(doc)
               .OfClass(typeof(Level))
               .OfType<Level>()
               .ToList();
            
            foreach (var item in ducts)
            {
               dutsId.Add(item.Id);
            }

            uidoc.Selection.SetElementIds(dutsId);

            string ResStr = string.Empty;

            foreach (var item in ListLevel)
            {
                var levelDuct = ducts.Where(x => x.ReferenceLevel.Id == item.Id).ToList();

                ResStr += $"Уровень {item.Name} содержит {levelDuct.Count.ToString()} воздуховодов" +Environment.NewLine;

            }


            TaskDialog.Show("Воздуховоды", $"Количество воздуховодов по уровням "+ ResStr);


            return Result.Succeeded;
        }
    }
}
