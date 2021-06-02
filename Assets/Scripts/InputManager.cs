using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IMessageReceiver
{
    enum StarSelectionState
    {
        Unselected,
        SenderSelected,
        SenderAndReceiverSelected,
    }

    StarSelectionState starSelectionState = StarSelectionState.Unselected;

    Star selectedSender;
    Star selectedReceiver;

    void Start()
    {
        MessageManager.StartReceivingMessage<StarClickedMessage>(this);
    }

    void Update()
    {
        
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        var starClickedMessage = (StarClickedMessage)message;
        if (starClickedMessage != null)
        {
            var star = starClickedMessage.star;
            Debug.Log("Star was clicked: " + star);
            Debug.Log("Current StarSelectedState: " + starSelectionState);
        }
    }
}
