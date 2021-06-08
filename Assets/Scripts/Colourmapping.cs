using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColourMapping
{

    public static Dictionary<ObjectOwner, Color> colormapping = new Dictionary<ObjectOwner, Color>()
    {
        { ObjectOwner.player0, new Color(15 / 255.0f, 78 / 255.0f, 180 / 255.0f) },
        { ObjectOwner.player1, new Color(184 / 255.0f, 78 / 255.0f, 20 / 255.0f) },
        { ObjectOwner.player2, Color.yellow },
        { ObjectOwner.player3, Color.green },
        { ObjectOwner.neutral, new Color(99 / 255.0f, 98 / 255.0f, 64 / 255.0f) }
    };
}
