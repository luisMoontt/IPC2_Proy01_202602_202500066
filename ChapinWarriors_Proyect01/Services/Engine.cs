using System;

using ChapinWarriors.Models;

namespace ChapinWarriors.Services
{
    public class Engine
    {
        private DataProcess process;
        private RobotList<Robot> r_list;
        private CitiesList c_list;

        City? misionCity;
        MapMatrix? cityMap;
        MilitaryList? cityMilitaries;
        

        public Engine()
        {
            process = new DataProcess();
            r_list = process.LoadRobotsXml(); 
            c_list = process.LoadCitiesXml(); 
        }
        // MISIONES //////////////////////////////////
        public void extractMision(){
            Fighter? missionRobot;
            cityMap = misionCity.getMap();
            cityMilitaries = misionCity.getMList();
            MapNode? startNode; 
            MapNode? finalNode;
            int n_E = cityMap.countSymbol("E");
            int n_R = cityMap.countSymbol("R");
            int[] E_cordinates = new int[2];
            int[] R_cordinates = new int[2];
            bool loop4 = true;

            while(loop4)
            {
                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E} y Recursos disponilbes para extracción: {n_R}.");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine("\n<<LISTA DE ROBOTS DISPONIBLES PARA MISION DE EXTRACCIÓN>>");
                r_list.listFighters();
                Console.WriteLine("\nIntroducir nombre del robot para realizar la extracción.");
                string? r_name = Console.ReadLine();

                missionRobot = r_list.getFighter(r_name);

                if(missionRobot==null){
                    Console.WriteLine("El nombre no corresponde a ningún ChapinFighter");
                    break;
                }

                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E} y Recursos disponilbes para extracción: {n_R}");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine($"\n<<{missionRobot.getType().ToUpper()} - {missionRobot.getName()} ({missionRobot.getCapacity()})>> Seleccionado para extracción.");
                
                if(n_E > 1)
                {
                    do
                    {
                        Console.WriteLine("\nIntroducir coordenada en X de la entrada a seleccionar.");
                        int c_sNode = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Introducir coordenada en Y de la entrada a seleccionar.");
                        int r_sNode = int.Parse(Console.ReadLine()!);
                        

                        startNode = cityMap.GetMapNode(r_sNode-1, c_sNode-1);
                        //Console.WriteLine(startNode.getSymbol);

                        if(startNode==null || startNode.getSymbol()!="E")
                        {
                            Console.WriteLine($"\nEntrada no encontrada en coordenadas ({c_sNode},{r_sNode}), volver a ingresar datos...");
                        }
                        E_cordinates[0] = r_sNode;
                        E_cordinates[1] = c_sNode;

                    } while (startNode == null || startNode.getSymbol()!="E");
                    
                } else
                {
                    startNode = cityMap.findSymbol("E");
                    E_cordinates = cityMap.GetNodeCoordinates(startNode);
                }

