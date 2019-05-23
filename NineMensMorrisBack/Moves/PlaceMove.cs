using NineMensMorrisBack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Moves
{
    public class PlaceMove : IMove
    {
        private Node _pickedNode;
        private GameState _gameStatus;

        public PlaceMove(GameState gameStatus, Node pickedNode)
        {
            _gameStatus = gameStatus;
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
