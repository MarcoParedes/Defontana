using System;
namespace Defontana.Console.ADO
{
    public class VentaEntity
    {
        public long ID_VentaDetalle { get; set; }
        public long ID_Producto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public int TotalLinea { get; set; }
        public long ID_Venta { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public long ID_Local { get; set; }
        public string Local { get; set; }
        public long ID_Marca { get; set; }
        public string Marca { get; set; }
    }
}

