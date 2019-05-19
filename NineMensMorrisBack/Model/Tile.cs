using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace NineMensMorrisBack.Model
{
    public class Tile : ICloneable
    {
        public string Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Player Owner { get; set; }
        public Ellipse Graphic { get; set; }

        public Node LastNode { get; set; }

        public Tile(string id, Player owner, int row, int column)
        {
            Id = id;
            Owner = owner;
            Row = row;
            Column = column;

        }

        public object Clone()
        {
            Tile newTile = new Tile(Id, Owner, Row, Column);

            if (Graphic != null)
                newTile.Graphic = Graphic;

            return newTile;
        }

        public override string ToString()
        {
            String graphicString = "null";
            if( Graphic != null)
            {
                graphicString = "Graphics: " + Graphic.ToString();
            }

            return $"Id: {Id}\n" +
                $"Owner: {Owner}\n" +
                $"Row: {Row}\n" +
                $"Column: {Column}\n" +
                graphicString;
        }


    }
}
