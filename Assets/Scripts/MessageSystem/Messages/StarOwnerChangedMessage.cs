using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StarOwnerChangedMessage : Message
{
    public MapState state;

    public int starId;
    public ObjectOwner starOwner;
}
