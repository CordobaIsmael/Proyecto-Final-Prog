using System;
using System.Net;

namespace ProyectoFinal_CordobaIsmael2
{
    internal class Program
    {
        static List<Alumno> alumnos = new List<Alumno>();
        static List<Materia> materias = new List<Materia>();
        static List<AlumnoMateria> alumnosEnMaterias= new List<AlumnoMateria>();
        public static string archivo = "Lista de Alumnos.txt";
        public static string archivo2 = "Materias Cursadas.txt";
        public static string archivo3 = "Materias.txt";
        public struct Alumno
        {
            public int Legajo;
            public string Apellido;
            public string Nombre;
            public string DNI;
            public DateTime fechaNacimiento;
            public string Domicilio;
            public bool estaActivo;
        }

        public struct Materia
        {
            public int indiceMateria;
            public string nombreMateria;
            public bool estaActiva;
        }

        public struct AlumnoMateria
        {
            public int IndiceAlumnoMateria;
            public int IndiceAlumno;
            public int IndiceMateria;
            public string Estado;
            public int Nota;
            public DateTime Fecha;
        }
        static int LeerEntero(string Mensaje)
        {
            int Numero = 0;
            bool esEntero = false;
            string datoEntrada;
            Console.Write(Mensaje);
            while (!esEntero)
            {
                datoEntrada = Console.ReadLine();
                if (!int.TryParse(datoEntrada, out Numero))
                {
                    Console.WriteLine("El valor ingresado no es valido. Ingreselo de nuevo: ");
                }
                else
                {
                    esEntero = true;
                }
            }
            return Numero;
        }

        static bool ExisteAlumno(int dni)
        {
            bool alumnoExistente = false;
            int i = 0;
            while (!alumnoExistente && i < alumnos.Count)
            {
                if (int.Parse(alumnos[i].DNI) == dni)
                {
                    alumnoExistente = true;
                }
                i++;
            }
            return alumnoExistente;
        }

        static Alumno buscarAlumno(int dni)
        {
            Alumno alumnoEncontrado = new Alumno();
            alumnoEncontrado.estaActivo = true;
            bool alumnoExistente = false;
            int i = 0;
            while (!alumnoExistente && i < alumnos.Count)
            {
                if (int.Parse(alumnos[i].DNI) == dni)
                {
                    alumnoExistente = true;
                    alumnoEncontrado.Legajo = alumnos[i].Legajo;
                    alumnoEncontrado.Apellido = alumnos[i].Apellido;
                    alumnoEncontrado.Nombre = alumnos[i].Nombre;
                    alumnoEncontrado.DNI = alumnos[i].DNI;
                    alumnoEncontrado.fechaNacimiento = alumnos[i].fechaNacimiento;
                    alumnoEncontrado.Domicilio = alumnos[i].Domicilio;
                    alumnoEncontrado.estaActivo = alumnos[i].estaActivo;
                }
                i++;
            }
            return alumnoEncontrado;
        }

