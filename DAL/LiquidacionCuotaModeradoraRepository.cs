using Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LiquidacionCuotaModeradoraRepository
    {
        static string ruta = "Liquidacion.txt";


        public Liquidacion BuscarPorNumeroLiquidacion(int numeroLiquidacion)
        {

            foreach (var item in Consultar())
            {
                if (item.numeroLiquidacion.Equals(numeroLiquidacion))
                {
                    return item;
                }
            }
            return null;
        }

        public void Guardar(Liquidacion liquidacion)
        {
            FileStream file = new FileStream(ruta, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);
            escritor.WriteLine($"{liquidacion.numeroLiquidacion};{liquidacion.valorServicio};{liquidacion.paciente.identificacion};{liquidacion.paciente.afiliacion};{liquidacion.paciente.salario};{liquidacion.fechaLiquidacion};{liquidacion.tarifa};{liquidacion.topeMaximo};{liquidacion.cuotaModeradora}");
            escritor.Close();
            file.Close();
        }

        public static List<Liquidacion> Consultar()
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();
            FileStream file = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader lector = new StreamReader(file);
            string linea = "";
            while ((linea = lector.ReadLine()) != null)
            {
                Liquidacion liquidacion = MapearLiquidacion(linea);
                liquidaciones.Add(liquidacion);
            }
            lector.Close();
            file.Close();
            return liquidaciones;
        }
        private static Liquidacion MapearLiquidacion(string linea)
        {
                
            string[] datosLiquidacion = linea.Split(';');
          
            if (datosLiquidacion[3].Equals("Subsidiado"))
            {
                Liquidacion liquidacion = new LiquidacionSubsidiado()
                {
                    numeroLiquidacion = Int32.Parse(datosLiquidacion[0]),
                    valorServicio = Double.Parse(datosLiquidacion[1]),
                    paciente = new Paciente(datosLiquidacion[2], datosLiquidacion[3], Double.Parse(datosLiquidacion[4])),
                    fechaLiquidacion = DateTime.Parse(datosLiquidacion[5]),
                    tarifa = Double.Parse(datosLiquidacion[6]),
                    topeMaximo = Double.Parse(datosLiquidacion[7]),
                    cuotaModeradora = Double.Parse(datosLiquidacion[8])
                };
                return liquidacion;
            }
            else
            {
                Liquidacion liquidacion = new LiquidacionContributivo()
                {
                    numeroLiquidacion = Int32.Parse(datosLiquidacion[0]),
                    valorServicio = Double.Parse(datosLiquidacion[1]),
                    paciente = new Paciente(datosLiquidacion[2], datosLiquidacion[3], Double.Parse(datosLiquidacion[4])),
                    fechaLiquidacion = DateTime.Parse(datosLiquidacion[5]),
                    tarifa = Double.Parse(datosLiquidacion[6]),
                    topeMaximo = Double.Parse(datosLiquidacion[7]),
                    cuotaModeradora = Double.Parse(datosLiquidacion[8])
                };
                return liquidacion;
            }
        }

        public void Eliminar(int numeroLiquidacion)
        {
            List<Liquidacion> liquidaciones = Consultar();
            FileStream file = new FileStream(ruta, FileMode.Create);
            file.Close();
            foreach (var item in liquidaciones)
            {
                if (!item.numeroLiquidacion.Equals(numeroLiquidacion))
                {
                    Guardar(item);
                }
            }
        }


    }
}
