using System;
using ChapinWarriors.Models;
using ChapinWarriors.Services;

class Program
{
    static void Main(string[] args)
    {
        DataProcess process = new DataProcess();

        RobotList<Robot> r_list = process.LoadRobotsXml();
        CitiesList c_list = process.LoadCitiesXml();

        r_list.ShowRobotList();
        c_list.ShowCityList();
    }
}