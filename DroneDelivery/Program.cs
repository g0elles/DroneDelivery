using DroneDelivery.Helpers;
using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DroneDelivery
{
    class Program
    {
        static void Main(string[] args)
        {
            string drones = File.ReadLines(@".\data\data.csv").First();
            List<Drone> dronefloat = new List<Drone>();
            List<Location> locations = new List<Location>();
            string[] droneArray = drones.Split(',');
            scheduler sche = new scheduler();
            List<string> locationList = File.ReadLines(@".\data\data.csv").Skip(1).ToList();
            for (int i = 1; i <= droneArray.Length; i++)
            {
                if (i % 2 == 0)
                {}
                else
                {
                    Drone dr = new Drone
                    {
                        name = droneArray[i-1],
                        maxWeight = Convert.ToInt32(droneArray[i])
                    };
                    dronefloat.Add(dr);
                }               
            }

            foreach (string item in locationList)
            {
                Location loc = new Location { name=item.Split(',')[0],weight = Convert.ToInt32(item.Split(',')[1])};
                locations.Add(loc);
            }

            List<Schedule> schedules = new List<Schedule>();


            do
            {
                foreach (Drone d in dronefloat)
                {
                    Schedule sch = new Schedule();
                    var val = schedules.FirstOrDefault(x => x.drone.name == d.name);
                    var index = -1;
                    if (val != null)
                    {
                        sch = schedules.FirstOrDefault(x => x.drone.name == d.name);
                        index = schedules.IndexOf(sch);
                    }
                    else
                    {
                        sch.drone = d;
                        sch.locations = new List<List<Location>>();
                    }
                    List<Location> loc = new List<Location>();
                    List<Location> loc2 = sche.pickLocationsForCapacity(d.maxWeight, locations, out loc);
                    if (loc2 != null)
                    {
                        sch.locations.Add(loc2);
                    }

                    locations = loc;
                    if (index >= 0)
                    {
                        schedules[index] = sch;
                    }
                    else
                    {
                        schedules.Add(sch);
                    }
                   

                }
            } while (locations.Count != 0);



            foreach (var DroneSchedule in schedules.Select((sched,index)=>(sched,index)))
            {

                Console.WriteLine($"[Dron #{DroneSchedule.index+1} {DroneSchedule.sched.drone.name}]\n");
                foreach (var trip in DroneSchedule.sched.locations.Select((tr,index)=>(tr,index)))
                {
                    if (trip.tr.Count > 0)
                    {
                        Console.WriteLine($"Trip #{trip.index + 1}\n");
                        var finaltrip = string.Join(",", trip.tr.Select((tr1, index) => $"[Location #{index + 1} {tr1.name}]"));
                        Console.WriteLine($"{finaltrip.ToString()}\n");
                    }
                  
                }

            }

            Console.WriteLine("\n Por favor presione una tecla parar culminar");
            Console.ReadKey();









        }

        

    }
}
