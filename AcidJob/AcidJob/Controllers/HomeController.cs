using AcidJob.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AcidJob.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {


            using (var client = new HttpClient())
            {
                //enviar request a web api REST usando HttpClient
                //paises
                HttpResponseMessage Res = await client.GetAsync("https://api.worldbank.org/countries?per_page=400&format=json");
                //verificar respuesta es correcta
                if (Res.IsSuccessStatusCode)
                {
                    //almaceno resultados
                    var json = Res.Content.ReadAsStringAsync().Result;


                    JArray data = JArray.Parse(json);
                    var results = data.Last.ToList();
                    List<Adminregion> paisList = new List<Adminregion>();
                    paisList.Add(new Adminregion() { Id = "All", Value = "Todos" });
                    foreach (JToken result in results)
                    {
                        // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                        var Adminregion = new Adminregion

                        {
                            Id = result["id"].ToString(),
                            Value = result["name"].ToString()
                        };


                        //añado datos al modelo Adminregion

                        paisList.Add(Adminregion);
                    }

                    var list = new SelectList(paisList, "Id", "Value");
                    ViewBag.paises = list;

                }

                //natalidad
                using (var client2 = new HttpClient())
                {
                    //enviar request a web api REST usando HttpClient
                    HttpResponseMessage Res2 = await client2.GetAsync("http://api.worldbank.org/v2/countries/chl/indicators/SP.DYN.CBRT.IN?date=2000:2001&per_page=20000&format=json");
                    //verificar respuesta es correcta
                    if (Res.IsSuccessStatusCode)
                    {
                        //almaceno resultados
                        var json = Res2.Content.ReadAsStringAsync().Result;
                        JArray data = JArray.Parse(json);
                        var results = data.Last.ToList();
                        List<Datos> natalidadList = new List<Datos>();
                        foreach (JToken result in results)
                        {
                            // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                            var Datos = new Datos { Countryiso3Code = result["countryiso3code"].ToString(), Date = result["date"].ToString() };
                            //añado datos al modelo datos
                            natalidadList.Add(Datos);

                        }

                        var list = natalidadList.ToList();
                        ViewBag.natalidad = list;

                    }
                    //mortalidad
                    using (var client3 = new HttpClient())
                    {
                        //enviar request a web api REST usando HttpClient
                        HttpResponseMessage Res3 = await client3.GetAsync("http://api.worldbank.org/v2/countries/chl/indicators/SP.DYN.CDRT.IN?date=2000:2001&per_page=20000&format=json");
                        //verificar respuesta es correcta
                        if (Res.IsSuccessStatusCode)
                        {
                            //almaceno resultados
                            var json = Res3.Content.ReadAsStringAsync().Result;
                            JArray data = JArray.Parse(json);
                            var results = data.Last.ToList();
                            List<Datos> mortalidadList = new List<Datos>();
                            foreach (JToken result in results)
                            {
                                // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                                var Datos = new Datos { Countryiso3Code = result["countryiso3code"].ToString(), Date = result["date"].ToString() };
                                //añado datos al modelo datos
                                mortalidadList.Add(Datos);

                            }

                            var list = mortalidadList.ToList();
                            ViewBag.mortalidad = list;

                        }
                        return View();
                    }
                }
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> IndexPost(FormCollection collection)
        {

            string F1 = (DateTime.Parse(collection["Fechas.Fecha"]).Year).ToString();
            string F2 = (DateTime.Parse(collection["Fechas.Fecha2"]).Year).ToString();
            //post de dropdown no trae datos
            string dpv = (collection["paises"].ToString());


            //api natalidad
            using (var client = new HttpClient())
            {
                //enviar request a web api REST usando HttpClient
                HttpResponseMessage Res = await client.GetAsync("http://api.worldbank.org/v2/countries/" + dpv + "/indicators/SP.DYN.CBRT.IN?date=" + F1 + ":" + F2 + "&per_page=20000&format=json");
                //verificar respuesta es correcta
                if (Res.IsSuccessStatusCode)
                {
                    //almaceno resultados
                    var json = Res.Content.ReadAsStringAsync().Result;
                    JArray data = JArray.Parse(json);
                    var results = data.Last.ToList();
                    List<Datos> natalidadList = new List<Datos>();
                    foreach (JToken result in results)
                    {
                        // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                        var Datos = new Datos { Countryiso3Code = result["countryiso3code"].ToString(), Date = result["date"].ToString() };
                        //añado datos al modelo datos
                        natalidadList.Add(Datos);

                    }

                    var list = natalidadList.ToList();
                    ViewBag.natalidad = list;
                }
                //api paises
                {
                    //enviar request a web api REST usando HttpClient
                    HttpResponseMessage Res2 = await client.GetAsync("https://api.worldbank.org/countries?per_page=400&format=json");
                    //verificar respuesta es correcta
                    if (Res2.IsSuccessStatusCode)
                    {
                        //almaceno resultados
                        var json = Res2.Content.ReadAsStringAsync().Result;


                        JArray data = JArray.Parse(json);
                        var results = data.Last.ToList();
                        List<Adminregion> paisList = new List<Adminregion>();
                        paisList.Add(new Adminregion() { Id = "All", Value = "Todos" });
                        foreach (JToken result in results)
                        {
                            // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                            var Adminregion = new Adminregion

                            {
                                Id = result["id"].ToString(),
                                Value = result["name"].ToString()
                            };


                            //añado datos al modelo Adminregion
                            paisList.Add(new Adminregion() { Id = "All", Value = "Todos" });
                            paisList.Add(Adminregion);
                        }

                        var list = new SelectList(paisList, "Id", "Value");
                        ViewBag.paises = list;

                    }

                    //api mortalidad
                    {
                        //enviar request a web api REST usando HttpClient
                        HttpResponseMessage Res3 = await client.GetAsync("http://api.worldbank.org/v2/countries/" + dpv + "/indicators/SP.DYN.CDRT.IN?date=" + F1 + ":" + F2 + "&per_page=20000&format=json");
                        //verificar respuesta es correcta
                        if (Res3.IsSuccessStatusCode)
                        {
                            //almaceno resultados
                            var json = Res3.Content.ReadAsStringAsync().Result;
                            JArray data = JArray.Parse(json);
                            var results = data.Last.ToList();
                            List<Datos> mortalidadList = new List<Datos>();
                            foreach (JToken result in results)
                            {
                                // uso JToken.ToObject que es un metodo de ayuda que serializa json internamente
                                var Datos = new Datos { Countryiso3Code = result["countryiso3code"].ToString(), Date = result["date"].ToString() };
                                //añado datos al modelo datos
                                mortalidadList.Add(Datos);

                            }

                            var list = mortalidadList.ToList();
                            ViewBag.mortalidad = list;
                        }
                        return View();
                    }

                }


            }

        }

    }
}



