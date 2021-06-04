using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private GameObject unitPrefab;

    void Start()
    {
        MessageManager.StartReceivingMessage<UnitSendMessage>(this);
    }

    void Update()
    {
        
    }

    void IMessageReceiver.MessageReceived(Message message)
    {
        if (message is UnitSendMessage)
        {
            var unitSendMessage = message as UnitSendMessage;
            var unitObject = Instantiate(unitPrefab, transform);
            var unit = unitObject.GetComponent<Unit>();
            unit.Sender = unitSendMessage.sender;
            unit.Owner = unitSendMessage.sender.Owner;
            unit.Amount = unitSendMessage.amount;
            unit.transform.position = unitSendMessage.sender.transform.position;
            unit.transform.localPosition = new Vector3(unit.transform.localPosition.x, unit.transform.localPosition.y, 0);
            unit.Direction = (unitSendMessage.receiver.transform.position - unitSendMessage.sender.transform.position).normalized;
        }
    }
}
