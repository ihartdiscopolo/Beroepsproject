using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysteryNumber_Dominika
{
    public partial class MysteryNumber : Form
    {
        //Define variables
        Random GenerateRandomValue = new Random();
        int maxvalueDdeb = 0;
        int minvalueDdeb = 0;
        int mysteryNumberDdeb = 0;
        int attemptsLeftDdeb = 0;
        public MysteryNumber()
        {
            InitializeComponent();
            gbxPlayDdeb.Enabled = false;
        }

        private void MysteryNumber_Load(object sender, EventArgs e)
        {
            pbxAlienDdeb.Left = gbxSetupDdeb.Left;
            pbxAlienDdeb.Left = gbxPlayDdeb.Left;
            pbxAlienDdeb.Left = gbxInformationDdeb.Left;
            btnStartTheGameDdeb.Left = 55;
            lblDominikDdeb.Left = 18;
            this.Width = pbxAlienDdeb.Width + 20;
            this.Text = "Dominik Debska";
        }

        private void btnStartTheGameDdeb_Click(object sender, EventArgs e)
        {
            pbxAlienDdeb.Visible = false;
            btnStartTheGameDdeb.Visible = false;
            lblDominikDdeb.Visible = false;
            btnStartTheGameDdeb.Visible = false;
        }

        private void btnGoDdeb_Click(object sender, EventArgs e)
        {
            try
            {
                minvalueDdeb = int.Parse(tbxStartAtDdeb.Text);
                maxvalueDdeb = int.Parse(tbxStopAtDdeb.Text);

                if (minvalueDdeb > maxvalueDdeb)
                {
                    MessageBox.Show("Minimum value must be less than maximum value!");
                    LogMessage("Error, invalid range values.");
                    return;
                }

                if (minvalueDdeb < 5 || minvalueDdeb >= maxvalueDdeb || minvalueDdeb > 10)
                {
                    MessageBox.Show("Minimum value cannot be less than 5, or greater than 10!");
                    LogMessage("Error, invalid range values.");
                    return;
                }

                mysteryNumberDdeb = GenerateRandomValue.Next(minvalueDdeb, maxvalueDdeb);

                pbAttemptsLeftDdeb.Maximum = attemptsLeftDdeb;
                pbAttemptsLeftDdeb.Value = attemptsLeftDdeb;
                pbAttemptsLeftDdeb.Minimum = 0;
                pbWrongDdeb.Maximum = attemptsLeftDdeb;
                pbWrongDdeb.Value = 0;

                LogMessage($"A random number between {minvalueDdeb} and {maxvalueDdeb} has been generated!!");
                MessageBox.Show($"You have {attemptsLeftDdeb} attempts to guess the number!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occurred, {ex.Message}");
                LogMessage($"Error, {ex.Message}");
            }
            gbxPlayDdeb.Enabled = true;
        }

        private void btnGuessDdeb_Click(object sender, EventArgs e)
        {
            try
            {
                if (mysteryNumberDdeb == 0)
                {
                    MessageBox.Show($"Please try to generate a number first!");
                    LogMessage("Error, no mysterynumber..");
                    return;
                }
                int userGuess = int.Parse(tbxMyGuessDdeb.Text);
                if (userGuess < minvalueDdeb || userGuess > maxvalueDdeb)
                {
                    MessageBox.Show($"Please guess a number between {minvalueDdeb} and {maxvalueDdeb}");
                    LogMessage($"Error, {userGuess} is out of range!");
                    return;
                }
                if (userGuess == mysteryNumberDdeb)
                {
                    MessageBox.Show($"{tbxEnterYourNameDdeb.Text} You have guessed the mysterynumber! :3");
                    LogMessage("Yippe!");

                    DialogResult playAgain = MessageBox.Show("Play again?", "Play again", MessageBoxButtons.YesNo);
                    if (playAgain == DialogResult.Yes)
                    {
                        ResetGame();
                    }
                    else
                    {
                        Application.Exit();
                    }

                }
                pbWrongDdeb.Value++;
                attemptsLeftDdeb--;
                MessageBox.Show($"Wrong guess attempts left {attemptsLeftDdeb}");
                LogMessage($"Wrong guess {userGuess} try again!");
                pbAttemptsLeftDdeb.Value = attemptsLeftDdeb;

                int difference = Math.Abs(mysteryNumberDdeb - userGuess);
                UpdateTemperatureMeter(difference);

                if (attemptsLeftDdeb <= 0)
                {
                    MessageBox.Show($"Game over, mystery number was {mysteryNumberDdeb}");
                    LogMessage("You suck");
                    DialogResult playAgain = MessageBox.Show("Do you want to play again??", "Play again?", MessageBoxButtons.YesNo);
                    if (playAgain == DialogResult.Yes)
                    {
                        ResetGame();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number >:(");
                LogMessage("Stop being stupid");
            }


        }
        private void UpdateTemperatureMeter(int difference)
        {
            try
            {
                int maxDifference = Math.Max(maxvalueDdeb - minvalueDdeb, 1);
                int trackBarValue = (int)((1.0 - (double)difference / maxDifference) * tbHotColdDdeb.Maximum);
                tbHotColdDdeb.Value = Math.Max(0, Math.Min(trackBarValue, tbHotColdDdeb.Maximum));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred while updating the tempature meter {ex.Message}");
                LogMessage($"Unable to update temperature meter :'( {ex.Message}");
            }
        }
        private void ResetGame()
        {
            mysteryNumberDdeb = 0;
            attemptsLeftDdeb = 0;
            tbxStartAtDdeb.Clear();
            tbxStopAtDdeb.Clear();
            tbxMyGuessDdeb.Clear();
            tbxNumberOfAttempsDdeb.Clear();
            pbAttemptsLeftDdeb.Value = 0;
            pbWrongDdeb.Value = 0;
            rtbxInformationDdeb.Clear();
            gbxPlayDdeb.Enabled = false;
        }

        private void btnAboutDdeb_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Dominik Debska :3");
            LogMessage("About Dominik Debska");
        }

        private void btnClearDdeb_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void btnCheatDdeb_Click(object sender, EventArgs e)
        {
            tbxMyGuessDdeb.Text = mysteryNumberDdeb.ToString();
            MessageBox.Show($"The mystery number is filled in for you.");
            LogMessage($"Cheat used: {mysteryNumberDdeb}");

            if (mysteryNumberDdeb == 0)
            {
                MessageBox.Show("Please generate a mystery number first!!");
                LogMessage("");
            }
            else
            {
                MessageBox.Show($"The mystery number was {mysteryNumberDdeb}");
                LogMessage($"Cheat : {mysteryNumberDdeb}");
            }
        }

        private void btnLocateDdeb_Click(object sender, EventArgs e)
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            MessageBox.Show($"The game is located at:\n{appPath}", "Locate Application");
            LogMessage($"Location: \n{appPath}");
        }
        private void LogMessage(string message)
        {
            rtbxInformationDdeb.AppendText($"{DateTime.Now}: {message} {Environment.NewLine}");
            rtbxInformationDdeb.ScrollToCaret();
        }

        
        private void btnDefaultDdeb_Click(object sender, EventArgs e)
        {
            tbxStartAtDdeb.Text = "5";
            tbxStopAtDdeb.Text = "100";
            tbxNumberOfAttempsDdeb.Text = "5";
        }
    }
}
