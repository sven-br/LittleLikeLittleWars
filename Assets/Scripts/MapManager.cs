using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private Star[] stars = null;
    [SerializeField] private Link[] links = null;

    private MapState mapState;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

        //get initial state of map (from gamemanager?)

        MessageManager.StartReceivingMessage<UnitSendMessage>(this);
        MessageManager.StartReceivingMessage<UnitReceiveMessage>(this);
        MessageManager.StartReceivingMessage<StarOwnerChangedMessage>(this);
        MessageManager.StartReceivingMessage<SpaceFightMessage>(this);
    }

    void Initialize()
    {
        mapState = new MapState {
            stars = InitializeStars(stars),
            links = InitializeLinks(link),
        };
    }

    StarState[] InitializeStars(Star[] stars)
    {
        var starState = new StarState[stars.Length];
        for (var i = 0; i < star.Length; ++i)
        {
            starState[i] = StarState(i, star);
            // star.setId(i);
        }
        return starState;
    }

    LinkState[] InitializeLinks(Link[] links)
    {
        var linkState = new LinkState();
        int i = 0;
        foreach (var link in links)
        {
            linkState.Add(link.star0.id, link.star1.id);
        }
        return linkState;
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
        newMapState.mapState = mapstate;
        MessageManager.SendMessage(newMapState);
    }

        // MessageManager.StartReceivingMessage<TickMessage>(this);
        // MessageManager.StartReceivingMessage<UnitReceiveMessage>(this);
}
