using NineMensMorrisBack.Model;
using NineMensMorrisBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Controller
{
    public class GameController
    {
        private Random _rnd;
        public GameState GameStatus;

        public Player Turn { get; set; }

        private Node _selectedNode;


        //losowana jest koleność graczy
        //pierwszy gracz bierze pionka z tali, sprawdza gdzie może go postawić (ocenia plansze) i stawia 
        //

        public GameController()
        {
            _rnd = new Random();
            GameStatus = new GameState();
            if (_rnd.Next(2) == 1)
            {
                Turn = Player.PlayerTwo;
            }
            else
            {
                Turn = Player.PlayerOne;
            }

            GameStatus.Turn = Turn;

            
        }

        public void TheGame()
        {
            bool isEnd = false;

            //firs part
            while ((GameStatus.PlayerOneInitSet.Count != 0) && (GameStatus.PlayerTwoInitSet.Count != 0))
            {
                SetInitPlace();
                if (CheckMorrisOnLastPlaced())
                {
                    ChooseTileToRemove();
                    //RemoveTile();
                }
            }


            while (!isEnd)
            {
                MoveTile();
                RateBoardState();
                if (CheckMorrisOnLastPlaced())
                {
                    ChooseTileToRemove();
                    //RemoveTile();
                }





                isEnd = IfPlayerHasLessThanTree();
            }



        }

        private void RateBoardState()
        {
            throw new NotImplementedException();
        }

        private bool IfPlayerHasLessThanTree()
        {
            if ( (GameStatus.PlayerOneGoals.Count > 6) || (GameStatus.PlayerTwoGoals.Count > 6))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveTile(Node nodeToRemove)
        {
            nodeToRemove.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            switch(nodeToRemove.TileOn.Owner)
            {
                case Player.PlayerOne:
                    GameStatus.PlayerTwoGoals.Add(nodeToRemove.TileOn);

                    break;
                case Player.PlayerTwo:
                    GameStatus.PlayerOneGoals.Add(nodeToRemove.TileOn);
                    break;
                default:

                    break;
            }
            
            nodeToRemove.TileOn = null;

        }

        private bool ChooseTileToRemove()
        {
            Tile tile = null;

            while(!IfTileCanBeRemoved(tile))
            {
                tile = null;
            }

            return true;
        }

        private bool IfTileCanBeRemoved(Tile tile)
        {
            throw new NotImplementedException();
        }

        private bool CheckMorrisOnLastPlaced()
        {
            throw new NotImplementedException();
        }

        private void SetInitPlace()
        {
            //CheckFreePlaces();
            //ChoosePlace();
            //SetValue();
        }

        public void AfterSet()
        {
            ChangeTurn();
            //CheckState();

        }

        private void CheckState()
        {
            //CheckMorrisOnLastPlaced();
        }

        public bool CheckNextMove()
        {
            

            return true;
        }


        public void SetNextTile()
        {
            if (Turn.Equals(Player.PlayerOne))
            {

            }
            else if(Turn.Equals(Player.PlayerTwo))
            {

            }

            
        }

        public void MoveTile()
        {
            PickTileToMove();
            PickPlaceToMove();
            ExecuteMove();

        }

        private void ExecuteMove()
        {
            throw new NotImplementedException();
        }

        private void PickPlaceToMove()
        {
            throw new NotImplementedException();
        }

        private void PickTileToMove()
        {
            throw new NotImplementedException();
        }

        public void ChangeTurn()
        {
            if (GameStatus.Turn == Player.PlayerOne)
            {
                GameStatus.Turn = Player.PlayerTwo;
                Turn = GameStatus.Turn;
            }
            else if( GameStatus.Turn == Player.PlayerTwo)
            {
                GameStatus.Turn = Player.PlayerOne;
                Turn = GameStatus.Turn;
            }
        }
        
    }
}
