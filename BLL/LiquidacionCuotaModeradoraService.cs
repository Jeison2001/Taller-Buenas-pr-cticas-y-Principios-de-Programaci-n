using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LiquidacionCuotaModeradoraService
    {
        LiquidacionCuotaModeradoraRepository liquidacionCuotaModeradoraRepository;
        public LiquidacionCuotaModeradoraService()
        {
            liquidacionCuotaModeradoraRepository = new LiquidacionCuotaModeradoraRepository ();
        }

        public string Guardar(Liquidacion liquidacion)
        {

            try
            {
                if (liquidacionCuotaModeradoraRepository.BuscarPorNumeroLiquidacion(liquidacion.numeroLiquidacion) == null)
                {
                    liquidacionCuotaModeradoraRepository.Guardar(liquidacion);
                    return "Datos Guardados Satisfactoriamente";
                }
                return $"El numero de liquidacion {liquidacion.numeroLiquidacion} ya se encuentra registrado";

            }
            catch (Exception exception)
            {

                return "Se presentó el siguiente error:" + exception.Message;
            }
        }

        public class ConsultaResponse
        {
            public List<Liquidacion> Liquidaciones { get; set; }
            public string Mensaje { get; set; }
            public bool Error { get; set; }

            public ConsultaResponse(List<Liquidacion> liquidacionesas)
            {
                Liquidaciones = liquidacionesas;
                Error = false;
            }

            public ConsultaResponse(string mensaje)
            {
                Mensaje = mensaje;
                Error = true;
            }

        }
        public ConsultaResponse Consultar()
        {
            try
            {
                return new ConsultaResponse(LiquidacionCuotaModeradoraRepository.Consultar());
            }
            catch (Exception exception)
            {
                return new ConsultaResponse("Se presentó el siguiente error:" + exception.Message);
            }
        }

        public string Eliminar(int numeroLiquidacion)
        {

            try
            {
                if (liquidacionCuotaModeradoraRepository.BuscarPorNumeroLiquidacion(numeroLiquidacion) != null)
                {
                    liquidacionCuotaModeradoraRepository.Eliminar(numeroLiquidacion);
                    return $"Se eliminó a la Persona con idnetificacion {numeroLiquidacion}";
                }
                return $"No se encontró a la persona con Identificacion {numeroLiquidacion}";
            }
            catch (Exception exception)
            {

                return "Se presentó el siguiente error:" + exception.Message;
            }

        }
    }
}
