using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IMessageReceiver
{
    enum StarSelectionState
    {
        Unselected,
        SenderSelected,
    }

    StarSelectionState starSelectionState = StarSelectionState.Unselected;

    Star selectedSender;

    private float sendPercentage = 1;

    private float SendPercentage
    {
        get { return sendPercentage; }
        set { if (value > 0 && value <= 1) sendPercentage = value; else Debug.Log("Illegal value for sendPercentage!") }
    }

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
            Star clickedOn = starClickedMessage.star;
            Debug.Log("Star was clicked: " + clickedOn);

            switch (starSelectionState)
            {
                case StarSelectionState.Unselected:
                    selectedSender = clickedOn;
                    starSelectionState = StarSelectionState.SenderSelected;
                    Debug.Log("Star" + clickedOn + " selected!");
                    break;

                case StarSelectionState.SenderSelected:
                    {
                    var msg = MessageProvider.GetMessage<UnitTransferMessage>();
                    msg.sender = selectedSender;
                    msg.receiver = clickedOn;
                    msg.amount = (int)(selectedSender.Units * sendPercentage);
                    MessageManager.SendMessage(msg);
                    starSelectionState = StarSelectionState.Unselected;
                    break;
                    }
                    
                default:
                    Debug.Log("Nothing happened upon click in the current input-state!");
                    break;
            }


            Debug.Log("Current StarSelectedState: " + starSelectionState);
        }
    }
}
