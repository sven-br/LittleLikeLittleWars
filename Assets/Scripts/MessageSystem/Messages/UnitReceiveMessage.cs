using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitReceiveMessage : Message
{
	public Star receiver;
	public int amount;
	public Star.StarOwner owner;
}
