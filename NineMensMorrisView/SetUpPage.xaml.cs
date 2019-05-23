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
    /// Interaction logic for SetUpPage.xaml
    /// </summary>
    public partial class SetUpPage : Page
    {
        //0 - min max
        //1 - alpha beta
        private int _player1Type;
        private int _player2Type;

        private int _player1CalculateHeuristicType;
        private int _player2CalculateHeuristicType;

        private int _player1GameHeuristicType;
        private int _player2GameHeuristicType;

        public SetUpPage()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetGameProperties();

            this.NavigationService.Navigate(new GamePage(CompressValuesToOne()));
        }

        private Dictionary<string, int> CompressValuesToOne()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("Player1Type", _player1Type);
            dict.Add("Player2Type", _player2Type);
            dict.Add("Player1CalculateHeuristicType", _player1CalculateHeuristicType);
            dict.Add("Player2CalculateHeuristicType", _player2CalculateHeuristicType);
            dict.Add("Player1GameHeuristicType", _player1GameHeuristicType);
            dict.Add("Player2GameHeuristicType", _player2GameHeuristicType);

            return dict;
        }

        private void SetGameProperties()
        {
            SetPlayersType();
            SetCalculateRatioHeuristicType();
            SetGameHeuristicType();

        }

        private void SetGameHeuristicType()
        {
            if ((bool)P1_GameHeuristic1_RadioButton.IsChecked)
            {
                _player1GameHeuristicType = 1;
            }
            else if ((bool)P1_GameHeuristic2_RadioButton.IsChecked)
            {
                _player1GameHeuristicType = 2;
            }
            else if ((bool)P1_GameHeuristic3_RadioButton.IsChecked)
            {
                _player1GameHeuristicType = 3;
            }

            if ((bool)P2_GameHeuristic1_RadioButton.IsChecked)
            {
                _player2GameHeuristicType = 1;
            }
            else if ((bool)P2_GameHeuristic2_RadioButton.IsChecked)
            {
                _player2GameHeuristicType = 2;
            }
            else if ((bool)P2_GameHeuristic3_RadioButton.IsChecked)
            {
                _player2GameHeuristicType = 3;
            }

        }

        private void SetCalculateRatioHeuristicType()
        {
            if ((bool)P1_CalculateHeuristic1_RadioButton.IsChecked)
            {
                _player1CalculateHeuristicType = 1;
            }
            else if ((bool)P1_CalculateHeuristic2_RadioButton.IsChecked)
            {
                _player1CalculateHeuristicType = 2;
            }
            else if ((bool)P1_CalculateHeuristic3_RadioButton.IsChecked)
            {
                _player1CalculateHeuristicType = 3;
            }

            if ((bool)P2_CalculateHeuristic1_RadioButton.IsChecked)
            {
                _player2CalculateHeuristicType = 1;
            }
            else if ((bool)P2_CalculateHeuristic2_RadioButton.IsChecked)
            {
                _player2CalculateHeuristicType = 2;
            }
            else if ((bool)P2_CalculateHeuristic3_RadioButton.IsChecked)
            {
                _player2CalculateHeuristicType = 3;
            }
        }

        private void SetPlayersType()
        {
            if ((bool)P1_Manual_RadioButton.IsChecked)
            {
                _player1Type = 2;
            }
            else if ((bool)P1_MinMax_RadioButton.IsChecked)
            {
                _player1Type = 0;
            }
            else if ((bool)P1_AlphaBeta_RadioButton.IsChecked)
            {
                _player1Type = 1;
            }

            if ((bool)P2_MinMax_RadioButton.IsChecked)
            {
                _player2Type = 0;
            }
            else if ((bool)P2_AlphaBeta_RadioButton.IsChecked)
            {
                _player2Type = 1;
            }
            else if ((bool)P2_Manual_RadioButton.IsChecked)
            {
                _player2Type = 2;
            }
        }



    }

    internal class Dictionery<T1, T2>
    {
        public Dictionery()
        {
        }
    }
}
