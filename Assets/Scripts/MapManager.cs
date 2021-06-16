using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, IMessageReceiver
{
    private MapState mapstate;

    // Start is called before the first frame update
    void Start()
    {
        //get initial state of map (from gamemanager?)

        MessageManager.StartReceivingMessage<UnitSendMessage>(this);
        MessageManager.StartReceivingMessage<UnitReceiveMessage>(this);
        MessageManager.StartReceivingMessage<StarOwnerChangedMessage>(this);
        MessageManager.StartReceivingMessage<SpaceFightMessage>(this);
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        switch (message)
        {
            case StarOwnerChangedMessage starownerchangedmsg:
                break;
            case UnitSendMessage unitsendmsg:
                break;
            case UnitReceiveMessage unitreceivemsg:
                break;
            case SpaceFightMessage spacefightmsg:
                break;
        }

        var newMapState = MessageProvider.GetMessage<MapChangedMessage>();
        newMapState.mapstate = mapstate;
        MessageManager.SendMessage(newMapState);
    }
}
