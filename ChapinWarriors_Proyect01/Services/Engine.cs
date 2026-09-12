using System;
using ChapinWarriors.UI;
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
                
                // if(n_E > 1)
                // {
                //     do
                //     {
                //         Console.WriteLine("\nIntroducir coordenada en X de la entrada a seleccionar.");
                //         int c_sNode = int.Parse(Console.ReadLine()!);
                //         Console.WriteLine("Introducir coordenada en Y de la entrada a seleccionar.");
                //         int r_sNode = int.Parse(Console.ReadLine()!);
                        

                //         startNode = cityMap.GetMapNode(r_sNode-1, c_sNode-1);
                //         //Console.WriteLine(startNode.getSymbol);

                //         if(startNode==null || startNode.getSymbol()!="E")
                //         {
                //             Console.WriteLine($"\nEntrada no encontrada en coordenadas ({c_sNode},{r_sNode}), volver a ingresar datos...");
                //         }
                //         E_cordinates[0] = r_sNode;
                //         E_cordinates[1] = c_sNode;

                //     } while (startNode == null || startNode.getSymbol()!="E");
                    
                // } else
                // {
                //     startNode = cityMap.findSymbol("E");
                //     E_cordinates = cityMap.GetNodeCoordinates(startNode);
                // }

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

                findshortRoad(R_cordinates[0], R_cordinates[1], cityMap);
                pause();

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
                //findExtractPath(startNode, finalNode, missionRobot);
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
                    Console.WriteLine("UNICAS");
                    finalNode = cityMap.findSymbol("C");
                    C_cordinates = cityMap.GetNodeCoordinates(finalNode);
                    pause();
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
                findRescuePath(startNode, finalNode, missionRobot);
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
        public void findExtractPath(MapNode startNode, MapNode finalNode, Fighter missionRobot)
        {
            MapMatrix missionMap = ClonarMapa(cityMap, misionCity.getC_Rows(), misionCity.getC_Columns());

            int[] startC = cityMap.GetNodeCoordinates(startNode);
            int[] finalC = cityMap.GetNodeCoordinates(finalNode);

            int startRow = startC[0] - 1, startCol = startC[1] - 1;
            int finalRow = finalC[0] - 1, finalCol = finalC[1] - 1;

            DragPath startPath = new DragPath(startRow, startCol);
            DragPath finalPath = new DragPath(finalRow, finalCol);

            PathNode[] hojasInicio = new PathNode[] { startPath.getRoot()! };
            PathNode[] hojasFinal = new PathNode[] { finalPath.getRoot()! };

            missionMap.GetMapNode(startRow, startCol)!.setState(false);
            missionMap.GetMapNode(finalRow, finalCol)!.setState(false);

            bool coincidencia = false;
            PathNode? nodoInicio = null;
            PathNode? nodoFinal = null;

            while (!coincidencia)
            {
                hojasInicio = ExpandirHojas(hojasInicio, startPath, missionMap, false, hojasFinal, out PathNode? propioA, out PathNode? contrarioA);
                if (contrarioA != null)
                {
                    nodoInicio = propioA;
                    nodoFinal = contrarioA;
                    coincidencia = true;
                    break;
                }

                hojasFinal = ExpandirHojas(hojasFinal, finalPath, missionMap, false, hojasInicio, out PathNode? propioB, out PathNode? contrarioB);
                if (contrarioB != null)
                {
                    nodoInicio = contrarioB;
                    nodoFinal = propioB;
                    coincidencia = true;
                    break;
                }

                if (!HayVivos(hojasInicio) && !HayVivos(hojasFinal))
                {
                    Console.Clear();
                    Console.WriteLine("\n>> MISION FALLIDA: no existe un camino posible hacia el objetivo <<");
                    missionMap.ShowConsoleMap();

                    MissionGraphExporter.ExportarMision(
                        missionMap, misionCity.getC_Rows(), misionCity.getC_Columns(), misionCity.getName(),
                        "Extracción", missionRobot.getName(), missionRobot.getType(), missionRobot.getCapacity(), false,
                        finalC[0], finalC[1], "Recurso a extraer", new Military[0]);

                    return;
                }
            }

            PathNode[] ruta = ConstruirRuta(nodoInicio!, nodoFinal!);

            resolverCombateExtraccion(ruta, missionMap, missionRobot, finalC[0], finalC[1]);
        }

        public void findRescuePath(MapNode startNode, MapNode finalNode, Rescue missionRobot)
        {
            MapMatrix missionMap = ClonarMapa(cityMap, misionCity.getC_Rows(), misionCity.getC_Columns());

            int[] startC = cityMap.GetNodeCoordinates(startNode);
            int[] finalC = cityMap.GetNodeCoordinates(finalNode);

            int startRow = startC[0] - 1, startCol = startC[1] - 1;
            int finalRow = finalC[0] - 1, finalCol = finalC[1] - 1;

            DragPath startPath = new DragPath(startRow, startCol);
            DragPath finalPath = new DragPath(finalRow, finalCol);

            PathNode[] hojasInicio = new PathNode[] { startPath.getRoot()! };
            PathNode[] hojasFinal = new PathNode[] { finalPath.getRoot()! };

            missionMap.GetMapNode(startRow, startCol)!.setState(false);
            missionMap.GetMapNode(finalRow, finalCol)!.setState(false);

            bool coincidencia = false;
            PathNode? nodoInicio = null;
            PathNode? nodoFinal = null;

            while (!coincidencia)
            {
                hojasInicio = ExpandirHojas(hojasInicio, startPath, missionMap, true, hojasFinal, out PathNode? propioA, out PathNode? contrarioA);
                if (contrarioA != null)
                {
                    nodoInicio = propioA;
                    nodoFinal = contrarioA;
                    coincidencia = true;
                    break;
                }

                hojasFinal = ExpandirHojas(hojasFinal, finalPath, missionMap, true, hojasInicio, out PathNode? propioB, out PathNode? contrarioB);
                if (contrarioB != null)
                {
                    nodoInicio = contrarioB;
                    nodoFinal = propioB;
                    coincidencia = true;
                    break;
                }

                if (!HayVivos(hojasInicio) && !HayVivos(hojasFinal))
                {
                    Console.Clear();
                    Console.WriteLine("\n>> MISION FALLIDA: no existe un camino posible hacia el objetivo <<");
                    missionMap.ShowConsoleMap();

                    MissionGraphExporter.ExportarMision(
                        missionMap, misionCity.getC_Rows(), misionCity.getC_Columns(), misionCity.getName(),
                        "Rescate", missionRobot.getName(), missionRobot.getType(), null, false,
                        finalC[0], finalC[1], "Unidad civil a rescatar", new Military[0]);

                    return;
                }
            }

            PathNode[] ruta = ConstruirRuta(nodoInicio!, nodoFinal!);

            foreach (PathNode paso in ruta)
            {
                missionMap.AddValue(paso.getP_row(), paso.getP_col(), 9, ".");
            }

            Console.Clear();
            Console.WriteLine("\n>> MISION DE RESCATE EXITOSA <<");
            missionMap.ShowConsoleMap();
            Console.WriteLine($"\nRobot utilizado: {missionRobot.getName()}");

            MissionGraphExporter.ExportarMision(
                missionMap, misionCity.getC_Rows(), misionCity.getC_Columns(), misionCity.getName(),
                "Rescate", missionRobot.getName(), missionRobot.getType(), null, true,
                finalC[0], finalC[1], "Unidad civil rescatada", new Military[0]);
        }


