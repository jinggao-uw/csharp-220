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

namespace Homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameState State { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            State = new GameState(GamerName.X);

            uxTurn.DataContext = State;
        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            // Clear all the units
            ClearAllUnits();

            // And reset the data model state
            State.Reset();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            string[] tags = button.Tag.ToString().Split(',');
            if (tags.Length == 2)
            {
                string currentGamer = State.GamerInTurn.ToString();
                bool result = State.MoveOneStep(System.Convert.ToInt32(tags[0]), System.Convert.ToInt32(tags[1]));
                if (result == true)
                {
                    // Claim the button
                    button.Content = currentGamer;
                }
            }
            else
            {
                // something is wrong with the button tag, do nothing.
            }

            // This could be done via databinding, but not necessarily.
            if (State.GameOver == true)
            {
                DisableAllUnits();
            }
        }


        private void uxMenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void ClearAllUnits()
        {
            foreach (var child in uxGrid.Children)
            {
                if (child is Button)
                {
                    var button = child as Button;
                    button.Content = "";
                    button.IsEnabled = true;
                }
            }
        }

        void DisableAllUnits()
        {
            foreach (var child in uxGrid.Children)
            {
                if (child is Button)
                {
                    var button = child as Button;
                    button.IsEnabled = false;
                }
            }
        }
    }
}
