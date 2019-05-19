using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Model
{
    public class Morris
    {
        public int Id { get; }
        public Tile One { get; set; }
        public Tile Two { get; set; }
        public Tile Three { get; set; }

        public Morris(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            bool flag;
            Morris compering = obj as Morris;
            if( One.Equals(compering.One) && Two.Equals(compering.Two) && Three.Equals(compering.Three))
            {
                flag = true;
            }
            else if (One.Equals(compering.Two) && Two.Equals(compering.One) && Three.Equals(compering.Three))
            {
                flag = true;
            }
            else if (One.Equals(compering.One) && Two.Equals(compering.Three) && Three.Equals(compering.Two))
            {
                flag = true;
            }
            else if (One.Equals(compering.Three) && Two.Equals(compering.Two) && Three.Equals(compering.One))
            {
                flag = true;
            }
            else if (One.Equals(compering.Two) && Two.Equals(compering.Three) && Three.Equals(compering.One))
            {
                flag = true;
            }
            else if (One.Equals(compering.Three) && Two.Equals(compering.One) && Three.Equals(compering.Two))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            
            return flag;
        }


    }
}
