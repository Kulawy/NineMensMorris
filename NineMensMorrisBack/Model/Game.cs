using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Model
{
    public class Game: ReactiveObject
    {
        private string _winner = "";
        private GameStage _state;

        private HashSet<Node> _tilesOnBoard;

        public Game()
        {
            State = GameStage.Placing;

        }

        public HashSet<Node> TilesOnBoard
        {
            get { return _tilesOnBoard; }
            set { this.RaiseAndSetIfChanged(ref _tilesOnBoard, value); }
        }


        public GameStage State
        {
            get { return _state; }
            set { this.RaiseAndSetIfChanged(ref _state, value); }
        }
        
        public string Winner
        {
            get { return Winner; }
            set { this.RaiseAndSetIfChanged(ref _winner, value); }
        }
        


    }
}