        static void AltaAlumno() // preguntar si esta bien la reactivacion
        {
            int dni = LeerEntero("Ingrese el DNI para comenzar la operación: ");
            CargarAlumnosDesdeArchivo();
            

            if (! ExisteAlumno(dni))
            {

                Console.WriteLine("Ingrese el apellido del alumno: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del alumno: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese la fecha de nacimiento del alumno: ");
                DateTime fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Ingrese el domicilio del alumno: ");
                string Domicilio = Console.ReadLine();
                int nuevoIndice = alumnos.Count + 1;
                Alumno nuevoAlumno = new Alumno
                {
                    Legajo = nuevoIndice,
                    Nombre = nombre,
                    Apellido = apellido,
                    DNI = Convert.ToString(dni),
                    fechaNacimiento = fechaNacimiento,
                    Domicilio = Domicilio,
                    estaActivo = true,
                };
                alumnos.Add(nuevoAlumno);
                GuardarAlumnosEnArchivo();
                Console.WriteLine("Alumno dado de alta exitosamente.\n -------------------------------------------------------------------");
            }
            else
            {
                Alumno alumnoEncontrado = buscarAlumno(dni);
                if (alumnoEncontrado.estaActivo)
                {
                    Console.WriteLine("ERROR: Ya existe un alumno activo con el mismo DNI.\n ---------------------------------------------------");
                }
                else
                {
                    Console.Write("El alumno ya existe pero está inactivo. ¿Desea reactivarlo? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if ((respuesta == "S") || (respuesta == "s"))
                    {
                        alumnoEncontrado.estaActivo = true;
                        GuardarAlumnosEnArchivo();
                        Console.WriteLine("Alumno reactivado exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("El alumno no se ha activado.\n --------------------------------------------------------------");
                    }
                }
            }

        }
        static void AltaMateria()
        {
            Console.Write("Ingrese el nombre de la materia que desea agregar: ");
            string nombredemateria  = Console.ReadLine();
            CargarMateriaDesdeArchivo();
            bool materiaExistente = false;
            Materia materiaEncontrada = new Materia();
            materiaEncontrada.estaActiva = true;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (materias[i].nombreMateria == nombredemateria)
                {
                    materiaExistente = true;
                    materiaEncontrada.indiceMateria = materias[i].indiceMateria;
                    materiaEncontrada.nombreMateria = materias[i].nombreMateria;
                    materiaEncontrada.estaActiva = materias[i].estaActiva;
                    break;
                }
            }

            if (!materiaExistente)
            {
                
                int nuevoIndice = materias.Count + 1;
                Materia nuevaMateria = new Materia
                {
                    indiceMateria = nuevoIndice,
                    nombreMateria = nombredemateria,
                    estaActiva = true,
                };
                materias.Add(nuevaMateria);
                GuardarMateriaEnArchivo();
                Console.WriteLine("Materia dada de alta.\n -------------------------------------------------------------------");
            }
            else
            {
                if (materiaEncontrada.estaActiva)
                {
                    Console.WriteLine("ERROR: Ya existe una nateria activa con el mismo nombre.\n ---------------------------------------------------");
                }
                else
                {
                    Console.Write("La materia ya existe pero está inactiva. ¿Desea reactivarla? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if ((respuesta == "S") || (respuesta == "s"))
                    {
                        materiaEncontrada.estaActiva = true;
                        GuardarMateriaEnArchivo();
                        Console.WriteLine("Alumno reactivado exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("La materia no se ha activado.\n --------------------------------------------------------------");
                    }
                }
            }

        }

        

        public static List<Materia> RetornarListaMateria(string archivo2)
        {
            List<Materia> listaMateria = new();
            using (StreamReader sr = new StreamReader(archivo2))
            {
                string? linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] MateriaArchivo = linea.Split(',');
                    Materia materiaStruct = new();
                    materiaStruct.indiceMateria = int.Parse(MateriaArchivo[0]);
                    materiaStruct.nombreMateria = MateriaArchivo[1];
                    materiaStruct.estaActiva = Convert.ToBoolean(MateriaArchivo[2]);
                    listaMateria.Add(materiaStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaMateria;
        }

        static void GuardarMateriaEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(archivo2, true))
            {
                foreach (var materia in materias)
                {
                    writer.WriteLine($"{materia.indiceMateria}, {materia.nombreMateria}, {materia.estaActiva}");
                }
            }
        }

        static void GuardarAlumnosEnArchivo()
        {
            using (StreamWriter writer = new(archivo, true))
            {
                foreach (var alumno in alumnos)
                {
                    writer.WriteLine($"{alumno.Legajo}, {alumno.Nombre}, {alumno.Apellido}, {alumno.DNI}, {alumno.fechaNacimiento:dd/MM/yyyy}, {alumno.Domicilio}, {alumno.estaActivo}");
                }
            }
        }

        static void CargarAlumnosDesdeArchivo()
        {
            alumnos.Clear();
            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    Alumno alumno = new()
                    {
                        Legajo = int.Parse(separador[0]),
                        Nombre = separador[1],
                        Apellido = separador[2],
                        DNI = separador[3],
                        fechaNacimiento = DateTime.Parse(separador[4]),
                        Domicilio = separador[5],
                        estaActivo = bool.Parse(separador[6])
                    };
                    alumnos.Add(alumno);
                }
            }
        }

        static void CargarMateriaDesdeArchivo()
        {
            if (File.Exists(archivo2))
            {
                string[] lineas = File.ReadAllLines(archivo2);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    Materia materia = new()
                    {
                        indiceMateria = int.Parse(separador[0]),
                        nombreMateria = separador[1],
                        estaActiva = bool.Parse(separador[2])
                    };
                    materias.Add(materia);
                }
            }
        }

