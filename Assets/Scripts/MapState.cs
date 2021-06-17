using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State {
    using StarId = System.Int32;
    using StarIdPair = System.Tuple<StarId, StarId>;
    using LinkState = System.Collections.Generic.HashSet<StarIdPair>;

    public struct StarState
    {
        StarId id;
        int units;
        ObjectOwner owner;
        Star.SpawnInterval interval;

        public StarState(StarId id, Star star)
        {
            units = star.units;
            owner = star.owner;
            interval = star.interval;
        }
    }

    public struct MapState
    {
        public StarState[] stars;
        public LinkState links;

        bool AreStarsLinked(StarId a, StarId b)
        {
            return links.Contains((a, b));
        }
    }

}
