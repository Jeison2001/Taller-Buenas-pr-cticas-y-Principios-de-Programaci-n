using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LiquidacionSubsidiado : Liquidacion
    {
       

        public override double CalcularTarifa()
        {

            
                return tarifa = 0.5;
            
        }

   

        public override double CalcularTope()
        {

            
            
                return topeMaximo = 200000;
            

        }

        
    }
}
