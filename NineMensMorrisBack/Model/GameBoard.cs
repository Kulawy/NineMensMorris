using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Model
{
    public class GameBoard
    {
        
        public HashSet<Node> Board { get; set; }

        public GameBoard()
        {
            InitBoard();
            SetRelations();
        }

        public Node GetNode(int row, int colmn)
        {
            return Board.First(n => (n.Row == row && n.Column == colmn));
        }

        public List<Node> GetRowNodes(int row)
        {
            return Board.Where(n => (n.Row == row)).ToList() ;
        }

        public List<Node> GetColumnNodes(int column)
        {
            return Board.Where(n => (n.Column == column)).ToList();
        }

        public List<Node> GetOtherRowNodes(int row, int column)
        {
            List<Node> nodesToReturn = new List<Node>();
            if( row == 4 )
            {
                if ( column < 4)
                {
                    nodesToReturn = Board.Where(n => (n.Row == row) && (n.Column != column) && (n.Column < 4)).ToList();
                }
                else if (column > 4 )
                {
                    nodesToReturn = Board.Where(n => (n.Row == row) && (n.Column != column) && (n.Column > 4)).ToList();
                }
            }
            else
            {
                nodesToReturn = Board.Where(n => (n.Row == row) && (n.Column != column)).ToList();
            }

            return nodesToReturn;
        }

        public List<Node> GetOtherColumnNodes(int column, int row)
        {
            List<Node> nodesToReturn = new List<Node>();
            if (column == 4)
            {
                if ( row < 4)
                {
                    nodesToReturn = Board.Where(n => (n.Column == column) && (n.Row != row) && ( n.Row < 4) ).ToList();
                }
                else if (row > 4 )
                {
                    nodesToReturn = Board.Where(n => (n.Column == column) && (n.Row != row) && ( n.Row > 4) ).ToList();
                }
            }
            else
            {
                nodesToReturn = Board.Where(n => (n.Column == column) && (n.Row != row)).ToList();
            }

            return nodesToReturn;
        }


        private void SetRelations()
        {
            //outer
            GetNode(1, 1).Right = GetNode(1, 4);
            GetNode(1, 1).Down = GetNode(4, 1);

            GetNode(1, 4).Left = GetNode(1, 1);
            GetNode(1, 4).Right = GetNode(1, 7);
            GetNode(1, 4).Down = GetNode(2, 4);

            GetNode(1, 7).Left = GetNode(1, 4);
            GetNode(1, 7).Down = GetNode(4, 7);

            GetNode(7, 1).Right = GetNode(7, 4);
            GetNode(7, 1).Up = GetNode(4, 1);

            GetNode(7, 4).Left = GetNode(7, 1);
            GetNode(7, 4).Right = GetNode(7, 7);
            GetNode(7, 4).Up = GetNode(6, 4);

            GetNode(7, 7).Left = GetNode(7, 4);
            GetNode(7, 7).Up = GetNode(4, 7);

            GetNode(4, 1).Up = GetNode(1, 1);
            GetNode(4, 1).Down = GetNode(7, 1);
            GetNode(4, 1).Right = GetNode(4, 2);

            GetNode(4, 7).Up = GetNode(1, 7);
            GetNode(4, 7).Down = GetNode(7, 7);
            GetNode(4, 7).Left = GetNode(4, 6);

            //middle
            GetNode(2, 2).Right = GetNode(2, 4);
            GetNode(2, 2).Down = GetNode(4, 2);

            GetNode(2, 4).Left = GetNode(2, 2);
            GetNode(2, 4).Right = GetNode(2, 6);
            GetNode(2, 4).Up = GetNode(1, 4);
            GetNode(2, 4).Down = GetNode(3, 4);

            GetNode(2, 6).Left = GetNode(2, 4);
            GetNode(2, 6).Down = GetNode(4, 6);

            GetNode(6, 2).Right = GetNode(6, 4);
            GetNode(6, 2).Up = GetNode(4, 2);

            GetNode(6, 4).Left = GetNode(6, 2);
            GetNode(6, 4).Right = GetNode(6, 6);
            GetNode(6, 4).Up = GetNode(5, 4);
            GetNode(6, 4).Down = GetNode(7, 4);

            GetNode(6, 6).Left = GetNode(6, 4);
            GetNode(6, 6).Up = GetNode(4, 6);

            GetNode(4, 2).Up = GetNode(2, 2);
            GetNode(4, 2).Down = GetNode(6, 2);
            GetNode(4, 2).Right = GetNode(4, 3);
            GetNode(4, 2).Left = GetNode(4, 1);

            GetNode(4, 6).Up = GetNode(2, 6);
            GetNode(4, 6).Down = GetNode(6, 6);
            GetNode(4, 6).Right = GetNode(4, 7);
            GetNode(4, 6).Left = GetNode(4, 5);

            //inner
            GetNode(3, 3).Right = GetNode(3, 4);
            GetNode(3, 3).Down = GetNode(4, 3);

            GetNode(3, 4).Left = GetNode(3, 3);
            GetNode(3, 4).Right = GetNode(3, 5);
            GetNode(3, 4).Up = GetNode(2, 4);

            GetNode(3, 5).Left = GetNode(3, 4);
            GetNode(3, 5).Down = GetNode(4, 5);

            GetNode(5, 3).Right = GetNode(5, 4);
            GetNode(5, 3).Up = GetNode(4, 3);

            GetNode(5, 4).Left = GetNode(5, 3);
            GetNode(5, 4).Right = GetNode(5, 5);
            GetNode(5, 4).Down = GetNode(6, 4);

            GetNode(5, 5).Left = GetNode(5, 4);
            GetNode(5, 5).Up = GetNode(4, 5);

            GetNode(4, 3).Up = GetNode(3, 3);
            GetNode(4, 3).Down = GetNode(5, 3);
            GetNode(4, 3).Left = GetNode(4, 2);

            GetNode(4, 5).Up = GetNode(3, 5);
            GetNode(4, 5).Down = GetNode(5, 5);
            GetNode(4, 5).Right = GetNode(4, 6);


        }

        private void InitBoard()
        {
            Board = new HashSet<Node>();

            //OUTER
            Board.Add(new Node("A1", 1, 1));
            Board.Add(new Node("A4", 1, 4));
            Board.Add(new Node("A7", 1, 7));

            Board.Add(new Node("G1", 7, 1));
            Board.Add(new Node("G4", 7, 4));
            Board.Add(new Node("G7", 7, 7));

            Board.Add(new Node("D1", 4, 1));
            Board.Add(new Node("D7", 4, 7));

            //MIDDLE
            Board.Add(new Node("B2", 2, 2));
            Board.Add(new Node("B4", 2, 4));
            Board.Add(new Node("B6", 2, 6));

            Board.Add(new Node("F2", 6, 2));
            Board.Add(new Node("F4", 6, 4));
            Board.Add(new Node("F6", 6, 6));

            Board.Add(new Node("D2", 4, 2));
            Board.Add(new Node("D6", 4, 6));

            //INNER
            Board.Add(new Node("C3", 3, 3));
            Board.Add(new Node("C4", 3, 4));
            Board.Add(new Node("C5", 3, 5));

            Board.Add(new Node("E3", 5, 3));
            Board.Add(new Node("E4", 5, 4));
            Board.Add(new Node("E5", 5, 5));

            Board.Add(new Node("D3", 4, 3));
            Board.Add(new Node("D5", 4, 5));

        }

    }
}
