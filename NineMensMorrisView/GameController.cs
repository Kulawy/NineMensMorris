using NineMensMorrisBack.Model;
using NineMensMorrisBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Navigation;
using NineMensMorrisView;
using System.Windows;
using System.Threading;

namespace NineMensMorrisBack.Controller
{
    public class GameController
    {
        private Random _rnd;
        public GameState PresentGameState;

        private int _player1MoveCounter;
        private int _player2MoveCounter;
        private double _player1TimeCounter;
        private double _player2TimeCounter;

        private int _player1Type;
        private int _player2Type;

        private int _player1CalculateHeuristicType;
        private int _player2CalculateHeuristicType;

        private int _player1GameHeuristicType;
        private int _player2GameHeuristicType;

        //private GameStage _stage;

        private Node _selectedNode = null;
        private Rectangle _moveTurnL_Rectangle;
        private Rectangle _moveTurnR_Rectangle;
        private NavigationService _navigationService;
        private GamePage _gamePage;
        
        //losowana jest koleność graczy
        //pierwszy gracz bierze pionka z tali, sprawdza gdzie może go postawić (ocenia plansze) i stawia 

        public GameController(Dictionary<string, int> valuesContainer, Dictionary<String, Object> graphicIcons)
        {

            _rnd = new Random();
            _player1MoveCounter = 0;
            _player2MoveCounter = 0;
            _player1TimeCounter = 0;
            _player2TimeCounter = 0;

            _player1Type = valuesContainer["Player1Type"];
            _player2Type = valuesContainer["Player2Type"];

            _moveTurnL_Rectangle = (Rectangle) graphicIcons["RectangleLeft"];
            _moveTurnR_Rectangle = (Rectangle) graphicIcons["RectangleRight"];
            _gamePage = (GamePage)graphicIcons["GamePlan"];

            PresentGameState = new GameState();
            if (_rnd.Next(2) == 1)
            {
                PresentGameState.Turn = Player.PlayerTwo;
            }
            else
            {
                PresentGameState.Turn = Player.PlayerOne;
            }
        }

        public void TheGamePlay(Node choosenNode)
        {

            if (PresentGameState.Stage == GameStage.Morrice )
            {
                if ( choosenNode.TileOn != null &&  choosenNode.TileOn.Owner != PresentGameState.Turn)
                {
                    RemoveTile(choosenNode);
                    PresentGameState.IsMorrice--;
                    CheckAfterRemove();
                }


            }
            else if (PresentGameState.Stage == GameStage.Placing)
            {
                PlaceNode(choosenNode);
            }
            else if (PresentGameState.Stage == GameStage.PickMovingElement || PresentGameState.Stage == GameStage.PickMovingDestination)
            {
                WalkController(choosenNode);
            }
            else if (PresentGameState.PlayerOneGoals.Count == 6 || PresentGameState.PlayerTwoGoals.Count == 6)
            {
                FlyController(choosenNode);
            }
            MeasureTournament();
            CheckStage();
            AutoMove();
            
        }

        public void TheGamePlayAuto(Node choosenNode)
        {

            if (PresentGameState.IsMorrice > 0)
            {
                if (choosenNode.TileOn != null && choosenNode.TileOn.Owner != PresentGameState.Turn)
                {
                    RemoveTile(choosenNode);
                    PresentGameState.IsMorrice--;
                    CheckAfterRemove();
                }


            }
            else if (PresentGameState.Stage == GameStage.Placing)
            {
                PlaceNode(choosenNode);
            }
            else if (PresentGameState.Stage == GameStage.PickMovingElement || PresentGameState.Stage == GameStage.PickMovingDestination)
            {
                WalkController(choosenNode);
            }
            else if (PresentGameState.PlayerOneGoals.Count == 6 || PresentGameState.PlayerTwoGoals.Count == 6)
            {
                FlyController(choosenNode);
            }
            CheckStage();
            //AutoMove();
            

        }

