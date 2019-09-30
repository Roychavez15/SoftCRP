using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data;
using SoftCRP.Web.Data.Entities;
using SoftCRP.Web.Models;
using SWDLCondelpi;
namespace SoftCRP.Web.Controllers
{

    //[Authorize(Roles = "Manager")]
    [Authorize]
    public class ClientesController : Controller
    {
        

        private readonly DataContext _context;
        private readonly WSDLCondelpiData.Service1Soap _service1Soap;

        public ClientesController(
            DataContext context,
            WSDLCondelpiData.Service1Soap service1Soap)
        {
            _context = context;
            _service1Soap = service1Soap;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var dataxml = await _service1Soap.Consulta_Data_autoAsync("1791287835001", "pcu1955");
            //WSDLCondelpiData.ArrayOfXElement arrayOfXElement = new WSDLCondelpiData.ArrayOfXElement();
            //XDocument document = XDocument.Load(dataxml.Nodes[0].ToString());

            //var booksQuery = from b in document.Elements("NewDataSet").Elements("data1")
            //                 select b;

            //DataTable table = booksQuery.ToDataTable();
            XmlDocument document = new XmlDocument();

            document.LoadXml(dataxml.Nodes[1].ToString());
            XmlNodeList Datos = document.GetElementsByTagName("NewDataSet");

            if (Datos.Count > 0)
            {
                var InfoAutos = ToDatosAuto(Datos);                
            }
           

            return View(await _context.Clientes.ToListAsync());
        }

        private DatosAuto ToDatosAuto(XmlNodeList Datos)
        {
            return new DatosAuto
            {
                Adendum= ((XmlElement)Datos[0]).GetElementsByTagName("adendum")[0].InnerText,
                Año= ((XmlElement)Datos[0]).GetElementsByTagName("año")[0].InnerText,
                Canon = ((XmlElement)Datos[0]).GetElementsByTagName("canon")[0].InnerText,
                Chasis = ((XmlElement)Datos[0]).GetElementsByTagName("chasis")[0].InnerText,
                Ciudad_operacion = ((XmlElement)Datos[0]).GetElementsByTagName("ciudad_operacion")[0].InnerText,
                Clase = ((XmlElement)Datos[0]).GetElementsByTagName("clase")[0].InnerText,
                Cliente = ((XmlElement)Datos[0]).GetElementsByTagName("cliente")[0].InnerText,
                Color = ((XmlElement)Datos[0]).GetElementsByTagName("color")[0].InnerText,
                Contrato = ((XmlElement)Datos[0]).GetElementsByTagName("contrato")[0].InnerText,
                Cotizacion = ((XmlElement)Datos[0]).GetElementsByTagName("cotizacion")[0].InnerText,
                Des_modelo = ((XmlElement)Datos[0]).GetElementsByTagName("des_modelo")[0].InnerText,
                Dispositivo = ((XmlElement)Datos[0]).GetElementsByTagName("dispositivo")[0].InnerText,
                Ejecutivo = ((XmlElement)Datos[0]).GetElementsByTagName("ejecutivo")[0].InnerText,
                Estatus = ((XmlElement)Datos[0]).GetElementsByTagName("estatus")[0].InnerText,
                Fechacontrato = ((XmlElement)Datos[0]).GetElementsByTagName("fechacontrato")[0].InnerText,
                FechafinContrato = ((XmlElement)Datos[0]).GetElementsByTagName("fechafinContrato")[0].InnerText,
                Fecha_entrega = ((XmlElement)Datos[0]).GetElementsByTagName("fecha_entrega")[0].InnerText,
                Fecha_km = ((XmlElement)Datos[0]).GetElementsByTagName("fecha_km")[0].InnerText,
                Fecha_ultima_rutina = ((XmlElement)Datos[0]).GetElementsByTagName("fecha_ultima_rutina")[0].InnerText,
                FormaFacturacion = ((XmlElement)Datos[0]).GetElementsByTagName("FormaFacturacion")[0].InnerText,
                
                Id_ultima_rutina = ((XmlElement)Datos[0]).GetElementsByTagName("id_ultima_rutina")[0].InnerText,
                Km = ((XmlElement)Datos[0]).GetElementsByTagName("km")[0].InnerText,
                KmAnual = ((XmlElement)Datos[0]).GetElementsByTagName("KmAnual")[0].InnerText,
                Marca = ((XmlElement)Datos[0]).GetElementsByTagName("marca")[0].InnerText,
                Modelo = ((XmlElement)Datos[0]).GetElementsByTagName("modelo")[0].InnerText,
                Motor = ((XmlElement)Datos[0]).GetElementsByTagName("motor")[0].InnerText,
                Mto_correctivo = ((XmlElement)Datos[0]).GetElementsByTagName("mto_correctivo")[0].InnerText,
                Mto_llantas = ((XmlElement)Datos[0]).GetElementsByTagName("mto_llantas")[0].InnerText,
                Mto_preventivo = ((XmlElement)Datos[0]).GetElementsByTagName("mto_preventivo")[0].InnerText,
                Mto_sustituto = ((XmlElement)Datos[0]).GetElementsByTagName("mto_sustituto")[0].InnerText,
                NombreAseguradora = ((XmlElement)Datos[0]).GetElementsByTagName("NombreAseguradora")[0].InnerText,
                Nom_cliente = ((XmlElement)Datos[0]).GetElementsByTagName("nom_cliente")[0].InnerText,
                Nom_ejecutivo = ((XmlElement)Datos[0]).GetElementsByTagName("nom_ejecutivo")[0].InnerText,
                Placa = ((XmlElement)Datos[0]).GetElementsByTagName("placa")[0].InnerText,
                Plan_Seguro = ((XmlElement)Datos[0]).GetElementsByTagName("Plan_Seguro")[0].InnerText,
                Plazo = ((XmlElement)Datos[0]).GetElementsByTagName("Plazo")[0].InnerText,
                Plazo_pago = ((XmlElement)Datos[0]).GetElementsByTagName("Plazo_pago")[0].InnerText,
                Ramv = ((XmlElement)Datos[0]).GetElementsByTagName("ramv")[0].InnerText,
                Siniestros = ((XmlElement)Datos[0]).GetElementsByTagName("siniestros")[0].InnerText,
                Ultima_rutina = ((XmlElement)Datos[0]).GetElementsByTagName("ultima_rutina")[0].InnerText,
            };
        }
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Cedula")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Cedula")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
