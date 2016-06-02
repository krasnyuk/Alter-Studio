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
        public ActionResult SalesByTime()
        {

            IEnumerable<int> data = from c in _db.Clients
                            join o in _db.Orders on c.ClientId equals o.ClientId into g
                            select g.Count();

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
                .SetTitle(new Title { Text = "Количество заказов клиентов" })
                .SetSubtitle(new Subtitle { Text = "" })
                .SetXAxis(new XAxis
                {
                    Categories = _db.Clients.Select(x=>x.FirstName + " " + x.LastName).ToArray(),
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    TickInterval = 1,
                    Title = new YAxisTitle
                    {
                        Text = "Количество заказов",
                        Align = AxisTitleAligns.High
                    }
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' '; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -100,
                    Y = 100,
                    Floating = true,
                    BorderWidth = 1,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Количество заказов", Data = new Data(data.Cast<object>().ToArray())}
                });

            return View(chart);
           
        }
        public ActionResult CuratorsOrdersCount()
        {
            IEnumerable<int> data = from c in _db.Curators
                                    join o in _db.Orders on c.CuratorId equals o.CuratorId into g
                                    select g.Count();

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
                .SetTitle(new Title { Text = "Количество заказов у кураторов" })
                .SetSubtitle(new Subtitle { Text = "Рейтинг кураторов по количеству заказов" })
                .SetXAxis(new XAxis
                {
                    Categories = _db.Curators.Select(x => x.FirstName + " " + x.LastName).ToArray(),
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    TickInterval = 1,
                    Title = new YAxisTitle
                    {
                        Text = "Количество",
                        Align = AxisTitleAligns.High
                    }
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' '; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -100,
                    Y = 100,
                    Floating = true,
                    BorderWidth = 1,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Количество заказов", Data = new Data(data.Cast<object>().ToArray())}
                });

            return View(chart);
        }
        public ActionResult EmployeesOrdersCount()
        {
            IEnumerable<int> data = from c in _db.Employees
                                    join o in _db.OrderDetails on c.EmployeeId equals o.EmployeeId into g
                                    select g.Count();

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
                .SetTitle(new Title { Text = "Рейтинг сотрудников" })
                .SetSubtitle(new Subtitle { Text = "Количество заказов" })
                .SetXAxis(new XAxis
                {
                    Categories = _db.Employees.Select(x => x.FirstName + " " + x.LastName).ToArray(),
                    Title = new XAxisTitle { Text = string.Empty }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    TickInterval = 1,
                    Title = new YAxisTitle
                    {
                        Text = "Количество",
                        Align = AxisTitleAligns.High
                    }
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' '; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Right,
                    VerticalAlign = VerticalAligns.Top,
                    X = -100,
                    Y = 100,
                    Floating = true,
                    BorderWidth = 1,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Name = "Количество заказов", Data = new Data(data.Cast<object>().ToArray())}
                });

            return View(chart);
        }
        

    }
}