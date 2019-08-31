namespace Waypoints
{
    public class Station
    {
        /* The journey is assumed to be one-dimensional and
         * one-directional, hence one waypoint per station. */

        // Each station platform has a corresponding waypoint.
        public int W { get; set; }

        // Station 0 has waypoint 0, and the final station has the final waypoint.

        /* Efficiency may be improved by having only as many
         * waypoints per station for the number of directions
         * out of the station, rather than per platform.
         * Trains outbound in the same cardinal direction but at
         * different speed limits should count as two directions. */
    }
}