// See https://aka.ms/new-console-template for more information
using System.Linq;
using Defontana.Console.ADO;

var ventasData = new VentasData();
var data = ventasData.GetVentas(30);


Console.WriteLine($"El total de ventas de los últimos 30 días (monto total y Q de ventas).");
var totalVentas = data.Sum(x => x.TotalLinea);
var ventasList = data.GroupBy(x => x.ID_Venta)
                .Select(item => new
                {
                    ID_Local = item.FirstOrDefault()?.ID_Local,
                    Local = item.FirstOrDefault()?.Local,
                    Total = item.FirstOrDefault()?.Total
                }).ToList();

Console.WriteLine($"Monto total {totalVentas}  Q de ventas: {ventasList.Count}.");



Console.WriteLine($"El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto)");
var masAlta = data.OrderByDescending(d => d.Total)
    .Take(1)
    .Select(item => new {
        Fecha = item.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
        item.Total
    }).FirstOrDefault();
Console.WriteLine($"Fecha: {masAlta?.Fecha}  Monto: {masAlta?.Total}");



Console.WriteLine($"Indicar cuál es el producto con mayor monto total de ventas.");
var productoMayorMonto = data.GroupBy(x => x.ID_Producto)
        .Select(item => new
        {
            Producto = item.FirstOrDefault()?.Producto,
            Total = item.Sum(x => x.TotalLinea)
        })
        .OrderByDescending(x => x.Total)
        .Take(1)
        .FirstOrDefault();

Console.WriteLine($"Producto: {productoMayorMonto?.Producto}");



Console.WriteLine($"Indicar el local con mayor monto de ventas.");

var localMayorMonto = ventasList.GroupBy(x => x.ID_Local)
        .Select(item => new
        {
            Local = item.FirstOrDefault()?.Local,
            Total = item.Sum(x => x.Total)
        })
        .OrderByDescending(x => x.Total)
        .Take(1)
        .FirstOrDefault();
Console.WriteLine($"Local: {localMayorMonto?.Local}");



Console.WriteLine($"¿Cuál es la marca con mayor margen de ganancias?");
var marcaGanancias = data.GroupBy(x => x.ID_Marca)
        .Select(item => new
        {
            Marca = item.FirstOrDefault()?.Marca,
            Total = item.Sum(x => x.TotalLinea)
        })
        .OrderByDescending(x => x.Total)
        .Take(1)
        .FirstOrDefault();
Console.WriteLine($"Marca: {marcaGanancias?.Marca}");



Console.WriteLine($"¿Cómo obtendrías cuál es el producto que más se vende en cada local?");
var productoLocal = data.GroupBy(x => x.ID_Local)
        .OrderBy(x => x.Key)
        .Select(item => new
        {
            Local = item.FirstOrDefault()?.Local,
            Producto = data
                .Where(x => x.ID_Local == item.Key)
                .GroupBy(x => x.ID_Producto)
                .Select(item => new
                {
                    Producto = item.FirstOrDefault()?.Producto,
                    Cantidad = item.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(1)
                .FirstOrDefault()
                ?.Producto ?? ""
         }).ToList();

productoLocal.ForEach(item =>
{
    Console.WriteLine($"Local: {item.Local}  Producto: {item.Producto}");
});
