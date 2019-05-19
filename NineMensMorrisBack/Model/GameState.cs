using NineMensMorrisBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NineMensMorrisBack.Utils.CalculateRateHeuristics;

namespace NineMensMorrisBack.Model
{
    public class GameState
    {
        public List<Tile> PlayerOneInitSet { get; set; }
        public List<Tile> PlayerTwoInitSet { get; set; }
        public HashSet<Tile> PlayerOneGoals { get; set; }
        public HashSet<Tile> PlayerTwoGoals { get; set; }

        public GameBoard Board { get; set; }

        public Player Turn { get; set; }
        private Random _rnd;
        public int Rate { get; set; }



        public GameState()
        {
            _rnd = new Random();
            Board = new GameBoard();
            InitStarters();
            
        }

        private void InitStarters()
        {
            PlayerOneInitSet = new List<Tile>(9);
            PlayerTwoInitSet = new List<Tile>(9);
            PlayerOneGoals = new HashSet<Tile>();
            PlayerTwoGoals = new HashSet<Tile>();

            PlayerOneInitSet.Add(new Tile( "P1-1", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-2", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-3", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-4", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-5", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-6", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-7", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-8", Player.PlayerOne, -1, -1));
            PlayerOneInitSet.Add(new Tile( "P1-9", Player.PlayerOne, -1, -1));

            PlayerTwoInitSet.Add(new Tile("P2-1", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-2", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-3", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-4", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-5", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-6", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-7", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-8", Player.PlayerTwo, -1, -1));
            PlayerTwoInitSet.Add(new Tile("P2-9", Player.PlayerTwo, -1, -1));

        }

        

        public int CalculateRate(CalculateRateHeuristic calc)
        {
            Rate = calc(this);
            return 0;
        }

        public int CheckState(int heuristicTypeNumber)
        {
            CalculateRateHeuristics calcRateObj = new CalculateRateHeuristics();
            CalculateRateHeuristic heuristicType;
            if ( heuristicTypeNumber == 1)
                heuristicType = new CalculateRateHeuristic(calcRateObj.FirstHeuristic);
            else
                heuristicType = new CalculateRateHeuristic(calcRateObj.FirstHeuristic);

            CalculateRate(heuristicType);


            return 0;
        }

        public int CheckIfMorriceOnLastMove(Node node)
        {
            List<Node> column = Board.GetOtherColumnNodes(node.Column, node.Row);
            List<Node> row = Board.GetOtherRowNodes(node.Row, node.Column);

            int isMorrice = 0;

            if (column.All(n => n.GraphicRepresentation.Background.Equals(node.GraphicRepresentation.Background)))
            {
                isMorrice++;
            }

            if (row.All(n => n.GraphicRepresentation.Background.Equals(node.GraphicRepresentation.Background)))
            {
                isMorrice++;
            }

            if (isMorrice > 0)
            {
                Console.WriteLine("MŁYNEK");
            }
            return isMorrice;
        }







    }
}
