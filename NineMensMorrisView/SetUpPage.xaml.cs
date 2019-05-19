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

        public SetUpPage()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new GamePage(_player1Type, _player2Type));
        }

        private void SetGameProperties()
        {
            if( (bool)P1_Manual_RadioButton.IsChecked)
            {
                _player1Type = 2;
            }
            else if ( (bool)P1_MinMax_RadioButton.IsChecked)
            {
                _player1Type = 0;
            }
            else if((bool) P1_AlphaBeta_RadioButton.IsChecked)
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

        }


    }
}
