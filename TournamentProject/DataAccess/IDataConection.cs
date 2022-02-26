using System;
using System.Collections.Generic;
using System.Text;
using TournamentProject.Models;

namespace TournamentProject.DataAccess
{
    public interface IDataConection
    {
        void CreatePrize(PrizeModel model);
        void CreatePerson(PersonModel model);
        void CreateTeam(TeamModel model);
        void CreateTournament(TournamentModel model);
        void UpdateMatchup(MatchupModel model);
        void CompleteTournament(TournamentModel model);

        List<PersonModel> GetPerson_All();
        List<TeamModel> GetTeam_All();
        List<TournamentModel> GetTournament_All();
    }
}
