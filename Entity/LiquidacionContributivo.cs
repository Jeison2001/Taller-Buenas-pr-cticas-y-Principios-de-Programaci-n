using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LiquidacionContributivo : Liquidacion
    {

        public override double CalcularTarifa()
        {
           

                if (paciente.salario < (SMMLV * 2))
                {
                    tarifa = 0.15;
                }
                else if (paciente.salario < (SMMLV * 5))
                {
                    tarifa = 0.20;
                }
                else if (paciente.salario >= (SMMLV * 5))
                {
                    tarifa = 0.25;
                }
                return tarifa;
           
        }



        public override double CalcularTope()
        {
           
                if (paciente.salario < (SMMLV * 2))
                {
                    topeMaximo = 250000;
                }
                else if (paciente.salario < (SMMLV * 5))
                {
                    topeMaximo = 900000;
                }
                else if (paciente.salario >= (SMMLV * 5))
                {
                    topeMaximo = 1500000;
                }
                return topeMaximo;
            
        }

        
    }
}
