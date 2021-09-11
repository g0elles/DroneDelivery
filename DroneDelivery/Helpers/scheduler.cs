using DroneDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DroneDelivery.Helpers
{
    class scheduler
    {
        public  List<Location> pickLocationsForCapacity(int capacity, List<Location> locations, out List<Location> finalLocation)
        {
            List<Location> selectedLocations = new List<Location>();
            finalLocation = new List<Location>(locations);
            int currentCapacity = capacity;
            foreach (Location item in locations)
            {
                if (item.weight <= currentCapacity)
                {
                    selectedLocations.Add(item);
                    currentCapacity = currentCapacity - item.weight;
                    finalLocation.RemoveAll(x => x.name == item.name);
                }
            }

            return selectedLocations;
        }

    }
}
