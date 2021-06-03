using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColourMapping
{

    public static Dictionary<ObjectOwner, Color> colormapping = new Dictionary<ObjectOwner, Color>()
    {
        { ObjectOwner.player0, Color.red },
        { ObjectOwner.player1, Color.yellow },
        { ObjectOwner.player2, Color.blue },
        { ObjectOwner.player3, Color.green },
        { ObjectOwner.neutral, Color.grey }
    };
}
