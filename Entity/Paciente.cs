using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Paciente
    {
        public Paciente(string identificacion, string afiliacion, double salario)
        {
            this.identificacion = identificacion;
            this.afiliacion = afiliacion;
            this.salario = salario;
        }
        
        public string identificacion { get; set; }
        public string afiliacion { get; set; }
        public double salario { get; set; }

        
      
    }
}