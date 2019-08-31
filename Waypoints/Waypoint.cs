namespace Waypoints
{
    public class Waypoint
    {
        /* Waypoints can either be a new addition
         * or an attachment to the already placed
         * signals and speed limit sensors. */

        // Coordinates in 3D space.
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // Speed limit for section of track directly after the waypoint.
        public int SpeedLimit { get; set; }
    }
}
