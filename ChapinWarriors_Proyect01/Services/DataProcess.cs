using System;
using System.IO;
using System.Xml.Linq;
using ChapinWarriors.Models;

namespace ChapinWarriors.Services
{
    public class DataProcess
    {
        public XDocument? loadDoc(){
            string relativePath = "Data/config.XML";

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            }
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"No se encontró el archivo XML en {fullPath}");
                return null;
            }

            XDocument doc = XDocument.Load(fullPath);
            return doc;
        }
        
        
        /////////////////////////////////////////////////////////////////////////////////
        
        public CitiesList LoadCitiesXml()
        {
            CitiesList c_list = new CitiesList();

            try
            {
                XDocument doc = loadDoc();
                if (doc == null){
                    return null;
                }

                var cityElements = doc.Descendants("ciudad");

                foreach( var c_element in cityElements)
                {
                    XElement? cityName = c_element.Element("nombre");
                    if ( cityName == null) continue;

                    string c_name = cityName.Value.Trim();
                    int rows = int.Parse(cityName.Attribute("filas")!.Value);
                    int cols = int.Parse(cityName.Attribute("columnas")!.Value);

                    City newCity = new City(c_name, rows, cols);
                    MapMatrix map = new MapMatrix(rows, cols);

                    var rowElements = c_element.Elements("fila");

                    foreach(var r_element in rowElements)
                    {
                        int xmlRowIndex = int.Parse(r_element.Attribute("numero")!.Value) - 1; 
                        string lineContent = r_element.Value;

                        for (int colIndex = 0; colIndex < lineContent.Length && colIndex < cols; colIndex++)
                        {
                            string charSymbol = lineContent[colIndex].ToString();
                            
                            switch(charSymbol)
                            {
                                case "*":
                                    map.AddValue(xmlRowIndex, colIndex, 1, charSymbol);
                                    break;
                                case "E":
                                    map.AddValue(xmlRowIndex, colIndex, 2, charSymbol);
                                    break;
                                case "C":
                                    map.AddValue(xmlRowIndex, colIndex, 3, charSymbol);
                                    break;
                                case "R":
                                    map.AddValue(xmlRowIndex, colIndex, 4, charSymbol);
                                    break;
                                default:
                                    map.AddValue(xmlRowIndex, colIndex, 0, charSymbol);
                                    break;
                            }
                        }
                    }

                    MilitaryList m_list = new MilitaryList();
                    var militaryElements = c_element.Elements("unidadMilitar");

                    foreach (var mElem in militaryElements)
                    {
                        int m_capacity = int.Parse(mElem.Value.Trim());
                        int m_row = int.Parse(mElem.Attribute("fila")!.Value);
                        int m_col = int.Parse(mElem.Attribute("columna")!.Value);

                        Military unit = new Military(m_capacity, m_row, m_col);

                        map.AddValue(m_row-1, m_col-1, 5, "M");

                        m_list.AddMilitary(unit);
                    }
                    
                    newCity.setMap(map);
                    newCity.setMList(m_list);

                    c_list.AddCity(newCity);
                    Console.WriteLine($"Ciudad '{cityName}' cargada exitosamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al leer el XML de ciudades: {ex.Message}");
            }

            return c_list;
        }

        public RobotList<Robot> LoadRobotsXml()
        {
            RobotList<Robot> r_list = new RobotList<Robot>();

            try
            {
                XDocument doc = loadDoc();
                if (doc == null){
                    return null;
                }

                var robotElemets = doc.Descendants("robot");

                foreach (var element in robotElemets)
                {
                    XElement? nameNode = element.Element("nombre");
                    if (nameNode != null)
                    {
                        string nameRobot = nameNode.Value.Trim();
                        string typeRobot = nameNode.Attribute("tipo")?.Value ?? "Desconocido";

                        if(typeRobot == "ChapinFighter")
                        {
                            int capacityRobot = int.Parse(nameNode.Attribute("capacidad")!.Value);
                            r_list.Add(new Fighter(nameRobot, typeRobot, capacityRobot)); 
                        } 
                        else if(typeRobot == "ChapinRescue")
                        {
                            r_list.Add(new Rescue(nameRobot, typeRobot));
                        }
                        else if(typeRobot == "ElChimatron")
                        {
                            int capacityRobot = int.Parse(nameNode.Attribute("capacity")!.Value);
                            r_list.Add(new ElChimatron(nameRobot, typeRobot, capacityRobot)); 
                        }
                    }

                } 
            }
            catch (Exception ex){
                Console.WriteLine($"Ocurrió un error al leer el XML: {ex.Message}");
            }
            
            return r_list;
        }


    }
}