using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public abstract class Liquidacion

    {
        public const double SMMLV = 908526;
   

        public int numeroLiquidacion { get; set; }
        public double valorServicio { get; set; }
        public Paciente paciente { get; set; }

        public DateTime fechaLiquidacion { get; set; }

        public double tarifa { get; set; }

        public double topeMaximo { get; set; }

        public double cuotaModeradora { get; set; }

        public double CalcularCuotaModeradora()
        {
            cuotaModeradora = tarifa * valorServicio;
            if (topeMaximo < cuotaModeradora)
            {
                cuotaModeradora = topeMaximo;
            }
            return cuotaModeradora;
        }

        public abstract double CalcularTarifa();

        public abstract double CalcularTope();


        public override string ToString()
        {
            return $"NumeroLiquidacion: {numeroLiquidacion} identificacion: {paciente.identificacion} aAfiliacion:{paciente.afiliacion} salario:{paciente.salario} valor del servicii:{valorServicio}\n tarifa :{tarifa} topeMax :{topeMaximo} cuota moderadora :{cuotaModeradora}";
        }

    }
}
