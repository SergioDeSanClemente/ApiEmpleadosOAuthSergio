using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEmpleadosOAuth.Data;
using ApiEmpleadosOAuth.Models;

namespace ApiEmpleadosOAuth.Repositories {
    public class EmpleadosRepository {
        private EmpleadosContext context;
        public EmpleadosRepository(EmpleadosContext context) {
            this.context = context;
        }
        public List<Empleado> GetEmpleados() {
            return this.context.Empleados.ToList();
        }
        public Empleado FinEmpleado(int id) {
            return this.context.Empleados.SingleOrDefault(x=>x.IdEmpleado==id);
        }
        public Empleado ExistsEmpleado(string apellido, int empno) {
            var consulta = from datos in this.context.Empleados
                           where datos.Apellido == apellido
                           && datos.IdEmpleado == empno
                           select datos;
            if (consulta.Count() == 0) {
                return null;
            }
            else {
                return consulta.First();
            }

            
        }

    }
}
