using System.Collections.Generic;
using System.Linq;
using TournamentProject.DataAccess.TextHelpers;
using TournamentProject.Models;

namespace TournamentProject.DataAccess
{
    public class TextConnector : IDataConection
    {
        public void CreatePerson(PersonModel model)
        {
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int curentId = 1;

            if (people.Count > 0)
            {
                curentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = curentId;

            people.Add(model);

            people.SaveToPeopleFile();
        }

        // TODO - Wire up the CreatePrize for text files
        public void CreatePrize(PrizeModel model)
        {
            //* Load the text file
            //* Convert the text to List<PrizeModel>
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //* Find the max ID
            //* Add a new record with the new ID = max + 1
            int curentId = 1;

            if (prizes.Count > 0)
            {
                curentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = curentId;

            prizes.Add(model);

            //Convert the prizes to a List<string>
            //Save the List<string> to the text file
            prizes.SaveToPrizeFile();
        }
        public void CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();

            int curentId = 1;

            if (teams.Count > 0)
            {
                curentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = curentId;

            teams.Add(model);

            teams.SaveToTeamFile();
        }
        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();

            int curentId = 1;

            if (tournaments.Count > 0)
            {
                curentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = curentId;

            model.SaveRoundsToFile();

            tournaments.Add(model);

            tournaments.SaveToTournamentFile();

            TournamentLogic.UpdateTournamentResults(model);
        }
        public void UpdateMatchup(MatchupModel model)
        {
            // Update the Matchups
            // Update the MatchupEntries
            model.UpdateMatchupToFile();
        }

        public List<PersonModel> GetPerson_All()
        {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
        public List<TeamModel> GetTeam_All()
        {
            return GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }
        public List<TournamentModel> GetTournament_All()
        {
            return GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
        }

        public void CompleteTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();

            tournaments.Remove(model); //remove the complete tournament

            tournaments.SaveToTournamentFile();

            TournamentLogic.UpdateTournamentResults(model);
        }
    }
}