/////////////////////////////////////////////////////////////////////////
        public void findshortRoad(int finalR, int finalC, MapMatrix cityMap)
        {
            int enters = cityMap.countSymbol("E");
            Road[] roads = new Road[enters];
            MapNode[] allEnters = cityMap.GetEntryNodes();

            for (int i = 0; i < allEnters.Length; i++)
            {
                int[] coords = cityMap.GetNodeCoordinates(allEnters[i]);
                int r = coords[0];
                int c = coords[1];

                Console.WriteLine($"Entrada {i + 1}: Fila = {r}, Columna = {c}");
            }

            pause();
        }
//////////////////////////////////////////////////////////////////////////


        private bool HayVivos(PathNode[] hojas)
        {
            foreach (PathNode h in hojas)
            {
                if (h.getState()) return true;
            }
            return false;
        }

        private PathNode? BuscarHojaViva(PathNode[] hojas, int row, int col)
        {
            foreach (PathNode h in hojas)
            {
                if (h.getState() && h.getP_row() == row && h.getP_col() == col)
                {
                    return h;
                }
            }
            return null;
        }

        private PathNode[] ExpandirHojas(PathNode[] hojas, DragPath arbol, MapMatrix mapa, bool bloquearMilitares, PathNode[] hojasContrarias, out PathNode? propio, out PathNode? contrario)
        {
            string[] lados = { "u", "d", "l", "r" };
            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };

            PathNode[] nuevasHojas = new PathNode[0];
            propio = null;
            contrario = null;

            foreach (PathNode hoja in hojas)
            {
                if (!hoja.getState()) continue;

                bool seExpandio = false;

                for (int i = 0; i < 4; i++)
                {
                    int nRow = hoja.getP_row() + dRow[i];
                    int nCol = hoja.getP_col() + dCol[i];

                    MapNode? vecino = mapa.GetMapNode(nRow, nCol);
                    if (vecino == null) continue;

                    bool esPared = vecino.getNodeType() == 1;
                    bool esMilitarBloqueado = bloquearMilitares && vecino.getNodeType() == 5;
                    if (esPared || esMilitarBloqueado) continue;

                    if (vecino.getState())
                    {
                        PathNode nuevo = arbol.addStep(hoja, nRow, nCol, lados[i])!;
                        vecino.setState(false);

                        Array.Resize(ref nuevasHojas, nuevasHojas.Length + 1);
                        nuevasHojas[nuevasHojas.Length - 1] = nuevo;
                        seExpandio = true;
                    }
                    else if (contrario == null)
                    {
                        PathNode? match = BuscarHojaViva(hojasContrarias, nRow, nCol);
                        if (match != null)
                        {
                            contrario = match;
                            propio = hoja;
                        }
                    }
                }

                if (!seExpandio) hoja.setState(false);
            }

            return nuevasHojas;
        }

        private PathNode[] ConstruirRuta(PathNode nodoInicio, PathNode nodoFinal)
        {
            int nIni = 0;
            PathNode? actual = nodoInicio;
            while (actual != null) { nIni++; actual = actual.getParent(); }

            int nFin = 0;
            actual = nodoFinal;
            while (actual != null) { nFin++; actual = actual.getParent(); }

            PathNode[] ruta = new PathNode[nIni + nFin];

            actual = nodoInicio;
            for (int i = nIni - 1; i >= 0; i--)
            {
                ruta[i] = actual!;
                actual = actual!.getParent();
            }

            actual = nodoFinal;
            for (int i = 0; i < nFin; i++)
            {
                ruta[nIni + i] = actual!;
                actual = actual!.getParent();
            }

            return ruta;
        }

        private Military? BuscarMilitarEnPosicion(int row, int col)
        {
            MilitaryNode? current = cityMilitaries.getHead();

            while (current != null)
            {
                Military m = current.getData();
                if (m.getM_Row() - 1 == row && m.getM_Column() - 1 == col)
                {
                    return m;
                }
                current = current.getNext();
            }

            return null;
        }

        private MapMatrix ClonarMapa(MapMatrix original, int rows, int columns)
        {
            MapMatrix copia = new MapMatrix(rows, columns);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    MapNode? nodoOriginal = original.GetMapNode(r, c);
                    MapNode? nodoCopia = copia.GetMapNode(r, c);

                    if (nodoOriginal != null && nodoCopia != null)
                    {
                        nodoCopia.setNodeType(nodoOriginal.getNodeType());
                        nodoCopia.setSymbol(nodoOriginal.getSymbol());
                        nodoCopia.setState(nodoOriginal.getState());
                    }
                }
            }

            return copia;
        }

        private void resolverCombateExtraccion(PathNode[] ruta, MapMatrix missionMap, Fighter missionRobot, int objetivoRow, int objetivoCol)
        {
            Military[] enfrentados = new Military[0];
            bool robotVivo = true;
            int marcarHasta = ruta.Length;

            for (int i = 0; i < ruta.Length; i++)
            {
                PathNode paso = ruta[i];
                Military? enemigo = BuscarMilitarEnPosicion(paso.getP_row(), paso.getP_col());

                if (enemigo != null)
                {
                    int dano = Math.Min(missionRobot.getCapacity(), enemigo.getM_Capacity());
                    missionRobot.setCapacity(missionRobot.getCapacity() - dano);
                    enemigo.setM_Capacity(enemigo.getM_Capacity() - dano);

                    Array.Resize(ref enfrentados, enfrentados.Length + 1);
                    enfrentados[enfrentados.Length - 1] = enemigo;

                    if (missionRobot.getCapacity() <= 0)
                    {
                        robotVivo = false;
                        marcarHasta = i + 1;
                        break;
                    }
                }
            }

            for (int i = 0; i < marcarHasta; i++)
            {
                missionMap.AddValue(ruta[i].getP_row(), ruta[i].getP_col(), 9, ".");
            }

            Console.Clear();
            missionMap.ShowConsoleMap();

            Console.WriteLine(robotVivo
                ? "\n>> MISION DE EXTRACCION EXITOSA <<"
                : "\n>> MISION FALLIDA: el robot fue destruido en combate <<");

            Console.WriteLine($"\nRobot utilizado: {missionRobot.getName()} ({missionRobot.getType()}) | Vida restante: {Math.Max(missionRobot.getCapacity(), 0)}");

            Console.WriteLine("\nUnidades militares enfrentadas:");
            foreach (Military m in enfrentados)
            {
                Console.WriteLine($" - Posición ({m.getM_Row()},{m.getM_Column()}) | Capacidad restante: {Math.Max(m.getM_Capacity(), 0)}");
            }

            MissionGraphExporter.ExportarMision(
                missionMap, misionCity.getC_Rows(), misionCity.getC_Columns(), misionCity.getName(),
                "Extracción", missionRobot.getName(), missionRobot.getType(), Math.Max(missionRobot.getCapacity(), 0), robotVivo,
                objetivoRow, objetivoCol, "Recurso extraído", enfrentados);
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