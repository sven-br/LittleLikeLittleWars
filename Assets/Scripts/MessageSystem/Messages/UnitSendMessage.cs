using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitSendMessage : Message
{
	public int senderID;
	public int receiverID;
	public int amount;
	public ObjectOwner owner;
}
