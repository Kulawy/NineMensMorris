﻿using NineMensMorrisBack.Controller;
using NineMensMorrisBack.Model;
using NineMensMorrisBack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NineMensMorrisView
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    /// 
    public partial class GamePage : Page
    {

        #region Consts

        #endregion

        
        private Random _rnd;
        private GameController _controller;
        private Dictionary<string, int> _valuesContainer;
        //private Tile _selectedTile = null;
        //private Node _selectedNode = null;
        //private int _isMorrice = 0;

        public GamePage(Dictionary<string, int> valuesContainer)
        {
            InitializeComponent();
            _rnd = new Random();
            _valuesContainer = valuesContainer;
            _controller = new GameController(valuesContainer, SetIconsToMap());
            SetUpTilesToController();
            SetUpNodeGraphicToController();
            _controller.UpdateGraphicTurnController();
        }

        private Dictionary<String, Object> SetIconsToMap()
        {
            Dictionary<String, Object> shapes = new Dictionary<string, Object>();
            shapes.Add("RectangleLeft", MoveTurnL_Rectangle);
            shapes.Add("RectangleRight", MoveTurnR_Rectangle);
            shapes.Add("GamePlan", this);

            return shapes;
        }


        //public int CheckTurn()
        //{
        //    switch (_controller.GameStatus.Turn)
        //    {
        //        case Player.PlayerOne:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
        //            return 1;
        //        case Player.PlayerTwo:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
        //            return 2;
        //        default:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
        //            return 0;
        //    }
        //}

        //public void CheckTurnGraphic(int turnValue)
        //{
        //    switch (turnValue)
        //    {
        //        case 1:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_WHITE;
        //            break;
        //        case 2:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_BLACK;
        //            break;
        //        default:
        //            MoveTurnL_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
        //            MoveTurnR_Rectangle.Fill = GlobalValues.BRUSH_EMPTY;
        //            break;
        //    }
        //}

        private void SetUpNodeGraphicToController()
        {
            HashSet<Border> nodes = new HashSet<Border>();
            nodes.Add(FieldA1_Border);
            nodes.Add(FieldA4_Border);
            nodes.Add(FieldA7_Border);
            nodes.Add(FieldG1_Border);
            nodes.Add(FieldG4_Border);
            nodes.Add(FieldG7_Border);
            nodes.Add(FieldD1_Border);
            nodes.Add(FieldD7_Border);


            nodes.Add(FieldB2_Border);
            nodes.Add(FieldB4_Border);
            nodes.Add(FieldB6_Border);
            nodes.Add(FieldF2_Border);
            nodes.Add(FieldF4_Border);
            nodes.Add(FieldF6_Border);
            nodes.Add(FieldD2_Border);
            nodes.Add(FieldD6_Border);

            nodes.Add(FieldC3_Border);
            nodes.Add(FieldC4_Border);
            nodes.Add(FieldC5_Border);
            nodes.Add(FieldE3_Border);
            nodes.Add(FieldE4_Border);
            nodes.Add(FieldE5_Border);

            nodes.Add(FieldD3_Border);
            nodes.Add(FieldD5_Border);

            for (int i = 0; i < nodes.Count; i++)
            {
                //nodes.ElementAt(i).Name.Contains(_controller.GameStateO.Board.Board.ElementAt(i))
                _controller.PresentGameState.Board.Board.ElementAt(i).GraphicRepresentation = nodes.ElementAt(i);
            }

        }

        private void SetUpTilesToController()
        {
            List<Ellipse> p1Tiles = new List<Ellipse>();
            p1Tiles.Add(TileLeft9_Elipse);
            p1Tiles.Add(TileLeft8_Elipse);
            p1Tiles.Add(TileLeft7_Elipse);
            p1Tiles.Add(TileLeft6_Elipse);
            p1Tiles.Add(TileLeft5_Elipse);
            p1Tiles.Add(TileLeft4_Elipse);
            p1Tiles.Add(TileLeft3_Elipse);
            p1Tiles.Add(TileLeft2_Elipse);
            p1Tiles.Add(TileLeft1_Elipse);

            List<Ellipse> p2Tiles = new List<Ellipse>();
            p2Tiles.Add(TileRight9_Elipse);
            p2Tiles.Add(TileRight8_Elipse);
            p2Tiles.Add(TileRight7_Elipse);
            p2Tiles.Add(TileRight6_Elipse);
            p2Tiles.Add(TileRight5_Elipse);
            p2Tiles.Add(TileRight4_Elipse);
            p2Tiles.Add(TileRight3_Elipse);
            p2Tiles.Add(TileRight2_Elipse);
            p2Tiles.Add(TileRight1_Elipse);

            for (int i = 0; i < p1Tiles.Count; i++)
            {
                _controller.PresentGameState.PlayerOneInitSet[i].Graphic = p1Tiles[i];
                _controller.PresentGameState.PlayerTwoInitSet[i].Graphic = p2Tiles[i];

                Console.WriteLine(_controller.PresentGameState.PlayerOneInitSet[i]);
                Console.WriteLine(_controller.PresentGameState.PlayerTwoInitSet[i]);

            }


        }


        private void ClickOnNode(Border nodeBorder)
        {
            Node chosenNode = FindNodeByNodeBorder(nodeBorder);
            _controller.TheGamePlay(chosenNode);

            #region OldCode
            //if (_controller.GameStatus.IsMorrice > 0)
            //{
            //    Node nodeWithTileToRemove = FindNodeByNodeBorder(nodeBorder);
            //    if (_controller.Turn != nodeWithTileToRemove.TileOn.Owner)
            //    {
            //        _controller.RemoveTile(nodeWithTileToRemove);
            //        _isMorrice--;
            //        CheckAfterRemove();
            //    }

            //}
            //else if (_controller.GameStatus.PlayerOneInitSet.Any() || _controller.GameStatus.PlayerTwoInitSet.Any())
            //{
            //    foreach (Node n in _controller.GameStatus.Board.Board)
            //    {
            //        if (n.TileOn == null && nodeBorder.Name.Contains(n.Id))
            //        {
            //            if (_controller.Turn == Player.PlayerOne)
            //            {
            //                n.TileOn = _controller.GameStatus.PlayerOneInitSet.Last();
            //                _controller.GameStatus.PlayerOneInitSet.Remove(_controller.GameStatus.PlayerOneInitSet.Last());
            //                n.TileOn.Graphic.Visibility = Visibility.Hidden;
            //                n.GraphicRepresentation.Background = n.TileOn.Graphic.Fill;
            //                n.TileOn.Graphic.SetValue(Canvas.LeftProperty, nodeBorder.GetValue(Canvas.LeftProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.RightProperty, nodeBorder.GetValue(Canvas.RightProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.TopProperty, nodeBorder.GetValue(Canvas.TopProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.BottomProperty, nodeBorder.GetValue(Canvas.BottomProperty));
            //                CheckAfterSet(n);
            //            }
            //            else if (_controller.Turn == Player.PlayerTwo)
            //            {
            //                n.TileOn = _controller.GameStatus.PlayerTwoInitSet.Last();
            //                _controller.GameStatus.PlayerTwoInitSet.Remove(_controller.GameStatus.PlayerTwoInitSet.Last());
            //                n.TileOn.Graphic.Visibility = Visibility.Hidden;
            //                n.GraphicRepresentation.Background = n.TileOn.Graphic.Fill;
            //                n.TileOn.Graphic.SetValue(Canvas.LeftProperty, nodeBorder.GetValue(Canvas.LeftProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.RightProperty, nodeBorder.GetValue(Canvas.RightProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.TopProperty, nodeBorder.GetValue(Canvas.TopProperty));
            //                n.TileOn.Graphic.SetValue(Canvas.BottomProperty, nodeBorder.GetValue(Canvas.BottomProperty));
            //                CheckAfterSet(n);
            //            }
            //        }
            //    }
            //}
            //else if (_controller.GameStatus.PlayerOneGoals.Count < 6 && _controller.GameStatus.PlayerTwoGoals.Count < 6)
            //{

            //    Console.WriteLine("Before: {0}", _selectedNode);
            //    if ((CheckTurn() == 1 && nodeBorder.Background == GlobalValues.BRUSH_WHITE)
            //        || (CheckTurn() == 2 && nodeBorder.Background == GlobalValues.BRUSH_BLACK)
            //        || (_selectedNode != null && nodeBorder.Background == GlobalValues.BRUSH_EMPTY))
            //    {
            //        Node clickedNode = FindNodeByNodeBorder(nodeBorder);
            //        if (_selectedNode == null && clickedNode.TileOn != null)
            //        {
            //            //_selectedTile = n.TileOn;
            //            _selectedNode = clickedNode;
            //            _selectedNode.TileOn.LastNode = clickedNode;
            //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
            //        }
            //        else if (_selectedNode != null && clickedNode.TileOn == null)
            //        {
            //            //n.TileOn = _selectedTile;
            //            if (_selectedNode.IsConneced(clickedNode))
            //            {
            //                clickedNode.TileOn = _selectedNode.TileOn;
            //                clickedNode.GraphicRepresentation.Background = clickedNode.TileOn.Graphic.Fill;
            //                _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            //                //_selectedTile = null;
            //                _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            //                _selectedNode.TileOn = null;
            //                _selectedNode = null;
            //                CheckAfterSet(clickedNode);
            //            }

            //        }

            //        else if (_selectedNode == clickedNode)
            //        {
            //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            //            _selectedNode = null;

            //        }

            //        #region OLD CODE
            //        //foreach (Node n in _controller.GameStatus.Board.Board)
            //        //{
            //        //    if (n.GraphicRepresentation == nodeBorder && _selectedNode == null && n.TileOn != null)
            //        //    {
            //        //        //_selectedTile = n.TileOn;
            //        //        _selectedNode = n;
            //        //        _selectedNode.TileOn.LastNode = n;
            //        //        _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
            //        //    }
            //        //    else if (n.GraphicRepresentation == nodeBorder && _selectedNode != null && n.TileOn == null )
            //        //    {
            //        //        //n.TileOn = _selectedTile;
            //        //        if (_selectedNode.IsConneced(n))
            //        //        {
            //        //            n.TileOn = _selectedNode.TileOn;
            //        //            n.GraphicRepresentation.Background = n.TileOn.Graphic.Fill;
            //        //            _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
            //        //            //_selectedTile = null;
            //        //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            //        //            _selectedNode.TileOn = null;
            //        //            _selectedNode = null;
            //        //            CheckAfterSet(n);
            //        //        }

            //        //    }

            //        //    else if (n.GraphicRepresentation == nodeBorder && _selectedNode == n)
            //        //    {
            //        //        _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
            //        //        _selectedNode = null;

            //        //    }
            //        //}
            //        #endregion

            //    }
            //    Console.WriteLine("After: {0}", _selectedNode);

            //}
            //else if (_controller.GameStatus.PlayerOneGoals.Count == 6 || _controller.GameStatus.PlayerTwoGoals.Count == 6)
            //{

            //    Fly(nodeBorder);
            //    //if (_controller.GameStatus.PlayerOneGoals.Count >= 6) // PlayerTwo is flying
            //    //{
            //    //    Fly(nodeBorder);
            //    //}
            //    //else if (_controller.GameStatus.PlayerTwoGoals.Count >= 6) // PlayerOne is flying
            //    //{
            //    //    Fly(nodeBorder);
            //    //}

            //}

            #endregion

        }

        private Node FindNodeByNodeBorder(Border nodeBorder)
        {
            return _controller.PresentGameState.Board.Board.First(n => n.GraphicRepresentation == nodeBorder);
        }


        //private void CheckAfterSet(Node node)
        //{
        //    _controller.GameStatus.IsMorrice = _controller.GameStatus.CheckIfMorriceOnLastMove(node);
        //    //_controller.GameStateO.CheckBoard();
        //    if (_controller.GameStatus.IsMorrice <= 0)
        //    {
        //        _controller.ChangeTurn();
        //    }
        //    _controller.CheckTurnController();
        //}

        //private void CheckAfterRemove()
        //{
        //    if ( _controller.GameStatus.PlayerOneGoals.Count > 6)
        //    {
        //        NavigationService.Navigate(new WinPage(Player.PlayerOne));
        //    }
        //    else if (_controller.GameStatus.PlayerTwoGoals.Count > 6)
        //    {
        //        NavigationService.Navigate(new WinPage(Player.PlayerTwo));
        //    }
        //    else 
        //    {
        //        if (_controller.GameStatus.IsMorrice <= 0)
        //        {
        //            _controller.ChangeTurn();
        //            _controller.CheckTurnController();
        //        }
        //    }
        //}

        //private void Walk(Border nodeBorder)
        //{

        //    if ((CheckTurn() == 1 && nodeBorder.Background == GlobalValues.BRUSH_WHITE)
        //            || (CheckTurn() == 2 && nodeBorder.Background == GlobalValues.BRUSH_BLACK)
        //            || (_selectedNode != null && nodeBorder.Background == GlobalValues.BRUSH_EMPTY))
        //    {
        //        Node clickedNode = FindNodeByNodeBorder(nodeBorder);
        //        if (_selectedNode == null && clickedNode.TileOn != null)
        //        {
        //            //_selectedTile = n.TileOn;
        //            _selectedNode = clickedNode;
        //            _selectedNode.TileOn.LastNode = clickedNode;
        //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
        //        }
        //        else if (_selectedNode != null && clickedNode.TileOn == null)
        //        {
        //            //n.TileOn = _selectedTile;
        //            if (_selectedNode.IsConneced(clickedNode))
        //            {
        //                clickedNode.TileOn = _selectedNode.TileOn;
        //                clickedNode.GraphicRepresentation.Background = clickedNode.TileOn.Graphic.Fill;
        //                _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
        //                //_selectedTile = null;
        //                _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
        //                _selectedNode.TileOn = null;
        //                _selectedNode = null;
        //                CheckAfterSet(clickedNode);
        //            }

        //        }

        //        else if (_selectedNode == clickedNode)
        //        {
        //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
        //            _selectedNode = null;

        //        }
        //    }

        //}

        //private void Fly(Border nodeBorder)
        //{
        //    // TODO: pick to GameController
        //    if ((CheckTurn() == 1 && nodeBorder.Background == GlobalValues.BRUSH_WHITE)
        //            || (CheckTurn() == 2 && nodeBorder.Background == GlobalValues.BRUSH_BLACK)
        //            || (_selectedNode != null && nodeBorder.Background == GlobalValues.BRUSH_EMPTY))
        //    {
        //        Node clickedNode = FindNodeByNodeBorder(nodeBorder);
        //        if (_selectedNode == null && clickedNode.TileOn != null)
        //        {
        //            //_selectedTile = n.TileOn;
        //            _selectedNode = clickedNode;
        //            _selectedNode.TileOn.LastNode = clickedNode;
        //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_YELLOW;
        //        }
        //        else if (_selectedNode != null && clickedNode.TileOn == null)
        //        {
        //            //n.TileOn = _selectedTile;
        //            if( CanFly() )
        //            {
        //                clickedNode.TileOn = _selectedNode.TileOn;
        //                clickedNode.GraphicRepresentation.Background = clickedNode.TileOn.Graphic.Fill;
        //                _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
        //                //_selectedTile = null;
        //                _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
        //                _selectedNode.TileOn = null;
        //                _selectedNode = null;
        //                CheckAfterSet(clickedNode);
        //            }
        //            else if (_selectedNode.IsConneced(clickedNode))
        //            {
        //                clickedNode.TileOn = _selectedNode.TileOn;
        //                clickedNode.GraphicRepresentation.Background = clickedNode.TileOn.Graphic.Fill;
        //                _selectedNode.GraphicRepresentation.Background = GlobalValues.BRUSH_EMPTY;
        //                //_selectedTile = null;
        //                _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
        //                _selectedNode.TileOn = null;
        //                _selectedNode = null;
        //                CheckAfterSet(clickedNode);
        //            }

        //        }

        //        else if (_selectedNode == clickedNode)
        //        {
        //            _selectedNode.GraphicRepresentation.BorderBrush = GlobalValues.BRUSH_TRANSPARENT;
        //            _selectedNode = null;

        //        }
        //    }
        //}

        //private bool CanFly()
        //{
        //    if( CheckTurn() == 1 && _controller.GameStatus.PlayerTwoGoals.Count == 6)
        //    {
        //        return true;
        //    }
        //    else if (CheckTurn() == 2 && _controller.GameStatus.PlayerOneGoals.Count == 6)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
            
        //}

        //private void PerformNode(Border nodeGraphic)
        //{
        //    Console.WriteLine(nodeGraphic.Background.ToString());
        //}



        #region Controlls Binding
        private void FieldA1_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldA1_Border);
            //_controller.GameStatus.Board.Board.ElementAt(0).BorderColor = System.Windows.Media.Brushes.Yellow;
        }

        private void FieldA4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldA4_Border);
        }

        private void FieldA7_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldA7_Border);
        }

        private void FieldB2_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldB2_Border);
        }

        private void FieldB4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldB4_Border);
        }

        private void FieldB6_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldB6_Border);
        }

        private void FieldC3_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldC3_Border);
        }

        private void FieldC4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldC4_Border);
        }

        private void FieldC5_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldC5_Border);
        }

        private void FieldD1_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD1_Border);
        }

        private void FieldD2_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD2_Border);
        }

        private void FieldD3_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD3_Border);
        }

        private void FieldD5_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD5_Border);
        }

        private void FieldD6_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD6_Border);
        }

        private void FieldD7_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldD7_Border);
        }

        private void FieldE3_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldE3_Border);
        }

        private void FieldE4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldE4_Border);
        }

        private void FieldE5_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldE5_Border);
        }

        private void FieldF2_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldF2_Border);
        }

        private void FieldF4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldF4_Border);
        }

        private void FieldF6_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldF6_Border);
        }

        private void FieldG1_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldG1_Border);
        }

        private void FieldG4_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldG4_Border);
        }

        private void FieldG7_Border_Click(object sender, MouseButtonEventArgs e)
        {
            ClickOnNode(FieldG7_Border);
        }

        private void TileRight9_Elipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        private void MoveTurnR_Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((_controller.PresentGameState.Turn == Player.PlayerOne && _valuesContainer["Player1Type"] != 2) || (_controller.PresentGameState.Turn == Player.PlayerTwo && _valuesContainer["Player2Type"] != 2))
            {
                _controller.AutoMove();
            }
        }

        private void MoveTurnL_Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((_controller.PresentGameState.Turn == Player.PlayerOne && _valuesContainer["Player1Type"] != 2) || (_controller.PresentGameState.Turn == Player.PlayerTwo && _valuesContainer["Player2Type"] != 2))
            {
                _controller.AutoMove();
            }
        }
        
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                if ((_controller.PresentGameState.Turn == Player.PlayerOne && _valuesContainer["Player1Type"] != 2) || (_controller.PresentGameState.Turn == Player.PlayerTwo && _valuesContainer["Player2Type"] != 2))
                {
                    _controller.AutoMove();
                }
                e.Handled = true;
            }
        }
    }
}