        public void AutoMove()
        {
            if ( (PresentGameState.Turn == Player.PlayerOne && _player1Type != 2) || (PresentGameState.Turn == Player.PlayerTwo && _player2Type != 2))
            {
                List<Node> possibleNodes = GetPossibleMoves(PresentGameState.Turn);

                //Thread.Sleep(1000);
                //_gamePage.UpdateLayout();
                //((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();
                
                Console.WriteLine( _player1MoveCounter + "\n" + _player2MoveCounter + "\n");
                while (possibleNodes.Count == 0 && _selectedNode != null)
                {
                    _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
                    _selectedNode = null;
                    CheckStage();
                    possibleNodes = GetPossibleMoves(PresentGameState.Turn);
                }
                TheGamePlay(possibleNodes[_rnd.Next(possibleNodes.Count)]);
                
            }

        }


        private void MeasureTournament()
        {
            if (PresentGameState.Turn == Player.PlayerOne)
            {
                _player1MoveCounter++;
            }
            else
            {
                _player2MoveCounter++;
            }
        }

        private void ChangeStateAfterMove()
        {
            if (PresentGameState.PlayerOneInitSet.Any() || PresentGameState.PlayerTwoInitSet.Any())
            {
                PresentGameState.Stage = GameStage.Placing;
            }
            else if ((PresentGameState.PlayerOneGoals.Count < 6 && PresentGameState.PlayerTwoGoals.Count < 6) && _selectedNode == null)
            {
                PresentGameState.Stage = GameStage.PickMovingElement;
            }
            else if ((PresentGameState.PlayerOneGoals.Count < 6 && PresentGameState.PlayerTwoGoals.Count < 6) && _selectedNode != null)
            {
                PresentGameState.Stage = GameStage.PickMovingDestination;
            }
            else if (((PresentGameState.PlayerOneGoals.Count == 6 && PresentGameState.Turn == Player.PlayerTwo) || (PresentGameState.PlayerTwoGoals.Count == 6 && PresentGameState.Turn == Player.PlayerOne)) && _selectedNode == null)
            {
                PresentGameState.Stage = GameStage.PickFlyElement;
            }
            else if (((PresentGameState.PlayerOneGoals.Count == 6 && PresentGameState.Turn == Player.PlayerTwo) || (PresentGameState.PlayerTwoGoals.Count == 6 && PresentGameState.Turn == Player.PlayerOne)) && _selectedNode != null )
            {
                PresentGameState.Stage = GameStage.PickFlyDestination;
            }
            else if ( PresentGameState.PlayerOneGoals.Count > 6 || PresentGameState.PlayerTwoGoals.Count > 6 )
            {
                PresentGameState.Stage = GameStage.GameOver;
            }
        }


        

        public void RemoveTile(Node nodeToRemove)
        {
            nodeToRemove.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            switch(nodeToRemove.TileOn.Owner)
            {
                case Player.PlayerOne:
                    PresentGameState.PlayerTwoGoals.Add(nodeToRemove.TileOn);
                    break; 
                case Player.PlayerTwo:
                    PresentGameState.PlayerOneGoals.Add(nodeToRemove.TileOn);
                    break;
                default:
                    break;
            }
            nodeToRemove.TileOn = null;

        }
        

        private bool IfTileCanBeRemoved(Node node)
        {
            if ( node.TileOn != null && PresentGameState.Turn != node.TileOn.Owner)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        }
        
        public void ChangeTurn()
        {
            if (PresentGameState.Turn == Player.PlayerOne)
            {
                PresentGameState.Turn = Player.PlayerTwo;
            }
            else if( PresentGameState.Turn == Player.PlayerTwo)
            {
                PresentGameState.Turn = Player.PlayerOne;
            }
        }

        private void PlaceNode(Node choosenNode)
        {
            if (choosenNode.TileOn == null)
            {
                if (PresentGameState.Turn == Player.PlayerOne)
                {
                    choosenNode.TileOn = PresentGameState.PlayerOneInitSet.Last();
                    PresentGameState.PlayerOneInitSet.Remove(PresentGameState.PlayerOneInitSet.Last());
                    choosenNode.TileOn.Graphic.Visibility = Visibility.Hidden;
                    choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;

                    CheckAfterSet(choosenNode);
                }
                else if (PresentGameState.Turn == Player.PlayerTwo)
                {
                    choosenNode.TileOn = PresentGameState.PlayerTwoInitSet.Last();
                    PresentGameState.PlayerTwoInitSet.Remove(PresentGameState.PlayerTwoInitSet.Last());
                    choosenNode.TileOn.Graphic.Visibility = Visibility.Hidden;
                    choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;

                    CheckAfterSet(choosenNode);
                }
            }
        }