        static void BajaAlumno()
        {
            int dni = LeerEntero("Ingrese el DNI del alumno que desea dar de baja: ");
            using (StreamWriter escritor = new StreamWriter(archivo, false))
            {
                List<Alumno> alumnos = RetornarListaAlumnos(archivo);
                bool existe = false;
                Alumno alumnos2 = new Alumno();
                for (int i = 0; i < alumnos.Count; i++)
                {
                    if (alumnos[i].DNI == Convert.ToString(dni))
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    alumnos2.estaActivo = false;
                }

                GuardarAlumnosEnArchivo();

            }
        }

        static void BajaMateria()
        {
            Console.Write("Ingrese el nombre de la materia que quiere dar de baja");
            string nombreMateria = Console.ReadLine();
            
            using (StreamWriter escritor = new StreamWriter(archivo2, true))
            {
                List<Materia> Materias = RetornarListaMateria(archivo2);
                bool existe = false;
                Materia Materia2 = new Materia();
                for (int i = 0; i < Materias.Count; i++)
                {
                    if (materias[i].nombreMateria == nombreMateria)
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    Materia2.estaActiva = false;
                }

                GuardarMateriaEnArchivo();

            }
        }
    

        static void ModificarAlumno()  //Como modificar un alumno, como transcribirlo
        {
            string respuesta;
            int numerodni = LeerEntero("Ingrese el numero de dni del alumno que desea eliminar: ");
            using (StreamWriter escritor = new StreamWriter(archivo, false))
            {
                CargarAlumnosDesdeArchivo();
                bool existe = false;
                Alumno alumnos2 = new Alumno();
                for (int i = 0; i < alumnos.Count; i++)
                {
                    if (alumnos[i].DNI == Convert.ToString(numerodni))
                    {
                        alumnos2.Legajo = alumnos[i].Legajo;
                        alumnos2.Nombre = alumnos[i].Nombre;
                        alumnos2.Apellido = alumnos[i].Apellido;
                        alumnos2.DNI = Convert.ToString(numerodni);
                        alumnos2.Domicilio = alumnos[i].Domicilio;
                        alumnos2.fechaNacimiento = alumnos[i].fechaNacimiento;
                        alumnos2.estaActivo = alumnos[i].estaActivo;
                        existe = true;
                        alumnos.Remove(alumnos[i]);
                    }
                }
                if (existe)
                {
                    Console.Write($"El alumno a modificar es {alumnos2.Apellido} {alumnos2.Nombre}? S/N");
                    respuesta = Console.ReadLine();
                    if ((respuesta == "s") || (respuesta == "S"))
                    {
                        Console.WriteLine("Reingrese el nombre del alumno: ");
                        alumnos2.Nombre = Console.ReadLine();
                        Console.WriteLine("Reingrese el apellido: ");
                        alumnos2.Apellido = Console.ReadLine();
                        Console.WriteLine("Reingrese el domicilio: ");
                        alumnos2.Domicilio = Console.ReadLine();
                        Console.WriteLine("Reingrese la fecha de nacimiento (Recuerde ingresar las barras. Ej: 00/00/0000: ");
                        alumnos2.fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
                        alumnos.Add(alumnos2);

                    }
                    else
                    {
                        alumnos.Add(alumnos2);
                    }

                }


                GuardarAlumnosEnArchivo();

            }

        }


        static void ModificarMateria()
        {

        }


