using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeListener : MonoBehaviour
{
    private int width = 0;
    private int height = 0;

    void Start()
    {
        
    }

    void Update()
    {
        var size = GetComponent<Camera>().pixelRect;

        if (size.width != width || size.height != height)
        {
            width = (int)size.width;
            height = (int)size.height;
            var message = MessageProvider.GetMessage<ViewportResizeMessage>();
            message.width = width;
            message.height = height;
            MessageManager.SendMessage(message);
        }
    }
}
