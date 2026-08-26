using System;
using System.IO;
using System.Xml.Linq;
using ChapinWarriors.Models;

namespace ChapinWarriors.Services
{
    public class DataProcess
    {
        public RobotList<Robot> LoadRobotsXml(string relativePath = "Data/config.XML")
        {
             RobotList<Robot> r_list = new RobotList<Robot>();
        
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            }
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"No se encontró el archivo XML en {fullPath}");
                return r_list;
            }

            try
            {
                XDocument doc = XDocument.Load(fullPath);

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