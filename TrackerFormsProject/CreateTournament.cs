    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentProject;
using TournamentProject.Models;
using TrackerFormsProject;

namespace WindowsFormsApp1
{
    public partial class CreateTournament : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournament()
        {
            InitializeComponent();

            WireUpLists();  
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            tournamentPrizesListBox.DataSource = null;
            tournamentPrizesListBox.DataSource = selectedPrizes;
            tournamentPrizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void deleteSelectedTeamsButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }
        }

        private void deleteSelectedPrizesButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)tournamentPrizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);
           
                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the CreatePrizeForm
            CreatePrize frm = new CreatePrize(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            // Get back from the form a PrizeModel
            // Put that PrizeModel into the list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeam frm = new CreateTeam(this);
            frm.Show();
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            
            if(feeAcceptable == false)
            {
                MessageBox.Show("You need to enter a valid Entry Fee.", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }   

            // Create the tournament model

            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // TODO - Wire our matchups
            TournamentLogic.CreateRounds(tm);

            // Create Tournament entry
            // Save all the prizes entries
            // Create all the team 
            GlobalConfig.Connection.CreateTournament(tm);

            //tm.AlertUsersToNewRound(); // The AlertUserts does not work.

            TournamentViewer frm = new TournamentViewer(tm);
            frm.Show();
            this.Close();
        }
    }
}
