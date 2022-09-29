using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2Grupal
{
    struct Registro
    {
        public string P2_nombre { get; set; }
        public string P2_codigo { get; set; }

        public int P2_cantidad { get; set; }

        public double P2_precio_unitario{ get; set; }

        public double monto()
        {
            double monto = 0;
            monto = P2_cantidad * P2_precio_unitario;
            return monto;
        }
    }
}
