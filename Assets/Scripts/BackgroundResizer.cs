using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundResizer : MonoBehaviour, IMessageReceiver
{
    void Start()
    {
        MessageManager.StartReceivingMessage<ViewportResizeMessage>(this);
    }

    void Update()
    {
        
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is ViewportResizeMessage)
        {
            var viewportResizeMessage = message as ViewportResizeMessage;
            var width = (float)viewportResizeMessage.width;
            var height = (float)viewportResizeMessage.height;

            var radius = new Vector2(3.5f * 16.0f / 9.0f, 3.5f);
            GetComponent<MeshFilter>().sharedMesh = createBackgroundMesh(radius);
        }
    }

    Mesh createBackgroundMesh(Vector2 radius)
    {
        var positions = new Vector3[]
        {
            -radius,
            new Vector2(radius.x, -radius.y),
            radius,
            new Vector2(-radius.x, radius.y),
        };

        var aspect = radius.x / radius.y;

        var uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(aspect, 0),
            new Vector2(aspect, 1),
            new Vector2(0, 1)
        };

        var indices = new int[]
        {
            0, 2, 1,
            0, 3, 2
        };

        var mesh = new Mesh();
        mesh.vertices = positions;
        mesh.uv = uvs;
        mesh.triangles = indices;

        return mesh;
    }
}
