using System;
using System.Collections.Generic;
using System.Text;
using TournamentProject.Models;

namespace TrackerFormsProject
{
    public interface IPrizeRequester
    {
        void PrizeComplete(PrizeModel model);
    }
}
