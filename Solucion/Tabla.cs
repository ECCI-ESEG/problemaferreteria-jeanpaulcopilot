using System;

namespace Solucion
{
  public class Tabla
  {
    private double ancho;
    private double largo;
    private double precioBase;
    public Tabla(double ancho, double largo, double precioBase)
    {
      if (this.ancho > this.largo)
      {
        throw new ArgumentException("El ancho no puede ser mayor que el largo");
      }
      this.ancho = ancho;
      this.largo = largo;
      this.precioBase = precioBase;
    }

    public double getAncho()
    {
      return ancho;
    }

    public double getLargo()
    {
      return largo;
    }

    public double getPrecioBase()
    {
      return precioBase;
    }

  }
}
