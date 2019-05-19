using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace NineMensMorrisBack.Model
{
    public class Node: ICloneable, INotifyPropertyChanged
    {
        public string Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Tile LastTile { get; set; }

        public Tile TileOn { get; set; }
        //public TileStatus Status { get; set; }
        

        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        private bool _isSelected;

        public Border GraphicRepresentation { get; set; }

        private Brush borderColor;
        public Brush BorderColor { get; set; }
        

        public bool Select()
        {
            _isSelected = !_isSelected;
            return _isSelected;
        }

        public Node(string id, int row, int column)
        {
            //Status = status;
            Id = id;
            Row = row;
            Column = column;
            TileOn = null;
        }

        public bool IsConneced(Node nextNode)
        {
            if( (nextNode == this.Right) || (nextNode == this.Left) || (nextNode == this.Up) || (nextNode == this.Down))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }


        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"Row: {Row}; Column: {Column}\n";
        }

        public object Clone()
        {
            Node newNode = new Node(Id, Row, Column);
            if (TileOn != null)
            {
                newNode.TileOn = (Tile)TileOn.Clone();
            }

            return newNode;
        }
    }
}
