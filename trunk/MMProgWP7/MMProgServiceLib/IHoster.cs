using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMProgServiceLib
{
    public interface IHoster
    {


        IList<MyMessageDTO> GetDTOUntilGuid(Guid? id, int maxnum);


    }
}
