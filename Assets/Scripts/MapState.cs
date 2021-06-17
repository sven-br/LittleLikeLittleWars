using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StarState
{
    public int id;
    public int units;
    public ObjectOwner owner;
    public Star.SpawnInterval interval;

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

    public bool AreStarsLinked(int a, int b)
    {
        return links.Contains((a, b));
    }

    public StarState GetStarStateById(int starId)
    {
        return stars[starId];
    }
}
