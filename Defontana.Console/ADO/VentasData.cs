using System;
using System.Data;
using System.Data.SqlClient;

namespace Defontana.Console.ADO
{
    public class VentasData
    {
        public List<VentaEntity> GetVentas(int days)
        {
            List<VentaEntity> ventasList = new List<VentaEntity>();

            using (SqlConnection connection = new SqlConnection("Server=lab-defontana.caporvnn6sbh.us-east-1.rds.amazonaws.com;Database=Prueba;Trusted_connection=true;User Id=ReadOnly;Password=d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU;Integrated Security=false;TrustServerCertificate=true"))
            {
                try
                {
                    var query = $"Select d.ID_VentaDetalle, d.ID_Producto, p.Nombre As Producto, d.Cantidad, d.TotalLinea, v.ID_Venta, v.Fecha, v.Total, v.ID_Local, l.Nombre As Local, p.Nombre As Producto, p.ID_Marca, m.Nombre As Marca From VentaDetalle d Inner Join Venta v On d.ID_Venta = v.ID_Venta Inner Join [Local] l On v.ID_Local = l.ID_Local Inner Join Producto p On d.ID_Producto = p.ID_Producto Inner Join Marca m On p.ID_Marca = m.ID_Marca Where Fecha Between DATEADD(DAY, -{days}, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date)";
                    connection.Open();
                    SqlCommand command = new SqlCommand()
                    {
                        CommandText = query,
                        CommandType = CommandType.Text,
                        Connection = connection
                    };

                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            VentaEntity entity = new VentaEntity()
                            {
                                ID_VentaDetalle = int.Parse(reader["ID_VentaDetalle"].ToString()),
                                ID_Producto = int.Parse(reader["ID_Producto"].ToString()),
                                Producto = reader["Producto"].ToString(),
                                Cantidad = int.Parse(reader["Cantidad"].ToString()),
                                TotalLinea = int.Parse(reader["TotalLinea"].ToString()),
                                ID_Venta = int.Parse(reader["ID_Venta"].ToString()),
                                Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                                Total = int.Parse(reader["Total"].ToString()),
                                ID_Local = int.Parse(reader["ID_Local"].ToString()),
                                Local = reader["Local"].ToString(),
                                ID_Marca = int.Parse(reader["ID_Marca"].ToString()),
                                Marca = reader["Marca"].ToString()
                            };

                            ventasList.Add(entity);
                        }
                    }

                    return ventasList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

