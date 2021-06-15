using System;
using System.Drawing;
using System.Windows.Forms;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    public partial class GameForm : Form
    {
        private readonly GameSettings m_Settings;
        public Button[,] m_MatrixButtons;
        private Game m_Game;
        private TableLayoutPanel m_tableLayoutPanel;
        private Point m_Location;

        public GameForm(GameSettings settings)
        {
            m_Settings = settings;
            FormClosed += GameForm_FormClosed;
            initGameBySettings(settings);
            addFormComponentAsListenersToGame();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void initGameBySettings(GameSettings settings)
        {

            Controls.Clear();
            int sizeBoard = settings.BoardSize + 1;

            eTypeGame TypeGame = settings.TypeGame;

            this.Text = "Othello - Red's Turn";
            this.BackColor = Color.AliceBlue;
            this.StartPosition = FormStartPosition.CenterScreen;
            m_MatrixButtons = new Button[sizeBoard, sizeBoard];

            m_tableLayoutPanel = new TableLayoutPanel();
            m_Location = new Point(5 * sizeBoard + 15, 5 * sizeBoard);
            m_tableLayoutPanel.Location = m_Location;
            this.m_tableLayoutPanel.Size = new Size(50 * sizeBoard, 50 * sizeBoard);
            m_tableLayoutPanel.RowCount = sizeBoard;
            m_tableLayoutPanel.ColumnCount = sizeBoard;
            this.Controls.Add(m_tableLayoutPanel);
            this.Size = new Size(60 * sizeBoard, 60 * sizeBoard);
            createBoardGame();
            m_Game = new Game(sizeBoard, TypeGame);

        }

        private void addFormComponentAsListenersToGame()
        {
            m_Game.m_GameButtonEventHandler += this.ButtonChange;
            m_Game.m_GameWindowEventHandler += this.SwitchWindowTitel;
            m_Game.m_GameMessageEventHandler += this.ShowMessageBox;
            m_Game.m_ValidButtonGameEventHandler += this.ButtonMakeValid;
            m_Game.m_GameIntializeButtonGameEventHandler += this.InitializeButton;
            m_Game.Initialize_Matrix();
        }

        public void SwitchWindowTitel(bool i_Flag, char i_CurrentPlayer)
        {
            string windowTitel;
            string player;

            if (i_Flag)
            {
                if (i_CurrentPlayer == 'X')
                {
                    this.Text = "Othello - Red's Turn";
                }
                else
                {
                    this.Text = "Othello - Yellow's Turn";
                }
            }
            else
            {
                if (i_CurrentPlayer == 'X')
                {
                    player = "Red's";
                }
                else
                {
                    player = "Yellow's";
                }
                windowTitel = string.Format("no moves skip to - {0} turn", player);
                this.Text = windowTitel;
            }
        }

        public void ShowMessageBox(char i_Winner, int i_OCounter, int i_XCounter, int i_ORoundsWinnigCount, int i_XRoundsWinnigCount)
        {
            string caption = "Othello";
            string winner = " ";
            int winnerGame = 0;
            int loser = 0;
            int RoundWinner = 0;
            int RoundLoser = 0; 
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string message;

            if (i_Winner == 'X')
            {
                winner = "Red Won!!";
                winnerGame = i_XCounter;
                loser = i_OCounter;
                RoundWinner = i_XRoundsWinnigCount;
                RoundLoser= i_ORoundsWinnigCount; 
            }
            else if (i_Winner == 'O')
            {
                winner = "Yellow Won!!";
                winnerGame = i_OCounter;
                loser = i_XCounter;
                RoundWinner = i_ORoundsWinnigCount;
                RoundLoser = i_XRoundsWinnigCount;
            } // Tie
            else if(i_Winner == 'T')
            {
                winner = "It's A Tie!!";
                winnerGame = i_XCounter;
                loser = i_OCounter;
                RoundWinner = i_XRoundsWinnigCount;
                RoundLoser = i_ORoundsWinnigCount;
            }
            message = string.Format("{0}" + " " + "(" + winnerGame.ToString() + "/" + loser.ToString() + ")" + " " +
                "(" + " " + RoundWinner.ToString() + "/" +
                RoundLoser.ToString() + ")" + Environment.NewLine + Environment.NewLine
                 + "Would you like another round?", winner);
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                initGameBySettings(m_Settings);
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                Application.Exit();
            }
        }

        public void InitializeButton(int i_RowIndex, int i_ColumnIndex, char i_Player)
        {
            int i = i_RowIndex;
            int j = i_ColumnIndex;

            if (i_Player.ToString() == "X")
            {
                m_MatrixButtons[i, j].BackgroundImage = B19_Ex05_Othelo_SimaWeiss_309823011.Properties.Resources.CoinRed;
            }
            else if (i_Player.ToString() == "O")
            {
              
                m_MatrixButtons[i, j].BackgroundImage = B19_Ex05_Othelo_SimaWeiss_309823011.Properties.Resources.CoinYellow;
            }
        }

        public void ButtonChange(int i_RowIndex, int i_ColumnIndex, char i_Player)
        {
            if (i_Player == 'X')
            {
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].BackgroundImage = B19_Ex05_Othelo_SimaWeiss_309823011.Properties.Resources.CoinRed;
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].Enabled = false;
            }
            else if (i_Player == 'O')
            {
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].BackgroundImage = B19_Ex05_Othelo_SimaWeiss_309823011.Properties.Resources.CoinYellow;
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].Enabled = false;
            }
        }

        public void ButtonMakeValid(int i_RowIndex, int i_ColumnIndex, bool i_Enabled)
        {
            if (i_Enabled)
            {
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].BackColor = Color.Green;
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].Enabled = true;
            }
            else
            {
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].BackColor = default(Color);
                m_MatrixButtons[i_RowIndex, i_ColumnIndex].Enabled = false;
            }
        }

        private void createBoardGame()
        {
            for (int i = 1; i < m_Settings.BoardSize + 1; i++)
            {
                for (int j = 1; j < m_Settings.BoardSize + 1; j++)
                {
                    m_MatrixButtons[i, j] = new Button();
                    m_MatrixButtons[i, j].Size = new Size(45, 45);
                    m_MatrixButtons[i, j].Click += button_Click;

                    m_MatrixButtons[i, j].Enabled = false;

                    this.m_tableLayoutPanel.Controls.Add(m_MatrixButtons[i, j]);
                    m_tableLayoutPanel.SetRow(m_MatrixButtons[i, j], i);

                    m_tableLayoutPanel.SetColumn(m_MatrixButtons[i, j], j);
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            int row = 0;
            int column = 0;
            Button buttonSelected = sender as Button;

            column = m_tableLayoutPanel.GetColumn(buttonSelected);
            row = m_tableLayoutPanel.GetRow(buttonSelected);
            m_Game.PlayGame(row, column);
        }
    }
}
