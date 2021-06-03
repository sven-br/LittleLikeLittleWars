using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField] private Star star0;
    [SerializeField] private Star star1;
    private bool registered = false;

    void Start()
    {
        
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

    void OnDrawGizmos()
    {
        var from = star0.transform.position;
        var to = star1.transform.position;
        var dir = (to - from).normalized;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(from, to);
    }
}
