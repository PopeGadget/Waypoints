namespace Waypoints
{
    public class Waypoint
    {
        /* Waypoints can either be a new addition or an attachment to
         * the already placed signals and speed limit triggers. */

        // Coordinates in 3D space.
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Speed limit for section of track directly after the waypoint.
        public int SpeedLimit { get; set; }
        /* This value for the final waypoint shouldn't matter, but I
         * imagine for the general case fetching the SpeedLimit value
         * of the previous waypoint and copying it over would make
         * the most sense, and change the value if one is specified
         * whilst passing in the waypoints list. */
    }
}
