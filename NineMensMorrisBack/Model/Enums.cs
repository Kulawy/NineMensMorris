using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorrisBack.Model
{
    public enum NodeStatus
    {
        Blank,
        P1,
        P2    
    }

    public enum Player
    {
        PlayerOne,
        PlayerTwo,
        Empty
    }

    public enum GameStage
    {
        Placing,
        PickMovingElement,
        PickMovingDestination,
        PickFlyElement,
        PickFlyDestination,
        Morrice,
        GameOver
    }



}
