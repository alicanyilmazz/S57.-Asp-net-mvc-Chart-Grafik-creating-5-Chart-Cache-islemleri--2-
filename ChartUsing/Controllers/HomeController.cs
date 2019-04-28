using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ChartUsing.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChartColumn(string tip = "Line")  //Chart tipini parametreye bagladık böylece dışarıdan tipe ne verirsek  ona göre grafik olusacak!
        {

            Chart chart = Chart.GetFromCache("chart1"); // "GetFromCache" ile eger ilgili Chart Cache de varsa "chart" değişkenine atacak yoksa null dönecektir.

            if (chart == null) //Chart yoksa onu yeniden üretecek
            {
                chart = new Chart(500, 500);

                chart.AddTitle("MyComputer Salesman Graphic");
                chart.AddLegend("Products");
                chart.AddSeries(name: "Computer A", chartType: tip, xValue: new[] { 20, 40, 60 }, yValues: new[] { 800, 1200, 2300 });
                chart.AddSeries(name: "Computer B", chartType: tip, xValue: new[] { 20, 40, 60 }, yValues: new[] { 900, 1600, 3300 });

                string dir = Server.MapPath("~/charts/");

                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }

                string imagePath = dir + "chart1.jpeg";
                string xmlPath = dir + "chart1.xml";

                chart.Save(imagePath, format: "jpeg");
                chart.SaveXml(xmlPath);
                chart.SaveToCache("chart1", 10, true); // 2.parametre kac dk cache de kalacak ,3. parametre Expiration yani 10 dk dolmadan page refresh edilirse true ise süreyi basa sarar false ise sarmaz 10dk dolunca siler direkt.

            }


            return View(chart); 
        }
    }
}
/*
 if (chart == null) //Chart yoksa onu yeniden üretecek
            {}

    olayı su aslında chart varsa zaten,

     Chart chart = Chart.GetFromCache("chart1"); // "GetFromCache" ile eger ilgili Chart Cache de varsa "chart" değişkenine atacak yoksa null dönecektir.

    kısımında alınan Chart verisi "chart" degiskenine atanacak ve 

     return View(chart);  ile sayfaya gönderilecek eger Cache de Chart verisi yoksa,

    if sartına girilecek ve Chart üretilip "chart" değişkenine atılacak ve bu da 

    return View(chart);  ile sayfaya gönderilecek

     */
     /*DEBUG yapıncanca ilk calıstırırken Cache de  olmadıgından if e girer DEBUG modda sonrasında sayfayı birdaha refresh edersen bu sefer if e görmediğini Cache den okudugunu göreceksin.*/