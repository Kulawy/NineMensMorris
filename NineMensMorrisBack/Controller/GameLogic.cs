using NineMensMorrisBack.Model;
using NineMensMorrisBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Windows;

namespace NineMensMorrisBack.Controller
{
    public class GameLogic
    {
        private Random _rnd;
        public GameState GameStatus;

        private Node _selectedNode = null;

        //losowana jest koleność graczy
        //pierwszy gracz bierze pionka z tali, sprawdza gdzie może go postawić (ocenia plansze) i stawia 

        public GameLogic(GameState gameStatus)
        {
            _rnd = new Random();

            GameStatus = gameStatus;

        }

        public void TheGamePlay(Node choosenNode)
        {

            if (GameStatus.IsMorrice > 0)
            {
                if (GameStatus.Turn != choosenNode.TileOn.Owner)
                {
                    RemoveTile(choosenNode);
                    GameStatus.IsMorrice--;
                    CheckAfterRemove();
                }

            }
            else if (GameStatus.PlayerOneInitSet.Any() || GameStatus.PlayerTwoInitSet.Any())
            {
                PlaceNode(choosenNode);
            }
            else if (GameStatus.PlayerOneGoals.Count < 6 && GameStatus.PlayerTwoGoals.Count < 6)
            {
                WalkController(choosenNode);
            }
            else if ((GameStatus.PlayerOneGoals.Count == 6 && GameStatus.Turn == Player.PlayerOne) || (GameStatus.PlayerTwoGoals.Count == 6 && GameStatus.Turn == Player.PlayerTwo))
            {
                FlyController(choosenNode);
            }

            #region OldCode
            //bool isEnd = false;

            ////firs part
            //while ((GameStatus.PlayerOneInitSet.Count != 0) && (GameStatus.PlayerTwoInitSet.Count != 0))
            //{
            //    SetInitPlace();
            //    if (CheckMorrisOnLastPlaced())
            //    {
            //        ChooseTileToRemove();
            //        //RemoveTile();
            //    }
            //}


            //while (!isEnd)
            //{
            //    MoveTile();
            //    RateBoardState();
            //    if (CheckMorrisOnLastPlaced())
            //    {
            //        ChooseTileToRemove();
            //        //RemoveTile();
            //    }

            //    isEnd = IfPlayerHasLessThanTree();
            //}

            #endregion

        }

        public Node PickNodeForMove(GameState state)
        {


            return null;
        }

        private void ChangeStateAfterMove()
        {
            if (GameStatus.PlayerOneInitSet.Any() || GameStatus.PlayerTwoInitSet.Any())
            {
                GameStatus.Stage = GameStage.Placing;
            }
            else if (GameStatus.PlayerOneGoals.Count < 6 && GameStatus.PlayerTwoGoals.Count < 6)
            {
                GameStatus.Stage = GameStage.PickMovingElement;
            }
            else if ((GameStatus.PlayerOneGoals.Count == 6 && GameStatus.Turn == Player.PlayerTwo) || (GameStatus.PlayerTwoGoals.Count == 6 && GameStatus.Turn == Player.PlayerOne))
            {
                GameStatus.Stage = GameStage.PickFlyElement;
            }

        }

        private void PlaceNode(Node choosenNode)
        {
            if (choosenNode.TileOn == null)
            {
                if (GameStatus.Turn == Player.PlayerOne)
                {
                    choosenNode.TileOn = GameStatus.PlayerOneInitSet.Last();
                    GameStatus.PlayerOneInitSet.Remove(GameStatus.PlayerOneInitSet.Last());
                    choosenNode.TileOn.Graphic.Visibility = Visibility.Hidden;
                    choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;

                    CheckAfterSet(choosenNode);
                }
                else if (GameStatus.Turn == Player.PlayerTwo)
                {
                    choosenNode.TileOn = GameStatus.PlayerTwoInitSet.Last();
                    GameStatus.PlayerTwoInitSet.Remove(GameStatus.PlayerTwoInitSet.Last());
                    choosenNode.TileOn.Graphic.Visibility = Visibility.Hidden;
                    choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;

                    CheckAfterSet(choosenNode);
                }
            }
        }

