using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public interface IPlayerData
	{
		Player Player { get; }
		Season CurrentSeason { get; }
        IReadOnlyList<Deck> Decks { get; }
        IReadOnlyList<HeroClass> HeroClasses { get; }
        IReadOnlyList<PlayerRank> PlayerRanks { get; }
        IReadOnlyList<Match> Matches { get; }
	}
}
