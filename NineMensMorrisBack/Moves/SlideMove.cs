using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NineMensMorrisBack.Model;

namespace NineMensMorrisBack.Moves
{
    public class SlideMove : IMove
    {
        private GameState _gameStatus;
        private Node _fromNode;
        private Node _toNode;

        public SlideMove(GameState gameStatus, Node fromNode, Node toNode)
        {
            _gameStatus = gameStatus;
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
