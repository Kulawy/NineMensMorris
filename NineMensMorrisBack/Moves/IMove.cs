using NineMensMorrisBack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Moves
{
    public interface IMove
    {
        
        bool IsLegal();
        void MakeMove();
        //void MakeMove(GameState gameState);

    }
}
