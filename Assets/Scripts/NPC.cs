using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IMessageReceiver
{
    public ObjectOwner player;
    public bool isAlive = true;
    private Star[] starsOwned;
    private int unitsOwned;

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is TickMessage)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MessageManager.StartReceivingMessage<MapChangedMessage>(this);
    }

    /*
    
    // Update is called once per frame
    void Update()
    {
        if (CheckAlive())
        {
            ChooseActionsForStars();
        }
        else
        {
            Debug.Log(player + " is defeated!");
            Destroy(gameObject);
        }

    }

    private bool CheckAlive()
    {
        if (starsOwned.Length == 0 && unitsOwned == 0) isAlive = false;
        return isAlive;
    }

    private void ChooseActionsForStars()
    {
        foreach(Star star in starsOwned)
        {
            private Dictionary<Star, int> scores = new Dictionary<Star, int>();
            
            foreach (Star neighbour in star.neighbors)
            {
                scores.add(new KeyValuePair<Star, int>(neighbour, CalculateScore(neighbour));
            }
            int max = scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            SendUnits(scors[max], star, star.Units); // erst alle messages sammeln und am ende losschicken? gibts sonst komische sync-effekte?
        }
    }

    private int CalculateScore(Star star)
    {
        int score = 0;
        return score;
    }

    private void SendUnits(Star receiver, Star sender, int amount)
    {
        var msg = MessageProvider.GetMessage<UnitSendMessage>();
        msg.sender = sender;
        msg.receiver = receiver;
        msg.amount = amount;
        msg.owner = player;
        MessageManager.SendMessage(msg);
    }
    */
}