                if(n_R > 1)
                {
                    do
                    {
                        Console.WriteLine("\nIntroducir coordenada en X del recurso a extraer.");
                        int c_rNode = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Introducir coordenada en Y del recurso a extraer.");
                        int r_rNode = int.Parse(Console.ReadLine()!);

                        finalNode = cityMap.GetMapNode(r_rNode-1, c_rNode-1);
                        if(finalNode==null || finalNode.getSymbol()!="R")
                        {
                            Console.WriteLine($"\nRecurso no encontrado en coordenadas ({c_rNode},{r_rNode}), volver a ingresar datos...");
                        }
                        R_cordinates[0] = r_rNode;
                        R_cordinates[1] = c_rNode;

                    } while (finalNode == null || finalNode.getSymbol()!="R");
                    
                } else
                {
                    finalNode = cityMap.findSymbol("R");
                    R_cordinates = cityMap.GetNodeCoordinates(finalNode);
                }

                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E} y Recursos disponilbes para extracción: {n_R}");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine($"\n<<{missionRobot.getType().ToUpper()} - {missionRobot.getName()} ({missionRobot.getCapacity()})>>");
                Console.WriteLine($"Seleccionado para despliegue en coordenadas en ({E_cordinates[0]},{E_cordinates[1]}) y extracción en ({R_cordinates[0]},{R_cordinates[1]})"); 
                Console.WriteLine($"\nConfirmar mision...");  
                pause();
                loop4 =false;
                findExtractPath(startNode, finalNode2);
            }
        }
        public void rescueMision(){
            Rescue? missionRobot;
            cityMap = misionCity.getMap();
            cityMilitaries = misionCity.getMList();
            MapNode? startNode; 
            MapNode? finalNode;
            int n_E = cityMap.countSymbol("E");
            int n_C = cityMap.countSymbol("C");
            int[] E_cordinates = new int[2];
            int[] C_cordinates = new int[2];
            bool loop4 = true;

            while(loop4)
            {
                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E},  Unidades Civiles disponilbes para rescate: {n_C}.");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine("\n<<LISTA DE ROBOTS DISPONIBLES PARA MISION DE EXTRACCIÓN>>");
                r_list.listRescues();
                Console.WriteLine("\nIntroducir nombre del robot para realizar la extracción.");
                string? r_name = Console.ReadLine();

                missionRobot = r_list.getRescue(r_name);

                if(missionRobot==null){
                    Console.WriteLine("El nombre no corresponde a ningún ChapinRescue");
                    break;
                }

                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E}, Unidades Civiles disponilbes para rescate: {n_C}.");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine($"\n<<{missionRobot.getType().ToUpper()} - {missionRobot.getName()}>> Seleccionado para rescate.");
                
                if(n_E > 1)
                {
                    do
                    {
                        Console.WriteLine("\nIntroducir coordenada en X de la entrada a seleccionar.");
                        int c_sNode = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Introducir coordenada en Y de la entrada a seleccionar.");
                        int r_sNode = int.Parse(Console.ReadLine()!);
                        

                        startNode = cityMap.GetMapNode(r_sNode-1, c_sNode-1);
                        //Console.WriteLine(startNode.getSymbol);

                        if(startNode==null || startNode.getSymbol()!="E")
                        {
                            Console.WriteLine($"\nEntrada no encontrada en coordenadas ({c_sNode},{r_sNode}), volver a ingresar datos...");
                        }
                        E_cordinates[0] = r_sNode;
                        E_cordinates[1] = c_sNode;

                    } while (startNode == null || startNode.getSymbol()!="E");
                    
                } else
                {
                    startNode = cityMap.findSymbol("E");
                    E_cordinates = cityMap.GetNodeCoordinates(startNode);
                }

                if(n_C > 1)
                {
                    do
                    {
                        Console.WriteLine("\nIntroducir coordenada en X de la Unidad Civil a rescatar.");
                        int c_cNode = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Introducir coordenada en Y de la Unidad Civil a rescatar.");
                        int r_cNode = int.Parse(Console.ReadLine()!);

                        finalNode = cityMap.GetMapNode(r_cNode-1, c_cNode-1);
                        if(finalNode==null || finalNode.getSymbol()!="C")
                        {
                            Console.WriteLine($"\nUnidad Civil no encontrado en coordenadas ({c_cNode},{r_cNode}), volver a ingresar datos...");
                        }
                        C_cordinates[0] = r_cNode;
                        C_cordinates[1] = c_cNode;

                    } while (finalNode == null || finalNode.getSymbol()!="C");
                    
                } else
                {
                    finalNode = cityMap.findSymbol("C");
                    C_cordinates = cityMap.GetNodeCoordinates(finalNode);
                }

                Console.Clear();
                Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                Console.WriteLine(" ");
                cityMap.ShowConsoleMap();
                Console.WriteLine(" ");
                Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                Console.WriteLine($"Entradas de despliegue disponibles: {n_E}, Unidades Civiles disponilbes para rescate: {n_C}.");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                cityMilitaries.ShowMilitaryList();
                Console.WriteLine($"\n<<{missionRobot.getType().ToUpper()} - {missionRobot.getName()}>>");
                Console.WriteLine($"Seleccionado para despliegue en ({E_cordinates[1]},{E_cordinates[0]}) y rescate en ({C_cordinates[1]},{C_cordinates[0]})"); 
                Console.WriteLine($"\nConfirmar mision...");  
                pause();
                loop4 =false;
                findExtractPath(startNode, finalNode2);
            }
        }

        // MENUS //////////////////////////////////////
        public void misionMenu(){
            bool loop = true;

            Console.Clear();
            Console.WriteLine("<<< MENU DE MISIONES >>>");
            while(loop)
            {
                Console.WriteLine("Para iniciar una mision necesitas elegir una ciudad");
                Console.WriteLine(" ");
                Console.WriteLine("(( CIUDADES ))");
                c_list.cityNames();
                Console.WriteLine(" ");
                Console.WriteLine("1. Introducir nombre de ciudad");
                Console.WriteLine("2. Atrás...");
                Console.Write("Ingresa a una opción: ");

                string? op1 = Console.ReadLine();

                if (op1 == null)
                {
                    Console.WriteLine("Error al ingresar datos");
                    break;
                }
                
                switch (op1)
                {
                    case "1":
                        bool loop2 = true;
                        
                        while(loop2)
                        {
                            Console.WriteLine("Introducir nombre de ciudad para realizar la misión.");
                            string? c_name = Console.ReadLine();

                            if(c_list.cityExists(c_name))
                            {   
                                loop2 = false;
                                //City? misionCity = c_list.getCity(c_name);
                                misionCity = c_list.getCity(c_name);
                                cityMap = misionCity.getMap();
                                //cityMilitaries = misionCity.getMList();

                                bool loop3 = true;
                        
                                while(loop3)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"\n<< MAPA DE CIUDAD: {misionCity.getName()} >>");
                                    Console.WriteLine(" ");
                                    cityMap.ShowConsoleMap();
                                    Console.WriteLine(" ");
                                    Console.WriteLine($"Ciudad: {misionCity.getName()} | Dimensiones: {misionCity.getC_Rows()}x{misionCity.getC_Columns()}");
                                    Console.WriteLine("C = Unidad Civil");
                                    Console.WriteLine("R = Recurso");
                                    Console.WriteLine("M = Unidad Militar");
                                    Console.WriteLine("E = Punto de Entrada");
                                    
                                    //Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                                    //cityMilitaries.ShowMilitaryList();
                                    Console.WriteLine("\n Elige una opción para el tipo de misión a realizar...");
                                    Console.WriteLine("1. Mision de Rescate.");
                                    Console.WriteLine("2. Mision de Extracción.");
                                    Console.WriteLine("3. Atrás...");
                                    string? op3 = Console.ReadLine();

                                    switch(op3)
                                    {
                                        case "1":
                                            Console.WriteLine("AQUI IRIA MISION DE RESCATE");
                                            rescueMision();
                                            break;                        
                                        case "2":
                                            Console.WriteLine("AQUI IRIA MISION DE EXTRACCION");
                                            extractMision();
                                            break;
                                        case "3":
                                            Console.Clear();
                                            loop3 = false;
                                            break;
                                        default:
                                            Console.WriteLine("Opción inválida");
                                            pause();
                                            break;
                                    }
                                }
                            } else 
                            {
                                Console.WriteLine("Ciudad no encontrada.");
                                pause();
                                loop2 = false;
                            }
                        }
                        break;                        
                    case "2":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        pause();
                        break;
                }
            }
        }

        public void mainMenu(){
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
                        misionMenu();
                        Console.Clear();
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
        // BUSCAR CAMINO //////////////////////////////
        public void findExtractPath(MapNode startNode, MapNode finalNode, int missionType){
            MapMatrix pathMap = cityMap;
            bool concidence = false;

            startC = cityMap.GetNodeCoordinates(startNode);
            finalC = cityMap.GetNodeCoordinates(finalNode);

            



            DragPath StartPath = DragPath()

            while(!concidence){

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