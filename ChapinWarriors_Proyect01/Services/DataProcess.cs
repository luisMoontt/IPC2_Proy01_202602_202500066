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

                var cityElements = doc.Descendants("city");

                foreach( var c_element in cityElements)
                {
                    XElement? cityName = c_element.Element("name");
                    if ( cityName == null) continue;

                    string c_name = cityName.Value.Trim();
                    int rows = int.Parse(cityName.Attribute("rows")!.Value);
                    int cols = int.Parse(cityName.Attribute("columns")!.Value);

                    City newCity = new City(c_name, rows, cols);
                    MapMatrix map = new MapMatrix(rows, cols);

                    var rowElements = c_element.Elements("row");

                    foreach(var r_element in rowElements)
                    {
                        int xmlRowIndex = int.Parse(r_element.Attribute("number")!.Value) - 1; 
                        string lineContent = r_element.Value.Trim();

                        for (int colIndex = 0; colIndex < lineContent.Length && colIndex < cols; colIndex++)
                        {
                            string charSymbol = lineContent[colIndex].ToString();
                            
                            map.AddValue(xmlRowIndex, colIndex, 0, charSymbol);
                        }
                    }
                    newCity.setMap(map);


                    MilitaryList m_list = new MilitaryList();
                    var militaryElements = c_element.Elements("militaryU");

                    foreach (var mElem in militaryElements)
                    {
                        int m_capacity = int.Parse(mElem.Value.Trim());
                        int m_row = int.Parse(mElem.Attribute("row")!.Value);
                        int m_col = int.Parse(mElem.Attribute("column")!.Value);

                        Military unit = new Military(m_capacity, m_row, m_col);
                        m_list.AddMilitary(unit);
                    }
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
                    XElement? nameNode = element.Element("name");
                    if (nameNode != null)
                    {
                        string nameRobot = nameNode.Value.Trim();
                        string typeRobot = nameNode.Attribute("type")?.Value ?? "Desconocido";

                        if(typeRobot == "Fighter")
                        {
                            int capacityRobot = int.Parse(nameNode.Attribute("capacity")!.Value);
                            r_list.Add(new Fighter(nameRobot, typeRobot, capacityRobot)); 
                        } 
                        else if(typeRobot == "Rescue")
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