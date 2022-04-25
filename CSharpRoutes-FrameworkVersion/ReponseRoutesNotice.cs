using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoutes.lib
{
    public class ReponseRoutesNotice
    {
        string _mensaje = "Correct";
        bool _result = true;
        public bool Resultado { get => _result; set => _result = value; }
        public string Mensaje { get => _mensaje; set => _mensaje = value; }
        public object Data { get; set; }
    }
}
