using System;

using ChapinWarriors.Models;

namespace ChapinWarriors.Services
{
    public class Engine
    {
        private DataProcess process;
        private RobotList<Robot> r_list;
        private CitiesList c_list;

        public Engine()
        {
            process = new DataProcess();
            r_list = process.LoadRobotsXml(); 
            c_list = process.LoadCitiesXml(); 
        }
        public void mainMenu(){
            // Console.WriteLine("¿Input redirigido? " + Console.IsInputRedirected);
            bool loop = true;

            Console.Clear();
            Console.WriteLine("<<< BIENVENIDO AL PROGRMA DE CONTROL DE MISIONES - CHAPINWARRIORS S.A. >>>");
            while(loop)
            {
                Console.WriteLine("(( MENU INICIAL ))");
                Console.WriteLine("Escribe el número correspondiente para ejecutar a cada opción del menú...");
                Console.WriteLine("1. Hacer una Misión.");
                Console.WriteLine("2. Mostrar Robots del sistema.");
                Console.WriteLine("3. Mostrar Ciudades Escaneadas.");
                Console.WriteLine("4. Salir");
                Console.Write("Ingresa a una opción: ");

                string? op1 = Console.ReadLine();


                if (op1 == null)
                {
                    Console.WriteLine("Error al ingresar datos");
                    break;
                }
                
                Console.Clear();
                switch (op1)
                {
                    case "1":
                        pause();
                        break;                        

                    case "2":
                        showRobots();
                        pause();
                        break;
                    case "3":
                        showCities();
                        pause();
                        break;
                    case "4":
                        Console.WriteLine("Cerrando el programa...");
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        pause();
                        break;
                }
            }

        }

        public void showRobots()
        {     
            r_list.ShowRobotList();
        }
        public void showCities()
        {     
            c_list.ShowCityList();
        }

       private void pause()
        {
            Console.WriteLine("\nPresiona ENTER para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }    

}