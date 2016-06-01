using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using System.Drawing;

namespace AlterStudio.Controllers
{
    [Authorize]
    public class AnalysisController : Controller
    {
        LocalStudioEntities _db = new LocalStudioEntities();
        public JsonResult GetServicesCostsJson()
        {
            var data = from d in _db.Services
                       select new
                       {
                           d.Title,
                           d.Cost
                       };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Graphics()
        {
            Highcharts chart = new Highcharts("chart")
                 .InitChart(new Chart { PlotBackgroundColor = null, PlotBorderWidth = null, PlotShadow = false })
                 .SetTitle(new Title { Text = "Услуги студии" })
                 .SetTooltip(new Tooltip { PointFormat = "{series.name}: <b>{point.percentage}%</b>" /*, percentageDecimals: 1*/ })
                 .SetPlotOptions(new PlotOptions
                 {
                     Pie = new PlotOptionsPie
                     {
                         AllowPointSelect = true,
                         Cursor = Cursors.Pointer,
                         DataLabels = new PlotOptionsPieDataLabels
                         {
                             Enabled = true,
                             Color = ColorTranslator.FromHtml("#000000"),
                             ConnectorColor = ColorTranslator.FromHtml("#000000"),
                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
                         }
                     }
                 })
                 .SetSeries(new Series
                 {
                     Type = ChartTypes.Pie,
                     Name = "Соотношение предоставляемых услуг",
                     Data = new Data(new object[]
                     {
                        new object[] { "Свадебная фотосъёмка", 35.0 },
                        new object[] { "Свадебная видеосъёмка", 26.8 },
                        new DotNet.Highcharts.Options.Point
                        {
                            Name = "Предметная съёмка",
                            Y = 12.8,
                            Sliced = true,
                            Selected = true
                        },
                        new object[] { "Выпускные фотокниги", 8.5 },
                        new object[] { "Рекламные ролики", 6.2 },
                        new object[] { "Другое", 10.7 }
                     })
                 });

            return View(chart);
        }
    }
}