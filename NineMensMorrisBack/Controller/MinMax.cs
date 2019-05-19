using NineMensMorrisBack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NineMensMorrisBack.Controller
{
    public class MinMax
    {

        private int _maxDepth;
        private double _bestVal;
        private Node _bestNode;

        public MinMax(int maxDepth)
        {
            _maxDepth = maxDepth;
        }
        
        private double Minmax(GameState gameState, int depth, bool isMaximizingPlayer)
        {
            double value;
            double _bestVal;
            //if ( depth == _maxDepth || DefineGameState(position) == GameStateEnum.GameOver )
            //{
            //    return CalculateState(position);
            //}

            if (isMaximizingPlayer)
            {
                _bestVal = double.MinValue;
                foreach( Node n in gameState.Board.Board)
                {
                    value = Minmax(gameState, depth + 1, false);
                    _bestVal = Max(_bestVal, value);
                }
                return _bestVal;
            }
            else
            {
                _bestVal = double.MaxValue;
                foreach (Node n in gameState.Board.Board)
                {
                    value = Minmax(gameState, depth + 1, true);
                    _bestVal = Min(_bestVal, value);
                }
                return _bestVal;
            }

        }

        private double Min(double val1, double val2)
        {
            if ( val1 <= val2)
            {
                return val1;
            }
            else
            {
                return val2;
            }
        }

        private double Max(double val1, double val2)
        {
            if (val1 >= val2)
            {
                return val1;
            }
            else
            {
                return val2;
            }
        }


        public Node FindBestMove(GameState gameState)
        {
            Node bestChoice = null;
            foreach(Node n in gameState.Board.Board)
            {
                //if current move is better than bestMove
                //    bestMove = current move;
            }

            return bestChoice;
        }





    }
}
