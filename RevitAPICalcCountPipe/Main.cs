using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPICalcCountPipe
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var pipes = new FilteredElementCollector(doc, uidoc.ActiveView.Id)
               .OfClass(typeof(Pipe))
               .Cast<Pipe>()
               .ToList();

            List<ElementId> pipesId = new List<ElementId>();

            foreach (var item in pipes)
            {
                pipesId.Add(item.Id);
            }

            uidoc.Selection.SetElementIds(pipesId);

            TaskDialog.Show("Трубы", $"Количество труб {pipes.Count.ToString()} ");

            return Result.Succeeded;
        }
    }
}
