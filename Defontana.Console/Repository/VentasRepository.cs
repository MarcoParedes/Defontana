using System;
using Defontana.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace Defontana.Console.Repository
{
    public class VentasRepository
    {
        public List<Ventum> GetVentas(int days)
        {
            using var context = new PruebaContext();

            var date = DateTime.Now;
            var now = new DateTime(date.Year, date.Month, date.Day);
            return context.Venta
                .Include(x => x.VentaDetalles)
                .Include(x => x.IdLocalNavigation)
                .Include("VentaDetalles.IdProductoNavigation")
                .Include("VentaDetalles.IdProductoNavigation.IdMarcaNavigation")
                .Where(x => x.Fecha >= now.AddDays(-days) && x.Fecha <= now)
                .ToList();
        }
    }
}

