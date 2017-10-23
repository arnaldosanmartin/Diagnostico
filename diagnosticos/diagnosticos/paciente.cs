using System;
using System.Collections.Generic;
using System.Text;

namespace diagnosticos
{
    public class paciente
    {
        public string name { get; set; }
        public IList<String> his;

        public paciente(String Name, IList<String> His)
        {
            this.name = Name;
            this.his = His;
        }
    }
}
