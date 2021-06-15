using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    public partial class SettingsForm : Form
    {
        private readonly GameSettings r_Settings = new GameSettings();

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void BoardSizeButton_Click(object sender, EventArgs e)
        {
            r_Settings.BoardSize += 2;
            if (r_Settings.BoardSize == 14)
            {
                r_Settings.BoardSize = 6;
            }

            string buttonText = string.Format("Board Size: {0}x{0} (Click to increase)", r_Settings.BoardSize);
            BoardSizeButton.Text = buttonText;
        }

        private void PlayAgainstComputerButton_Click(object sender, EventArgs e)
        {
            r_Settings.TypeGame = eTypeGame.AgainstComputer; 
            StartGame();
        }

        private void PlayAgainstFriendButton_Click(object sender, EventArgs e)
        {
            r_Settings.TypeGame = eTypeGame.AgainstPlayer;
            StartGame();
        }

        private void StartGame()
        {
            GameForm gameForm = new GameForm(r_Settings);
            gameForm.Show();
            Hide();
        }
    }
}
