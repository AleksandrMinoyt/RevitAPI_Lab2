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

namespace RevitAPI_CountColumn
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            ElementCategoryFilter ecf_c = new ElementCategoryFilter(BuiltInCategory.OST_Columns);
            ElementCategoryFilter ecf_sc = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
         
            LogicalOrFilter lof = new LogicalOrFilter(ecf_sc, ecf_c);

            var columns = new FilteredElementCollector(doc, uidoc.ActiveView.Id)
            .WherePasses(lof)
            .WhereElementIsNotElementType()
            .ToList();


            List<ElementId> columnsId = new List<ElementId>();

            foreach (var item in columns)
            {
                columnsId.Add(item.Id);
            }

            uidoc.Selection.SetElementIds(columnsId);
            TaskDialog.Show("Колонны", $"Количество колонн {columns.Count.ToString()} ");
            return Result.Succeeded;
        }
    }
}
