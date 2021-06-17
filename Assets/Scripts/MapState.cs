using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StarState
{
    int id;
    int units;
    ObjectOwner owner;
    Star.SpawnInterval interval;

    public StarState(int _id, Star star)
    {
        id = _id;
        units = star.units;
        owner = star.owner;
        interval = star.interval;
    }
}

public struct MapState
{
    public StarState[] stars;
    public HashSet<(int, int)> links;

    bool AreStarsLinked(int a, int b)
    {
        return links.Contains((a, b));
    }

    StarState GetStarStateById(int starId)
    {
        return stars[starId];
    }
}