        private bool IfPlayerHasLessThanTree()
        {
            if ((GameStatus.PlayerOneGoals.Count > 6) || (GameStatus.PlayerTwoGoals.Count > 6))
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
            switch (nodeToRemove.TileOn.Owner)
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
        
        public bool CheckNextMove()
        {


            return true;
        }
        


        public void FlyController(Node choosenNode)
        {
            if ((CheckTurnController() == 1 && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_WHITE)
                    || (CheckTurnController() == 2 && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_BLACK)
                    || (_selectedNode != null && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_EMPTY))
            {
                if (_selectedNode == null && choosenNode.TileOn != null)
                {
                    _selectedNode = choosenNode;
                    _selectedNode.TileOn.LastNode = choosenNode;
                    _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
                }
                else if (_selectedNode != null && choosenNode.TileOn == null)
                {
                    if (CanFly())
                    {
                        choosenNode.TileOn = _selectedNode.TileOn;
                        choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;
                        _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
                        _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
                        _selectedNode.TileOn = null;
                        _selectedNode = null;
                        CheckAfterSet(choosenNode);
                    }
                    else if (_selectedNode.IsConneced(choosenNode))
                    {
                        choosenNode.TileOn = _selectedNode.TileOn;
                        choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;
                        _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
                        _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
                        _selectedNode.TileOn = null;
                        _selectedNode = null;
                        CheckAfterSet(choosenNode);
                    }

                }

                else if (_selectedNode == choosenNode)
                {
                    _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
                    _selectedNode = null;

                }
            }
        }

        private bool CanFly()
        {
            if (CheckTurnController() == 1 && GameStatus.PlayerTwoGoals.Count == 6)
            {
                return true;
            }
            else if (CheckTurnController() == 2 && GameStatus.PlayerOneGoals.Count == 6)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void CheckAfterSet(Node node)
        {
            GameStatus.IsMorrice = GameStatus.CheckIfMorriceOnLastMove(node);
            if (GameStatus.IsMorrice <= 0)
            {
                ChangeTurn();
            }
            CheckTurnController();
        }

        private int CheckAfterRemove()
        {
            if (GameStatus.PlayerOneGoals.Count > 6)
            {
                return 1;
            }
            else if (GameStatus.PlayerTwoGoals.Count > 6)
            {
                return 2;
            }
            else
            {
                if (GameStatus.IsMorrice <= 0)
                {
                    ChangeTurn();
                    CheckTurnController();
                }
                return 0;
            }
        }

        public void ChangeTurn()
        {
            if (GameStatus.Turn == Player.PlayerOne)
            {
                GameStatus.Turn = Player.PlayerTwo;
            }
            else if (GameStatus.Turn == Player.PlayerTwo)
            {
                GameStatus.Turn = Player.PlayerOne;
            }
        }

        public int CheckTurnController()
        {
            switch (GameStatus.Turn)
            {
                case Player.PlayerOne:
                    return 1;
                case Player.PlayerTwo:
                    return 2;
                default:
                    return 0;
            }
        }

        private void WalkController(Node choosenNode)
        {

            if ((CheckTurnController() == 1 && choosenNode.TileOn.Owner == Player.PlayerOne)
                    || (CheckTurnController() == 2 && choosenNode.TileOn.Owner == Player.PlayerTwo)
                    || (_selectedNode != null && choosenNode.TileOn == null))
            {
                if (_selectedNode == null && choosenNode.TileOn != null)
                {
                    _selectedNode = choosenNode;
                    _selectedNode.TileOn.LastNode = choosenNode;
                }
                else if (_selectedNode != null && choosenNode.TileOn == null)
                {
                    if (_selectedNode.IsConneced(choosenNode))
                    {
                        choosenNode.TileOn = _selectedNode.TileOn;
                        _selectedNode.TileOn = null;
                        _selectedNode = null;
                        CheckAfterSet(choosenNode);
                    }

                }

                else if (_selectedNode == choosenNode)
                {
                    _selectedNode = null;
                }
            }

        }

        


    }
}
