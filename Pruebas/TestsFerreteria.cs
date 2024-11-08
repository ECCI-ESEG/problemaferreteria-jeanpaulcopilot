using NUnit.Framework;
using Solucion;

namespace Pruebas;

[TestFixture]
public class TestsFerreteria
{
    public record TestFerreteria(double anchoInicial, double largoInicial, double precioBase, List<TestTabla> tablas);
    public record TestTabla(double anchoSolicitado, double largoSolicitado, double resultadoEsperado);
    
    public static IEnumerable<TestFerreteria> GetTestCases()
    {
        // Caso normal.
        List<TestTabla> tablas = new List<TestTabla>();
        tablas.Add(new TestTabla(50, 50, 25000));
        yield return new TestFerreteria(100, 100, 10, tablas);

        // Caso donde ningun residual de la primera tabla cumpla con las condiciones y se tenga que recurrir a una estandar.
        tablas.Clear();
        tablas.Add(new TestTabla(101, 101, -1));
        tablas.Add(new TestTabla(101, 100, -1));
        tablas.Add(new TestTabla(100, 101, -1));
        yield return new TestFerreteria(100, 100, 10, tablas);
        
        // Caso donde un residual cumpla con la solicitud.
        tablas.Clear();
        tablas.Add(new TestTabla(50, 50, 25000));
        tablas.Add(new TestTabla(50, 50, 25000));
        yield return new TestFerreteria(100, 100, 10, tablas);
        
        // Caso donde ningun residual ni la estandar cumpla con la solicitud.
        tablas.Clear();
        tablas.Add(new TestTabla(70, 70, 49000));
        tablas.Add(new TestTabla(50, 40, 20000));
        yield return new TestFerreteria(100, 100, 10, tablas);
        
        // Caso donde la tabla es del mismo tamano que la estandar
        tablas.Clear();
        tablas.Add(new TestTabla(100, 100, 100000));
        tablas.Add(new TestTabla(50, 50, 25000));
        yield return new TestFerreteria(100, 100, 10, tablas);
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void TestProductPriceAfterDiscount(TestFerreteria test)
    {
        // Arrange
        var ferreteria = new Ferreteria(test.anchoInicial, test.largoInicial, test.precioBase);
        foreach(var solicitud in test.tablas)
        {
            var result = ferreteria.ProcesarSolicitud(solicitud.anchoSolicitado, solicitud.largoSolicitado);
            Assert.That(solicitud.resultadoEsperado, Is.EqualTo(result));
        }    
    }

}
