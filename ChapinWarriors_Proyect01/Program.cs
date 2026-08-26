using System;
using ChapinWarriors.Models;
using ChapinWarriors.Services;

class Program
{
    static void Main(string[] args)
    {
        DataProcess process = new DataProcess();

        RobotList<Robot> r_list = process.LoadRobotsXml();
        r_list.ShowRobotList();
    }
}