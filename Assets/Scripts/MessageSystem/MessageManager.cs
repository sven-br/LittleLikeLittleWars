using System;
using System.Collections.Generic;

public static class MessageManager
{
	private static Dictionary<Type, HashSet<IMessageReceiver>> _messages;

	private static List<IMessageReceiver> _receiverList;

	public static void StartReceivingMessage<T>(IMessageReceiver receiver) where T : Message
	{
		if (_messages == null)
		{
			_messages = new Dictionary<Type, HashSet<IMessageReceiver>>();
		}

		var messageType = typeof(T);

		if (!_messages.ContainsKey(messageType))
		{
			_messages.Add(messageType, new HashSet<IMessageReceiver>());
		}

		if (!_messages[messageType].Contains(receiver))
		{
			_messages[messageType].Add(receiver);
		}
	}

	public static void StopReceivingMessage<T>(IMessageReceiver receiver) where T : Message
	{
		var messageType = typeof(T);
		if (_messages.ContainsKey(messageType) && _messages[messageType].Contains(receiver))
		{
			_messages[messageType].Remove(receiver);
		}
	}

	public static void StopReceivingAllMessages(IMessageReceiver receiver)
	{
		foreach (var messageList in _messages.Values)
		{
			if (messageList.Contains(receiver))
			{
				messageList.Remove(receiver);
			}
		}
	}

	public static void Clear()
	{
		if (_messages != null)
        {
			_messages.Clear();
		}

		if (_receiverList != null)
		{
			_receiverList.Clear();
		}
	}

	public static void SendMessage(Message message)
	{
		if (!_messages.ContainsKey(message.GetType())) return;

		if (_receiverList == null)
		{
			_receiverList = new List<IMessageReceiver>();
		}
		else
		{
			_receiverList.Clear();
		}
		
		_receiverList.AddRange(_messages[message.GetType()]);
		
		message.Init(_receiverList.Count); // set number of references (same as number of receivers)

		for (int i = 0; i < _receiverList.Count; i++)
		{
			_receiverList[i].MessageReceived(message);
		}
	}
	
}
