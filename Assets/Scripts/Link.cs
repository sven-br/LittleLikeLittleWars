using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField] private Star star0 = null;
    [SerializeField] private Star star1 = null;
    private bool registered = false;

    void Start()
    {
        GetComponent<MeshFilter>().sharedMesh = createLinkMesh(star0.transform.position, star1.transform.position);
    }

    void Update()
    {
        if (!registered)
        {
            registered = true;
            var message = MessageProvider.GetMessage<RegisterLinkMessage>();
            message.link = this;
            MessageManager.SendMessage(message);
        }
    }

    public Star GetOtherStar(Star that)
    {
        if (that == star0)
            return star1;
        if (that == star1)
            return star0;
        return null;
    }

    Mesh createLinkMesh(Vector2 position0, Vector2 position1)
    {
        var mesh = new Mesh();

        var dir2 = (position1 - position0);
        var length = dir2.magnitude;
        dir2 = dir2.normalized;
        var dir = new Vector3(dir2.x, dir2.y, 0);
        var right = Vector3.Cross(dir, new Vector3(0, 0, -1));

        var pos0 = new Vector3(position0.x, position0.y, 0);
        var pos1 = new Vector3(position1.x, position1.y, 0);

        var radius = 0.2f;
        dir *= radius;
        right *= radius;

        var positions = new Vector3[]
        {
            pos0 - dir - right,
            pos0 - dir + right,
            pos1 + dir + right,
            pos1 + dir - right
        };

        var uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, length / radius / 2.0f),
            new Vector2(0, length / radius / 2.0f)
        };

        var indices = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };

        mesh.vertices = positions;
        mesh.uv = uvs;
        mesh.triangles = indices;

        return mesh;
    }

    void OnDrawGizmos()
    {
        var from = star0.transform.position;
        var to = star1.transform.position;
        var dir = (to - from).normalized;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(from, to);
    }
}
