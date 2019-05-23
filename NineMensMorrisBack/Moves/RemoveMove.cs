using NineMensMorrisBack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Moves
{
    public class RemoveMove : IMove
    {
        private Node _pickedNode;

        public RemoveMove(GameState gameStatus, Node pickedNode)
        {
            _pickedNode = pickedNode;
        }

        public bool IsLegal()
        {
            throw new NotImplementedException();
        }

        public void MakeMove()
        {
            throw new NotImplementedException();
        }
    }
}
