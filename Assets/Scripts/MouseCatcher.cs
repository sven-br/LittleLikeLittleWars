using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCatcher : MonoBehaviour
{
    private void OnMouseDown()
    {
        var message = MessageProvider.GetMessage<AllStarsUnselectedMessage>();
        MessageManager.SendMessage(message);
        Debug.Log("hahahaa");
    }
}
