
Use Prueba
Go

-- El total de ventas de los últimos 30 días (monto total y Q de ventas).
Select Sum(Total) As [Monto Total], COUNT(ID_Venta) As [Cantidad de Ventas]
From Venta
Where Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date) 

-- El día y hora en que se realizó la venta con el monto más alto (y cuál es aquel monto).
Select Top 1 Fecha, Total 
From Venta
Where Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date) 
Order By Total Desc

-- Indicar cuál es el producto con mayor monto total de ventas.
Select Top 1 d.ID_Producto, p.Nombre As Producto
From VentaDetalle d
    Inner Join Venta v On d.ID_Venta = v.ID_Venta
    Inner Join Producto p On d.ID_Producto = p.ID_Producto
Where v.Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date) 
Group By d.ID_Producto, p.Nombre
Order By Sum(TotalLinea) Desc


-- Indicar el local con mayor monto de ventas.
Select Top 1 v.ID_Local, l.Nombre As [Local]
From Venta v
    Inner Join Local l On v.ID_Local = l.ID_Local
Where v.Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date) 
Group By v.ID_Local, l.Nombre
Order By Sum(Total) Desc

-- ¿Cuál es la marca con mayor margen de ganancias?
Select Top 1 m.ID_Marca, m.Nombre As Marca
From VentaDetalle d
    Inner Join Venta v On d.ID_Venta = v.ID_Venta
    Inner Join Producto p On d.ID_Producto = p.ID_Producto
    Inner Join Marca m On p.ID_Marca = m.ID_Marca
Where v.Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date) 
Group By m.ID_Marca, m.Nombre
Order By Sum(d.TotalLinea) Desc


-- ¿Cómo obtendrías cuál es el producto que más se vende en cada local?
Select 
    ID_Local, Nombre As [Local],
    (
        Select Top 1 p.Nombre
        From Venta v
            Inner Join [Local] l On v.ID_Local = l.ID_Local
            Inner Join VentaDetalle d On v.ID_Venta = d.ID_Venta
            Inner Join Producto p On d.ID_Producto = p.ID_Producto
        Where v.Fecha Between DATEADD(DAY, -30, CAST(GETDATE() As Date)) And CAST(GETDATE() As Date)
            And v.ID_Local = [Local].ID_Local
        Group By v.ID_Local, l.Nombre, d.ID_Producto, p.Nombre
        Order By Sum(d.Cantidad) desc
    ) As Producto
From [Local]
