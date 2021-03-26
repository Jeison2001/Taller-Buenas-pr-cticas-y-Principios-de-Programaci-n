using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Entity;
using static BLL.LiquidacionCuotaModeradoraService;

namespace Presentacion
{
    class Program
    {
        static int numeroLiquidacion,dia,mes,ano;
        static String identificacion, afiliacion;
        static double salario, valorServicio;
        static DateTime fechaLiquidacion;

        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                switch (Menu())
                {
                    case 1:
                        RealizarLiquidacion();
                        break;
                    case 2:
                        ListarLiquidaciones();
                        break;
                    case 7:
                        EliminarLiquidacion();
                        break;
                    case 8:
                        opcion=8;
                        break;
                    default:

                        break;
                }
            } while (opcion != 8);
        }

        public static int Menu()
        {
            int opcion;

            do
            {
                Console.WriteLine("Menu de opciones");
                Console.WriteLine("1) Realizar liquidacion");
                Console.WriteLine("2) Listado de liquidaciones");
                Console.WriteLine("3) Consulta por tipo de afiliacion");
                Console.WriteLine("4) Consulta valor total de cuota");
                Console.WriteLine("5) Consulta por fecha");
                Console.WriteLine("6) Consulta por nombre");
                Console.WriteLine("7) Eliminar por Numero de liquidacion");
                Console.WriteLine("8) Salir");
                Console.Write("Opcion: ");
                opcion = Int32.Parse(Console.ReadLine());

            } while (opcion < 1 && opcion > 8);

            return opcion;
        }

        public static string RealizarLiquidacion()
        {   
            string respuesta="";
            Console.WriteLine("Registro liquidacion");
            Console.WriteLine("Digite el numero de liquidacion");
            numeroLiquidacion = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la identificacion del cliente");
            identificacion=Console.ReadLine();
            Console.WriteLine("Seleccione el tipo de Afiliacion del cliente");
            Console.WriteLine("1) Contributivo || 2) Subsidiado");
            do
            {
                afiliacion = Console.ReadLine();
            }while (!afiliacion.Equals("1") && !afiliacion.Equals("2"));           
            Console.WriteLine("Fecha liquidacion");
            Console.Write("Dia: ");
            dia = Int32.Parse(Console.ReadLine());
            Console.Write("  Mes: ");
            mes = Int32.Parse(Console.ReadLine());
            Console.Write("  año: ");
            ano = Int32.Parse(Console.ReadLine());
            fechaLiquidacion = new DateTime(ano, mes, dia);
            Console.Write("Salario: ");
            salario = Double.Parse(Console.ReadLine());
            Console.Write("Valor del servicio: ");
            valorServicio = Double.Parse(Console.ReadLine());
            LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
            
            if (afiliacion.Equals("2"))
            {
                afiliacion = "Subsidiado";
                Paciente paciente = new Paciente(identificacion, afiliacion, salario);
                Liquidacion liquidacion = new LiquidacionSubsidiado();
                liquidacion.numeroLiquidacion = numeroLiquidacion;
                liquidacion.valorServicio = valorServicio;
                liquidacion.paciente = paciente;
                liquidacion.fechaLiquidacion = fechaLiquidacion;
                liquidacion.tarifa= liquidacion.CalcularTarifa();
                liquidacion.topeMaximo= liquidacion.CalcularTope();
                liquidacion.cuotaModeradora = liquidacion.CalcularCuotaModeradora();
                liquidacionCuotaModeradoraService.Guardar(liquidacion);
            }
            else
            {
                Liquidacion liquidacion = new LiquidacionContributivo();
                afiliacion = "Contributivo";
                Paciente paciente = new Paciente(identificacion, afiliacion, salario);
                liquidacion.numeroLiquidacion = numeroLiquidacion;
                liquidacion.valorServicio = valorServicio;
                liquidacion.paciente = paciente;
                liquidacion.fechaLiquidacion = fechaLiquidacion;
                liquidacion.tarifa = liquidacion.CalcularTarifa();
                liquidacion.topeMaximo = liquidacion.CalcularTope();
                liquidacion.cuotaModeradora = liquidacion.CalcularCuotaModeradora();
                liquidacionCuotaModeradoraService.Guardar(liquidacion);
            }
            
            return respuesta;
        }

        public static string ListarLiquidaciones()
        {
            string respuesta = "";
            LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
            ConsultaResponse response = liquidacionCuotaModeradoraService.Consultar();
            if (!response.Error)
            {
                foreach (var item in response.Liquidaciones)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine(response.Mensaje);
            }
            Console.ReadKey();
            return respuesta;
        }

        public static string EliminarLiquidacion()
        {
            int numLiquidacion;
            string respuesta = "";
            LiquidacionCuotaModeradoraService liquidacionCuotaModeradoraService = new LiquidacionCuotaModeradoraService();
            Console.WriteLine("Digite el numero de liquidacion:");
            numLiquidacion = Int32.Parse(Console.ReadLine());
            liquidacionCuotaModeradoraService.Eliminar(numLiquidacion);
            return respuesta;
        }

    }
}