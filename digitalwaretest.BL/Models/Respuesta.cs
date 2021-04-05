using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using digitalwaretest.BL.Logica;
 


namespace digitalwaretest.BL.Models
{
    public class Respuesta<T> where T  : class 
    {

        public bool Exist { get; set; }
        public bool Estadopeticion { get; set; }
        public string MensajeRespuesta { get; set; }
        public  List<T> Registros { get; set; }

    }
}
