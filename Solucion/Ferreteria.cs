using System;

namespace Solucion
{
  public class Ferreteria
  {
    private double anchoInicial;
    private double largoInicial;
    private double precioBase;

    private Almacen almacen;

    public Ferreteria(double anchoInicial, double largoInicial, double precioBase)
    {
      this.anchoInicial = anchoInicial;
      this.largoInicial = largoInicial;
      this.precioBase = precioBase;
      this.almacen = new Almacen(500);
    }


    public double ProcesarSolicitud(double anchoSolicitado, double largoSolicitado)
    {
      if (anchoSolicitado > anchoInicial || largoSolicitado > largoInicial)
      {
        return -1;
      }

      Tabla tabla = almacen.ObtenerTabla(anchoSolicitado, largoSolicitado);

      if (tabla == null)
      {
        tabla = new Tabla(anchoInicial, largoInicial, precioBase);
      }
      else
      {
        almacen.EliminarTabla(tabla);
      }


      return CortarTabla(tabla, anchoSolicitado, largoSolicitado);

    }

    // Funcion que corta una tabla en línea recta 2 veces hasta obtener
    // la tabla solicitada, al cortar en vertical (ancho), se obtendrán 2 tablas
    // una de esas es la "restante" y la otra es parte de la solicitada. Luego, despues de cortar en horizontal (largo)
    // se obtendra la tabla solicitada y la tabla "residual". Sin embargo,
    // antes de hacer un corte definitivo, se comparan los dos cortes posibles
    // y se elige el que genere el menor residuo.
    public double CortarTabla(Tabla tabla, double anchoSolicitado, double largoSolicitado)
    {
      // Primer corte: decidir entre ancho y largo
      Tabla? primerRestante = null;
      Tabla? segundaRestante = null;


      Tabla restanteAncho = new Tabla(tabla.getAncho() - anchoSolicitado, tabla.getLargo(), tabla.getPrecioBase());
      Tabla restanteLargo = new Tabla(anchoSolicitado, tabla.getLargo() - largoSolicitado, tabla.getPrecioBase());

      double areaRestanteAncho = restanteAncho.getAncho() * restanteAncho.getLargo();
      double areaRestanteLargo = restanteLargo.getAncho() * restanteLargo.getLargo();

      if (areaRestanteAncho < areaRestanteLargo)
      {
        // Si se llega aqui, el primer corte es en anchura entonces
        primerRestante = restanteAncho;

        // Segundo corte en el largo sobre la tabla restante del primer corte
        if (tabla.getLargo() > largoSolicitado)
        {
          segundaRestante = new Tabla(anchoSolicitado, tabla.getLargo() - largoSolicitado, tabla.getPrecioBase());
        }
      }
      else
      {
        // Caso contrario, si se llega aqui, el primer corte es en largo
        primerRestante = restanteLargo;

        // Segundo corte en el ancho sobre la tabla restante del primer corte
        if (tabla.getAncho() > anchoSolicitado)
        {
          segundaRestante = new Tabla(tabla.getAncho() - anchoSolicitado, largoSolicitado, tabla.getPrecioBase());
        }
      }

      // Agregar la tabla restante del primer corte al almacén si es válida
      if (primerRestante != null && primerRestante.getAncho() > 0 && primerRestante.getLargo() > 0)
      {
        almacen.AgregarTabla(primerRestante);
      }

      // Agregar la tabla restante del segundo corte al almacén si es válida
      if (segundaRestante != null && segundaRestante.getAncho() > 0 && segundaRestante.getLargo() > 0)
      {
        almacen.AgregarTabla(segundaRestante);
      }

      return precioBase * anchoSolicitado * largoSolicitado;
    }



  }
}