        public static List<Alumno> RetornarListaAlumnos(string archivo)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();
            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] alumnoArchivo = linea.Split(',');
                    Alumno alumnoStruct = new Alumno();
                    alumnoStruct.Legajo = int.Parse(alumnoArchivo[0]);
                    alumnoStruct.Nombre = alumnoArchivo[1];
                    alumnoStruct.Apellido = alumnoArchivo[2];
                    alumnoStruct.DNI = (alumnoArchivo[3]);
                    alumnoStruct.fechaNacimiento = Convert.ToDateTime(alumnoArchivo[4]);
                    alumnoStruct.Domicilio = (alumnoArchivo[4]);
                    alumnoStruct.estaActivo = Convert.ToBoolean(alumnoArchivo[6]);
                    listaAlumnos.Add(alumnoStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaAlumnos;
        }

        public static void MostrarAlumnosActivos()
        {
            List<Alumno> ListaActualizada = RetornarListaAlumnos(archivo);
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < ListaActualizada.Count; i++)
            {
                if (ListaActualizada[i].estaActivo)
                {
                    Console.WriteLine(ListaActualizada[i].Legajo + ", " + ListaActualizada[i].Nombre + ", " + ListaActualizada[i].Apellido);
                }

            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");
        }


        public static void MostrarAlumnosInactivos()
        {
            RetornarListaAlumnos(archivo);
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < alumnos.Count; i++)
            {
                bool alumnoEncontrado = alumnos[i].estaActivo;
                if (!alumnoEncontrado)
                {
                    Console.WriteLine(alumnos[i].Legajo + ", " + alumnos[i].Nombre + ", " + alumnos[i].Apellido);
                }

            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");

        }


        static void CargarAlumnosEnMaterias()
        {
            if (File.Exists(archivo3))
            {
                string[] lineas = File.ReadAllLines(archivo3);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    AlumnoMateria materiaAlumno = new()
                    {
                        IndiceAlumnoMateria = Convert.ToInt32(separador[0]),
                        IndiceAlumno = Convert.ToInt32(separador[1]),
                        IndiceMateria = Convert.ToInt32(separador[2]),
                        Estado = separador[3],
                        Nota = Convert.ToInt32(separador[4]),
                        Fecha = Convert.ToDateTime(separador[5]),
                        
                    };
                    alumnosEnMaterias.Add(materiaAlumno);
                }
            }
        }

        static void AnotarAlumnoMateria()
        {
            int dni = LeerEntero("ingrese el dni del alumno que desea anotar en una materia: ");
            CargarAlumnosDesdeArchivo();
            CargarMateriaDesdeArchivo();
            CargarAlumnosEnMaterias();
            int legajoAlumno=-1;
            int indiceMateria;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].DNI == Convert.ToString(dni))
                {
                    legajoAlumno = alumnos[i].Legajo;
                }
            }
            Console.Write("Ingrese la materia en la que desea inscribir al alumno: ");
            string nombreMateria = Console.ReadLine();
            /*
            for (int i = 0; i < materias.Count; i++)
            {
                if (materias[i].nombreMateria == nombreMateria)
                {
                    indiceMateria = materias[i].indiceMateria;
                }
            }
            int indiceAlumnoMateria = alumnosEnMaterias.Count + 1;
            AlumnoMateria materiaAlumno = new()
            {
                IndiceAlumnoMateria = indiceAlumnoMateria,
                IndiceAlumno = legajoAlumno,
                IndiceMateria = indiceMateria,
                Estado = separador[3],
                Nota = Convert.ToInt32(separador[4]),
                Fecha = Convert.ToDateTime(separador[5]),

            };
            alumnosEnMaterias.Add(materiaAlumno);
            */

        }

        static int ValidarNumeroMenu()
        {
            bool esValido = false;
            int Numero = 0;
            do
            {
                Console.WriteLine("==== MENÚ ====");
                Console.WriteLine("1. Alta de alumno");
                Console.WriteLine("2. Baja de alumno");
                Console.WriteLine("3. Modificación de alumno");
                Console.WriteLine("4. Mostrar alumnos activos");
                Console.WriteLine("5. Mostrar alumnos inactivos");
                Console.WriteLine("6. Alta de materia");
                Console.WriteLine("7. Baja de materia");
                Console.WriteLine("8. Modificación de materia");
                Console.WriteLine("9. Anotar alumno a materia");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                esValido = int.TryParse(Console.ReadLine(), out Numero);
                if (esValido)
                {
                    if ((Numero < 0) || (Numero > 9))
                    {
                        Console.WriteLine("\nOpción inválida. Intente nuevamente: ");
                    }
                }
            } while (!esValido);
            return Numero;
        }
        static void Main(string[] args)
        {
            int numEj = 0;
            CargarAlumnosDesdeArchivo();
            do
            {
                numEj = ValidarNumeroMenu();
                if (numEj == 1)
                {
                    AltaAlumno();
                }
                else if (numEj == 2)
                {
                    BajaAlumno();
                }
                else if (numEj == 3)
                {
                    ModificarAlumno();
                }
                else if (numEj == 4)
                {
                    MostrarAlumnosActivos();
                }
                else if (numEj == 5)
                {
                    MostrarAlumnosInactivos();
                }
                else if (numEj == 6)
                {
                    AltaMateria();
                }
                
                else if (numEj == 7)
                {
                    BajaMateria();
                }
               
                else if (numEj == 8)
                {
                    ModificarMateria();
                } 
                else if (numEj == 9)
                {
                    AnotarAlumnoMateria();
                }
            } while (numEj != 0);
        }

    }
}
