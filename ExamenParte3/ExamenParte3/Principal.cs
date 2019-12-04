using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ExamenParte3
{
    public class Principal
    {
        //Se crea la lista de tipo global para tener mayor facilidad a la hora de utilizarla
        List<Gato> ListaGato = new List<Gato>();
        public Principal()
        {
            //Constructor donde se manda a llamar al metodo para obtener la lista del gato desde un inicio
            ListaGato = ObtenerListaGato();
        }
        //Metodo de bienvenida donde se llama al menu principal
        public void Bienvenido()
        {
            Console.WriteLine("---BIENVENIDO---");
            Console.WriteLine("Presione cualquier tecla para empezar . . .");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
        public void Menu()
        {         //Metodo menu donde para elegir una de las siguientes acciones
            Console.WriteLine("---MENU---");
            Console.WriteLine("Presione 1 para ver la lista de Gatos");
            Console.WriteLine("Presione 2 para ver los detalles de un gato");
            Console.WriteLine("Presione 3 para editar algun atributo del gato");
            Console.WriteLine("Presione 4 para salir del programa");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    MostrarLista();                  
                    break;
                case "2":
                    Console.Clear();
                    DetallarGato();
                    break;
                case "3":
                    Console.Clear();
                    EditarGato();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ha ingresado un dato invalido, preisone cualquier tecla para volver al menu");
                    Console.ReadLine();
                    Console.Clear();
                    Menu();
                    break;
            }
        }
        public List<string> ObtenerLineas(string path)
        {
            //aqui se crea el metodo obtener lineas que vimos en clase, sirve para pasar el archivo a una lista string
            List<string> Lineas = new List<string>();
            if (File.Exists(path))
            {
                string[] datos = File.ReadAllLines(path);
                foreach (var item in datos)
                {
                    Lineas.Add(item);
                }
            }
            else
            {
                Console.WriteLine("No se encontro archivo existente");
                return null;
            }
            return Lineas;
        }
        public List<Gato> ObtenerListaGato()
        {//Metodo para pasar la lista recien hecha de string a la lista de gatos, dividiendola con split y poniendo los atributos en orden
            Gato g = new Gato();
            var Lineas = ObtenerLineas("Lista.txt");
            List<Gato> ListaG = new List<Gato>();
            foreach (var item in Lineas)
            {
                string[] datos = item.Split(',');
                ListaG.Add(new Gato { Id = Convert.ToInt32(datos[0]), Nombre = datos[1], Dueño = datos[2], Raza = datos[3], Genero = datos[4] });               
            }
            return ListaG;
        }
        public void MostrarLista()
        {//Metodo que sirve para mostrar la lista de gatos, imprime el nombre del gato y su id
            Console.WriteLine("---GATOS---");
            foreach (var item in ListaGato)
            {
                Console.WriteLine(item.Id + " " + item.Nombre);
            }
            Console.WriteLine("\nPresione cualquier tecla para regresar al menu");
            Console.ReadLine();
            Console.Clear();
            Menu();
        }
        public void DetallarGato()
        {//Metodo para detallar a un gato en el cual se auntentifica si el gato al que quiere acceder el usuario esta ahi atraves del Id
            try
            {            
                Console.WriteLine("Ingrese el Id (Numero que aparece a la izquierda en la lista de los gatos) del gato que desee obtener sus detalles");
                int IdIngresada = Convert.ToInt32(Console.ReadLine());
                //Aqui hice una compuerta AND, ya que solo hay nueve gatos con Ids fijos, de estar el numero del usuario adentro se procede a mostrar los atributos del gato
                if (IdIngresada >= 0 && IdIngresada <= 9)
                {
                    foreach (var item in ListaGato)
                    {
                        if (IdIngresada == item.Id)
                        {
                            Console.WriteLine("Id: " + item.Id);
                            Console.WriteLine("Nombre del gato: " + item.Nombre);
                            Console.WriteLine("Nombre del dueño: " + item.Dueño);
                            Console.WriteLine("Raza: " + item.Raza);
                            Console.WriteLine("Genero: " + item.Genero);
                        }
                    }
                    Console.WriteLine("\nPresione cualquier tecla para regresar al menu");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
                else
                {
                    //De no encontrarse un gato con dicho Id se mandara al usuario al menu
                    Console.WriteLine("El dato que ha ingresado no coincide con ninguno de la lista");
                    Console.WriteLine("Ingrese cualquier tecla para regresar al menu");
                    Console.ReadLine();
                    Console.Clear();
                    Menu();

                }                                                           
            }
            catch (Exception e)
            {
                //Como el Id es de tipo Int puse este Try catch en caso de que se ingrese un valor distinto al int.
                Console.WriteLine("El dato ingresado debe de ser un numero, presione cualquier tecla para volver a intentar");
                Console.ReadKey();
                Console.Clear();
                DetallarGato();
            }
        }
        public void EditarGato()
        {
            //Metodo para editar los atributos de un gato en el cual se auntentifica si el gato al que quiere editar el usuario esta ahi atraves del Id
            try
            {
                Console.WriteLine("Ingrese el Id (Numero que aparece a la izquierda en la lista de los gatos) del gato que desee editar");
                int IdIngresada = Convert.ToInt32(Console.ReadLine());
                //Aqui hice una compuerta AND, ya que solo hay nueve gatos con Ids fijos, de estar el numero del usuario adentro se procede a mostrar el menu
                if (IdIngresada >= 0 && IdIngresada <= 9)
                {
                    foreach (var item in ListaGato)
                    {
                        if (IdIngresada == item.Id)
                        {
                            //Aqui hice un menu rapido para poder editar un atributo del gato o todos.
                            Console.Clear();
                            Console.WriteLine("---MENU--EDITAR---");
                            Console.WriteLine("Presione 1 para cambiar el Nombre del gato");
                            Console.WriteLine("Presione 2 para cambiar el dueño");
                            Console.WriteLine("Presione 3 para cambiar raza");
                            Console.WriteLine("Presione 4 para cambiar genero");
                            Console.WriteLine("Presione 5 para editar todos los atributos");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.WriteLine("Ingrese nuevo Nombre de gato");
                                    item.Nombre = Console.ReadLine();
                                    break;
                                case "2":
                                    Console.WriteLine("Ingrese nuevo Nombre de dueño");
                                    item.Dueño = Console.ReadLine();
                                    break;
                                case "3":
                                    Console.WriteLine("Ingrese nueva raza");
                                    item.Raza = Console.ReadLine();
                                    break;
                                case "4":
                                    Console.WriteLine("Ingrese nuevo genero");
                                    item.Genero = Console.ReadLine();
                                    break;
                                case "5":
                                    Console.WriteLine("Ingrese nuevo Nombre de gato");
                                    item.Nombre = Console.ReadLine();
                                    Console.WriteLine("Ingrese nuevo Nombre de dueño");
                                    item.Dueño = Console.ReadLine();
                                    Console.WriteLine("Ingrese nueva raza");
                                    item.Raza = Console.ReadLine();
                                    Console.WriteLine("Ingrese nuevo genero");
                                    item.Genero = Console.ReadLine();
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Ha ingresado un dato invalido, presione cualquier tecla para volver a intentar");
                                    Console.ReadLine();
                                    Console.Clear();
                                    EditarGato();
                                    break;
                            }
                        }
                        
                    }
                    Console.WriteLine("\nListo presione cualquier tecla para regresar al menu");
                    //En caso de que se proceda a editar los atributos de forma exitosa, se manda a llamar al metodo para realizar la actualizacion del archivo
                    ActualizarLista();
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
                else
                {
                    Console.WriteLine("El dato que ha ingresado no coincide con ninguno de la lista");
                    Console.WriteLine("Ingrese cualquier tecla para regresar al menu");
                    Console.ReadLine();
                    Console.Clear();
                    Menu();
                }
            }
            catch (Exception e)
            {
                //Como el Id es de tipo Int puse este Try catch en caso de que se ingrese un valor distinto al int.
                Console.WriteLine("El dato ingresado debe de ser un numero, presione cualquier tecla para volver a intentar");
                Console.ReadKey();
                Console.Clear();
                EditarGato();              
            }
        }
        public void ActualizarLista()
        {//Por ultimo este metodo actualiza la informacion del archivo txt
            //Primero se crea una lista de tipo string
            List<string> Lineas = new List<string>();
            foreach (var item in ListaGato)
            {
                //Aqui se va actualizando de objeto en objeto de la lista
                string[] NuevoAtrib = new string[5];
                NuevoAtrib[0] = Convert.ToString(item.Id);
                NuevoAtrib[1] = item.Nombre;
                NuevoAtrib[2] = item.Dueño;
                NuevoAtrib[3] = item.Raza;
                NuevoAtrib[4] = item.Genero;
                //Se utiliza el join como la contraparte del split, es decir en vez de que estemos separando para llenar atributos, estamos rejuntandolos para escribirlos en una sola linea de texto
                Lineas.Add(string.Join(",", NuevoAtrib));
            }
            var joinedstring = string.Join("\n", Lineas);
            //Una vez terminado este proceso ya se pasa a escribir el texto de la ListaGato, a el archivo txt.
            File.WriteAllText("Lista.txt", joinedstring);
        }
    }
}
