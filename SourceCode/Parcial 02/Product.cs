namespace Parcial_02
{
    public class Product
    {
        public decimal idProducto { get; set;}
        public string nombre { get; set;}
        public decimal stock_ingresado { get; set;}
        public decimal stock_actual { get; set;}
        public decimal precio_unidad_compra { get; set;}
        public decimal precio_unidad_venta { get; set;}
        public decimal cantidad_vendida { get; set;}
        public decimal precio_compra_acumulado { get; set;}
        public decimal precio_venta_acumulado { get; set;}
        public decimal ganancia { get; set;}
        

        public Product()
        {
            idProducto = 0;
            nombre = "";
            stock_ingresado = 0;
            stock_actual = 0;
            precio_unidad_compra = 0;
            precio_unidad_venta = 0;
            cantidad_vendida = 0;
            precio_compra_acumulado = 0;
            precio_venta_acumulado = 0;
            ganancia = 0;
            
        }
    }
}