using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NineMensMorrisBack.Model;

namespace NineMensMorrisBack.Moves
{
    public class FlyMove : IMove
    {
        GameState _gameStatus;
        Node _fromNode;
        Node _toNode;

        public FlyMove(GameState gameState, Node fromNode, Node toNode)
        {
            _gameStatus = gameState;
            _fromNode = fromNode;
            _toNode = toNode;
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