        #region Walk

        private void PickTileToWalkOnlyForAuto(Node choosenNode)
        {
            _selectedNode = choosenNode;
            _selectedNode.TileOn.LastNode = choosenNode;
            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
        }

        private void PickTileToDestinationOnlyForAuto(Node choosenNode)
        {
            choosenNode.TileOn = _selectedNode.TileOn;
            choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;
            _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            _selectedNode.TileOn = null;
            _selectedNode = null;
            CheckAfterSet(choosenNode);
        }

        private void WalkController(Node choosenNode)
        {

            if ((PresentGameState.Turn == Player.PlayerOne && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_WHITE)
                    || (PresentGameState.Turn == Player.PlayerTwo && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_BLACK)
                    || (_selectedNode != null && choosenNode.GraphicRepresentation.Background == GlobalValues.BRUSH_EMPTY))
            {

                if (_selectedNode == null && choosenNode.TileOn != null)
                {
                    //_selectedTile = n.TileOn;
                    _selectedNode = choosenNode;
                    _selectedNode.TileOn.LastNode = choosenNode;
                    _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
                }
                else if (_selectedNode != null && choosenNode.TileOn == null)
                {
                    //n.TileOn = _selectedTile;
                    if (_selectedNode.IsConneced(choosenNode))
                    {
                        choosenNode.TileOn = _selectedNode.TileOn;
                        choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;
                        _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
                        //_selectedTile = null;
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

        #endregion

        #region Fly
        private void PickTileToFlyOnlyForAuto(Node choosenNode)
        {
            _selectedNode = choosenNode;
            _selectedNode.TileOn.LastNode = choosenNode;
            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
        }

        private void PickDestinationToFlyOnlyForAuto(Node choosenNode)
        {
            choosenNode.TileOn = _selectedNode.TileOn;
            choosenNode.GraphicRepresentation.Background = choosenNode.TileOn.Graphic.Fill;
            _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            _selectedNode.TileOn = null;
            _selectedNode = null;
            CheckAfterSet(choosenNode);
        }


        private void FlyController(Node choosenNode)
        {
            if ((_selectedNode != null && choosenNode.TileOn == null)
                    ||(PresentGameState.Turn == Player.PlayerOne && choosenNode.TileOn.Owner == Player.PlayerOne)
                    || (PresentGameState.Turn == Player.PlayerTwo && choosenNode.TileOn.Owner == Player.PlayerTwo))
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

        #endregion

        

        private void CheckStage()
        {
            if ( PresentGameState.IsMorrice > 0 )
            {
                PresentGameState.Stage = GameStage.Morrice;
            }
            else if ( PresentGameState.Turn == Player.PlayerOne)
            {
                if ( PresentGameState.PlayerOneInitSet.Count > 0 )
                {
                    PresentGameState.Stage = GameStage.Placing;
                }
                else if ( PresentGameState.PlayerTwoGoals.Count < 6 && _selectedNode == null )
                {
                    PresentGameState.Stage = GameStage.PickMovingElement;
                }
                else if ( PresentGameState.PlayerTwoGoals.Count < 6 && _selectedNode != null )
                {
                    PresentGameState.Stage = GameStage.PickMovingDestination;
                }
                else if ( _selectedNode == null )
                {
                    PresentGameState.Stage = GameStage.PickFlyElement;
                }
                else if ( _selectedNode != null )
                {
                    PresentGameState.Stage = GameStage.PickFlyDestination;
                }
                

            }
            else if ( PresentGameState.Turn == Player.PlayerTwo)
            {
                if (PresentGameState.PlayerTwoInitSet.Count > 0)
                {
                    PresentGameState.Stage = GameStage.Placing;
                }
                else if (PresentGameState.PlayerOneGoals.Count < 6 && _selectedNode == null)
                {
                    PresentGameState.Stage = GameStage.PickMovingElement;
                }
                else if (PresentGameState.PlayerOneGoals.Count < 6 && _selectedNode != null)
                {
                    PresentGameState.Stage = GameStage.PickMovingDestination;
                }
                else if (_selectedNode == null)
                {
                    PresentGameState.Stage = GameStage.PickFlyElement;
                }
                else if (_selectedNode != null)
                {
                    PresentGameState.Stage = GameStage.PickFlyDestination;
                }
            }
        }

        private bool CanFly()
        {
            if (PresentGameState.Turn == Player.PlayerOne && PresentGameState.PlayerTwoGoals.Count == 6)
            {
                return true;
            }
            else if (PresentGameState.Turn == Player.PlayerTwo && PresentGameState.PlayerOneGoals.Count == 6)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private Node FindNodeByNodeBorder(Border nodeBorder)
        {
            return PresentGameState.Board.Board.First(n => n.GraphicRepresentation == nodeBorder);
        }

        private void CheckAfterSet(Node node)
        {
            PresentGameState.IsMorrice = PresentGameState.CheckIfMorriceOnLastMove(node);

            if (PresentGameState.IsMorrice <= 0)
            {
                ChangeTurn();
            }
            UpdateGraphicTurnController();
        }

        private void CheckAfterRemove()
        {
            if (PresentGameState.PlayerOneGoals.Count > 6)
            {
                _gamePage.NavigationService.Navigate(new WinPage(Player.PlayerOne));
            }
            else if (PresentGameState.PlayerTwoGoals.Count > 6)
            {
                _gamePage.NavigationService.Navigate(new WinPage(Player.PlayerTwo));
            }
            else
            {
                if (PresentGameState.IsMorrice <= 0)
                {
                    ChangeTurn();
                    UpdateGraphicTurnController();
                }
            }
        }

        public void UpdateGraphicTurnController()
        {
            switch (PresentGameState.Turn)
            {
                case Player.PlayerOne:
                    _moveTurnL_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
                    _moveTurnR_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
                    break;
                case Player.PlayerTwo:
                    _moveTurnL_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
                    _moveTurnR_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
                    break;
                default:
                    _moveTurnL_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
                    _moveTurnR_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
                    break;
            }
        }


        private List<Node> GetPossibleMoves(Player player)
        {
            List<Node> possibleNodes = new List<Node>();
            if( PresentGameState.Stage == GameStage.Morrice)
            {
                foreach (Node n in PresentGameState.Board.Board)
                {
                    if (n.TileOn != null && n.TileOn.Owner == OpisitePlayer())
                    {
                        possibleNodes.Add(n);
                    }
                }
            }
            else if ( PresentGameState.Stage == GameStage.Placing)
            {
                foreach(Node n in PresentGameState.Board.Board)
                {
                    if ( n.TileOn == null)
                    {
                        possibleNodes.Add(n);
                    }
                }
            }
            else if ( PresentGameState.Stage == GameStage.PickMovingElement)
            {
                foreach (Node n in PresentGameState.Board.Board)
                {
                    if (n.TileOn != null && n.TileOn.Owner == player)
                    {
                        possibleNodes.Add(n);
                    }
                }
            }
            else if ( PresentGameState.Stage == GameStage.PickMovingDestination)
            {
                foreach (Node n in PresentGameState.Board.Board)
                {
                    if ( n.TileOn == null && _selectedNode.IsConneced(n))
                    {
                        possibleNodes.Add(n);
                    }
                }
            }
            else if (PresentGameState.Stage == GameStage.PickFlyElement)
            {
                foreach (Node n in PresentGameState.Board.Board)
                {
                    if (n.TileOn != null &&  n.TileOn.Owner == player)
                    {
                        possibleNodes.Add(n);
                    }
                }
            }
            else if (PresentGameState.Stage == GameStage.PickFlyDestination)
            {
                foreach (Node n in PresentGameState.Board.Board)
                {
                    if (n.TileOn == null)
                    {
                        possibleNodes.Add(n);
                    }
                }
            }


            return possibleNodes;
        }

        private Player OpisitePlayer()
        {
            if ( PresentGameState.Turn == Player.PlayerOne){
                return Player.PlayerTwo;
            }
            else if (PresentGameState.Turn == Player.PlayerTwo)
            {
                return Player.PlayerOne;
            }
            else
            {
                return Player.Empty;
            }


        }
        

    }
}
