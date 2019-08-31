using System;
using System.Collections.Generic;

namespace Waypoints
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Station> stations = new List<Station>();
            List<Waypoint> waypoints = new List<Waypoint>();
            int wCount, sCount;

            int maxTrainSpeed;

            Calculations calc = new Calculations();

            string input;


            #region Manual inputs
            // This region can be replaced by values fetched from elsewhere.

            Console.WriteLine("Input the following as integers.\n");

            // Ask for the train's top speed.
            // Slower trains will be given more lenient schedules.
            Console.Write("Input the train's top speed: ");
            input = Console.ReadLine();
            maxTrainSpeed = Convert.ToInt16(input);

            // Ask for the number of stations and waypoints.
            do {
                Console.Write("Input the number of stations (>= 2): ");
                input = Console.ReadLine();
                sCount = Convert.ToInt16(input);

                if (sCount < 2)
                {
                    Console.WriteLine(" sCount < 2. Try again.");
                }
            } while (sCount < 2);

            do {
                Console.Write("Input the number of waypoints (>= stations): ");
                input = Console.ReadLine();
                wCount = Convert.ToInt16(input);

                if (wCount < sCount)
                {
                    Console.WriteLine(" wCount < sCount. Try again.");
                }
            } while (wCount < sCount);

            // Guaranteed stations at first and last waypoints.
            stations.Add(new Station() { W = 0 });
            stations.Add(new Station() { W = (wCount - 1) });

            // Ask for other station locations.
            for (int j = 2; j <= sCount; j++)
            {
                string waypointStr = "\n";
                string stationStr = "";

                // Ensure stations are ordered properly.
                stations.Sort((x, y) => x.W - y.W);

                int k = 0;
                for (int i = 0; i < wCount; i++)
                {
                    if (i < wCount - 1)
                    {
                        waypointStr += i + "-";
                    }
                    else
                    {
                        waypointStr += i + " ";
                    }

                    bool match = false;
                    foreach (Station s in stations)
                    {
                        if (s.W == i) // If there is a station at waypoint i.
                        {
                            if (s.W < wCount - 1) // If station is not at the final waypoint.
                            {
                                stationStr += k + " ";
                            }
                            else
                            {
                                stationStr += (sCount - 1) + " ";
                            }
                            match = true;
                            k++;
                        }
                    }

                    if (match == false)
                    {
                        stationStr += "  ";
                    }
                }

                Console.WriteLine(waypointStr + "(waypoints)");
                Console.WriteLine(stationStr + "(stations)");

                if (j < sCount)
                {
                    Console.Write("\nInput what waypoints have a station ({0} left): ", sCount - j);
                    input = Console.ReadLine();
                    stations.Add(new Station() { W = Convert.ToInt16(input) });
                }
            }

            // Ask for waypoint specifics.
            for (int i = 0; i < wCount; i++)
            {
                Console.Write("\nInput coordinates for waypoint {0} (\"x,y,z\"): ", i);
                input = Console.ReadLine();
                string[] coordinates = input.Split(',');
                // I'm assuming Roblox treats coordinates as decimals. Should still work as integers.
                double _X = Convert.ToDouble(coordinates[0]);
                double _Y = Convert.ToDouble(coordinates[1]);
                double _Z = Convert.ToDouble(coordinates[2]);

                Console.Write("Input speed limit for waypoint {0}: ", i);
                input = Console.ReadLine();
                int _SpeedLimit = Convert.ToInt16(input);
                waypoints.Add(new Waypoint() { X = _X, Y = _Y, Z = _Z, SpeedLimit = _SpeedLimit });
            }
            #endregion


            #region Distances output
            Console.WriteLine("\n\n=== OUTPUT ===\n");

            // Distance from waypoints A to B.
            for (int i = 1; i < wCount; i++)
	    {
		float wDistance = calc.getWDistance(waypoints[i - 1], waypoints[i]);
                Console.WriteLine("Distance between waypoints {0} and {1} = {2} miles", i - 1, i, wDistance);
            }

            Console.WriteLine();

            // Distance from stations A to B.
            for (int i = 1; i < sCount; i++)
	    {
		float sDistance = calc.getSDistance(stations[i - 1], stations[i], waypoints);
                Console.WriteLine("Distance between stations {0} and {1} = {2} miles", i - 1, i, sDistance);
            }

            // Distance from the first station to the last.
            float totalDistance = calc.getTotalDistance(stations, waypoints);
            Console.Write("\nTotal distance = {0} miles\n\n", totalDistance);
            #endregion

            #region Times output

            // Time taken to get from waypoints A to B.
            for (int i = 1; i < wCount; i++)
	    {
	        float wTime = calc.getWTime(waypoints[i - 1], waypoints[i], maxTrainSpeed);
                Console.WriteLine("Time between waypoints {0} and {1} = {2} minutes", i - 1, i, wTime);
	    }

            Console.WriteLine();

            // Time taken to get from stations A to B.
            for (int i = 1; i < sCount; i++)
	    {
	        int sTime = calc.getSTime(stations[i - 1], stations[i], waypoints, maxTrainSpeed);
	        Console.WriteLine("Time between stations {0} and {1} = {2} minutes", i - 1, i, sTime);
	    }

            // Time taken to get from the first station to the last.
            int totalTime = calc.getTotalTime(stations, waypoints, maxTrainSpeed);
            Console.Write("\nTotal time = {0} minutes", totalTime);
            #endregion

            Console.ReadLine();
        }
    }
}
