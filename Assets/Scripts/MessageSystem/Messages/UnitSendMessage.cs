using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitSendMessage : Message
{
	public Star sender;
	public Star receiver;
	public int amount;
	public ObjectOwner owner;
}
