using System;
using System.Collections.Generic;

namespace Hsec.Application.Observacion.Queries.GetPortletGrafico
{
    public class PortletMesesVM
    {
        public PortletMesesVM()
        {
            //Labels = new List<string>(12);
            Tarea = new List<int>() {0,0,0,0,0,0,0,0,0,0,0,0};
            Comportamiento = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Condicion = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Interacion_Seguridad = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        //public List<string> Labels { get; set; }
        public List<int> Tarea {get;set;}
        public List<int> Comportamiento { get; set; }
        public List<int> Condicion { get; set; }
        public List<int> Interacion_Seguridad { get; set; }
    }
}