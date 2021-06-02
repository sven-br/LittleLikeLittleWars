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
        set { if (value > 0 && value <= 1) sendPercentage = value; else Debug.Log("Illegal value for sendPercentage!"); }
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
            Star.Owner owner = starClickedMessage.owner;
            Debug.Log("Star was clicked: " + clickedOn);

            switch (starSelectionState)
            {
                case StarSelectionState.Unselected:
                    if (owner != Star.Owner.player0)
                    {
                        Debug.Log("Star does not belong to player!");
                    }
                    else
                    {
                        selectedSender = clickedOn;
                        starSelectionState = StarSelectionState.SenderSelected;

                        var starSelectedMessage = MessageProvider.GetMessage<StarSelectedMessage>();
                        starSelectedMessage.star = selectedSender;
                        MessageManager.SendMessage(starSelectedMessage);

                        Debug.Log("Star " + clickedOn + " selected!");
                    }
                    break;

                case StarSelectionState.SenderSelected:
                    {
                        if (selectedSender.HasNeighbor(clickedOn))
                        {
                            var msg = MessageProvider.GetMessage<UnitTransferMessage>();
                            msg.sender = selectedSender;
                            msg.receiver = clickedOn;
                            msg.amount = (int)(selectedSender.Units * sendPercentage);
                            msg.owner = selectedSender.getOwner();
                            MessageManager.SendMessage(msg);
                            starSelectionState = StarSelectionState.Unselected;

                            var starUnselectedMessage = MessageProvider.GetMessage<AllStarsUnselectedMessage>();
                            MessageManager.SendMessage(starUnselectedMessage);
                        }

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
