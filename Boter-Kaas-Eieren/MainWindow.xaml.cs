using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Boter_Kaas_Eieren
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        # region Variables

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if Player 1's turn (X), False if Player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended, False if game is still going on
        /// </summary>
        private bool mGameEnded;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Newgame();

        }

        #endregion

        private void Newgame()
        {
            //Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Player 1 starts the game
            mPlayer1Turn = true;

            //For every button on the grid Container
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //Clear text of buttons
                button.Content = string.Empty;
                button.Background = Brushes.Azure;
            });

            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //starts a new game on the click after the game is finished
            if (mGameEnded)
            {
                Newgame();
                return;
            }
            //Cast the sender to a button
            var button = (Button)sender;

            //Finds the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            //Don't do anything if the clicked button isn't free
            if (mResults[index] != MarkType.Free)
                return;

            //Button value is set X or O based on whose turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Button context is set to X or O
            button.Content = mPlayer1Turn ? "X" : "O";

            //Button Foreground is Indigo for X (Player 1), Crimson for O (player 2)
            button.Foreground = mPlayer1Turn ? Brushes.Indigo : Brushes.Crimson;            

            //Toggle player turn
            mPlayer1Turn ^= true;

            //Check for a winner
            CheckForWinner();
        }
        private void CheckForWinner()
        {
        #region Rows
            #region Row 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Chartreuse;
            }
            #endregion
            #region Row 2
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Chartreuse;
            }
            #endregion
            #region Row 3
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Chartreuse;
            }
            #endregion
        #endregion

        #region Columns
            #region Column 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Chartreuse;
            }
            #endregion
            #region Column 2
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Chartreuse;
            }
            #endregion
            #region Column 1
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Chartreuse;
            }
            #endregion
            #endregion

        #region Diagonals
            #region Left Top -> Right Bottom
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Chartreuse;
            }
            #endregion
            #region Left Bottom -> Right Top
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button_0_2.Background = Button_1_1.Background = Button_2_0.Background = Brushes.Chartreuse;
            }
            #endregion
            #endregion

        #region No Winner
            if (!mResults.Any(cell => cell == MarkType.Free))
            {
                mGameEnded = true;                
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Background = Brushes.Gold;
            });
        #endregion

            }
        }
    }
}
  