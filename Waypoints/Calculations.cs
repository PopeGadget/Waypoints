using System;
using System.Collections.Generic;

namespace Waypoints
{
    class Calculations
    {
        /* TODO:
         * > Include traffic as a factor for time.
         *  - Could add a TrafficScore property to the Waypoint class.
         * > Correct the conversion factor at line 24.
         *  - Currently assumes 1 mile per 100 Euclidian distance. */

        #region Distance calculations

        // Distance from waypoints A to B.
        public float getWDistance(Waypoint wA, Waypoint wB)
        {
            float wDistance;

            // Pythagorean theorem in three dimensions.
            wDistance = (float)Math.Abs(Math.Sqrt(Math.Pow(wB.X - wA.X, 2) + Math.Pow(wB.Y - wA.Y, 2) + Math.Pow(wB.Z - wA.Z, 2)));

            wDistance *= (float)(1.0 / 100.0); // Convert from Euclidian distance to in-game miles.
            return wDistance;
        }

        // Distance from stations A to B.
        public float getSDistance(Station sA, Station sB, List<Waypoint> waypoints)
        {
            float sDistance = 0;

            for (int i = sA.W + 1; i <= sB.W; i++)
            {
                // Sum waypoint distances between stations A and B.
                sDistance += getWDistance(waypoints[i - 1], waypoints[i]);
            }

            return sDistance;
        }

        // Distance from the first station to the last.
        public float getTotalDistance(List<Station> stations, List<Waypoint> waypoints)
        {
            float totalDistance = 0;

            for (int i = 1; i < stations.Count; i++)
            {
                // Sum station distances between the first and last stations.
                totalDistance += getSDistance(stations[i - 1], stations[i], waypoints);
            }

            /* Alternatively,
            for (int i = 1; i < waypoints.Count; i++)
            {
                // Sum waypoint distances between the first and last waypoints.
                totalDistance += getWDistance(waypoints[i - 1], waypoints[i]);
            }
            */

            return totalDistance;
        }
        #endregion

        #region Time calculations
        // Converted to minutes

        // Time taken to get from waypoints A to B.
        public float getWTime(Waypoint wA, Waypoint wB, int maxTrainSpeed)
        {
            float wTime;

            // Assume the speed limit is 5mph lower than it really is, to provide a bit of leeway.
            // Assume the train's max speed as the limit if the speed limit (- 5) is above the train's capabilities.
            // Time = Distance / Speed
            wTime = getWDistance(wA, wB) / Math.Min(wA.SpeedLimit - 5, maxTrainSpeed);
            
            wTime *= 60; // Convert from hours to minutes.
            return wTime;
        }

        // Time taken to get from stations A to B.
        public int getSTime(Station sA, Station sB, List<Waypoint> waypoints, int maxTrainSpeed)
        {
            float sTime = 0;

            for (int i = sA.W + 1; i <= sB.W; i++)
            {
                // Sum waypoint times between stations A and B.
                sTime += getWTime(waypoints[i - 1], waypoints[i], maxTrainSpeed);
            }

            sTime += (float)(1.0 / 3.0); // 20 seconds added to allow for loading times.
            sTime = (float)Math.Ceiling(sTime); // Round to highest minute.
            
            return (int)sTime;
        }

        // Time taken to get from the first station to the last.
        public int getTotalTime(List<Station> stations, List<Waypoint> waypoints, int maxTrainSpeed)
        {
            int totalTime = 0;

            for (int i = 1; i < stations.Count; i++)
            {
                // Sum station times between the first and last stations.
                totalTime += getSTime(stations[i - 1], stations[i], waypoints, maxTrainSpeed);
            }

            return totalTime;
        }
        #endregion
    }
}
