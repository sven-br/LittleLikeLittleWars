using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StarOwnerChangedMessage : Message
{
    public int id;
    public ObjectOwner owner;

    public MapState state;
}
