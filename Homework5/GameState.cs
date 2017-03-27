using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    public enum GamerName
    {
        O = 0,
        X = 1
    }

    public class GameState : INotifyPropertyChanged
    {
        // Private variables
        private const int GameSize = 3;
        private GamerName initialStarter;
        private GamerName?[,] matrix;

        // Properties
        public GamerName GamerInTurn { get; set; }
        public bool GameOver { get; set; }
        public GamerName? Winner { get; set; }

        // DependencyProperties
        private string gameMessage;
        public string Message
        {
            get
            {
                return gameMessage;
            }
            set
            {
                if (gameMessage != value)
                {
                    gameMessage = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Constructor
        public GameState(GamerName starter)
        {
            initialStarter = starter;
            matrix = new GamerName?[GameSize, GameSize];

            Reset();
        }

        // Reset the states
        public void Reset()
        {
            GamerInTurn = initialStarter;
            GameOver = false;
            Message = GamerInTurn.ToString() + "'s turn";

            for (int i = 0; i < GameSize; i++)
            {
                for (int j = 0; j < GameSize; j++)
                {
                    matrix[i, j] = null;
                }
            }

            Winner = null;
        }

        // Move one step for current gamer
        public bool MoveOneStep(int x, int y)
        {
            if (GameOver == true)
            {
                // We've already found a winner
                Message = @"Game is over";
                return false;
            }

            if (IsOccupiedOrInvalid(x, y))
            {
                // Can't place here at this step
                Message = @"Can't place here";
                return false;
            }

            SetOccupied(x, y);
            Evaluate();

            // flip the gamer
            GamerInTurn = (GamerInTurn == GamerName.O) ? GamerName.X : GamerName.O;

            if (GameOver == true)
            {
                Message = Winner.Value.ToString() + " is the winner";
            }
            else
            {

                Message = GamerInTurn.ToString() + "'s turn";
            }

            return true;
        }

        // Check if the passed in position is valid
        private bool IsOccupiedOrInvalid(int x, int y)
        {
            if (x < 0 || y < 0 || x > GameSize - 1 || y > GameSize - 1)
            {
                return true;
            }

            if (matrix[x, y].HasValue)
            {
                return true;
            }

            return false;
        }

        // Mark the position in the state matrix
        private void SetOccupied(int x, int y)
        {
            matrix[x, y] = GamerInTurn;
        }

        // Evaluate the current game state against the state matrix, and update the properties accordingly.
        private void Evaluate()
        {
            // Check all vertical and horizontal lines
            for (int i = 0; i < GameSize; i++)
            {
                GameOver = (matrix[i, 0] == GamerInTurn && matrix[i, 1] == GamerInTurn && matrix[i, 2] == GamerInTurn)
                    || (matrix[0, i] == GamerInTurn && matrix[1, i] == GamerInTurn && matrix[2, i] == GamerInTurn);

                if (GameOver)
                {
                    break;
                }
            }

            // Check two cross lines
            if (GameOver == false)
            {
                GameOver = (matrix[0, 0] == GamerInTurn && matrix[1, 1] == GamerInTurn && matrix[2, 2] == GamerInTurn)
                    || (matrix[0, 2] == GamerInTurn && matrix[1, 1] == GamerInTurn && matrix[2, 0] == GamerInTurn);
            }

            // Declare the winner
            if(GameOver)
            {
                Winner = GamerInTurn;
            }

            return;
        }
    }
}
