using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Star[] stars;
    public Link[] links;

    void Start()
    {
        foreach (var star in stars)
        {
            MessageManager.StartReceivingMessage<UnitTransferMessage>(star);
        }

        foreach (var link in links)
        {
            MessageManager.StartReceivingMessage<UnitTransferMessage>(link);
        }
    }

    void Update()
    {
        
    }
}
