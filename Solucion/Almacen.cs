using System;

namespace Solucion
{
  public class Almacen
  {
    private int capacidadMaxima;
    private List<Tabla> tablas;
    public Almacen(int capacidadMaxima)
    {
      this.capacidadMaxima = capacidadMaxima;
      this.tablas = new List<Tabla>();
    }

    public void AgregarTabla(Tabla tabla)
    {
      if (tablas.Count < capacidadMaxima)
      {
        tablas.Add(tabla);
      }
    }

    // Ordenar tablas de menor a mayor ancho, y para las tablas con mismo ancho
    // de menor a mayor largo.
    public void OrdenarTablas()
    {
      tablas.Sort((tabla1, tabla2) =>
      {
        if (tabla1.getAncho() == tabla2.getAncho())
        {
          return tabla1.getLargo().CompareTo(tabla2.getLargo());
        }
        return tabla1.getAncho().CompareTo(tabla2.getAncho());
      });
    }

    // Obtener la tabla que genere el menor residuo posible basado en el corte
    // solicitado
    public Tabla ObtenerTabla(double anchoSolicitado, double largoSolicitado)
    {
      foreach (var tabla in tablas)
      {
        if (tabla.getAncho() >= anchoSolicitado && tabla.getLargo() >= largoSolicitado)
        {
          return tabla;
        }
      }
      return null;
    }

    // Eliminar una tabla de la lista
    public void EliminarTabla(Tabla tabla)
    {
      tablas.Remove(tabla);
    }






  }
}
