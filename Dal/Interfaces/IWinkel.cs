using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Interfaces
{
    public interface IWinkel
    {
        bool KanItemKopen(int item_id, int user_id);
    }
}
