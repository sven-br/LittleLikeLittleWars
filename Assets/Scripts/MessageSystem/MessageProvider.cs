using System;
using System.Collections.Generic;

public static class MessageProvider
{
	private static Dictionary<Type, Queue<Message>> _messagePool;

	public static T GetMessage<T>() where T : Message, new()
	{
		var messageType = typeof(T);
		if (_messagePool == null)
		{
			_messagePool = new Dictionary<Type, Queue<Message>>();
		}

		if (!_messagePool.ContainsKey(messageType) || _messagePool[messageType].Count == 0)
		{
			return new T();
		}

		return _messagePool[messageType].Dequeue() as T;
	}

	public static void RecycleMessage(Message message)
	{
		var messageType = message.GetType();
		
		if (!_messagePool.ContainsKey(messageType))
		{
			_messagePool.Add(messageType, new Queue<Message>());
		}
		
		_messagePool[messageType].Enqueue(message);
	}
}
