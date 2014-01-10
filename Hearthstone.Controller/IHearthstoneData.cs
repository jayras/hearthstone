using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public interface IHearthstoneData
	{
		// Meta Data
        Season CurrentSeason { get; }
        IReadOnlyList<Season> AllSeasons { get; }
        IReadOnlyList<Rank> Ranks { get; }
        IReadOnlyList<PlayerRank> AllPlayerRanks {get;}
        IReadOnlyList<HeroClass> HeroClasses { get; }
        IReadOnlyList<Match> Matches { get; }
        IReadOnlyList<Deck> Decks { get; }

        int NextSeasonID { get; }
        int NextRankID { get; }
        int NextHeroClassID { get; }
        int NextMatchID { get; }
        int NextDeckID { get; }
	    int NextPlayerRankID { get; }

	    // Player Data
		List<Player> GetAllPlayers();
		Player GetPlayer(int playerID);
		bool AddPlayer(Player player);
		bool UpdatePlayer(Player player);
		bool UpdateDeck(Deck deck);
		bool DeleteDeck(Deck deck);
		bool AddDeck(Deck deck);

		// Match Data (For a player)
		bool UpdateMatch(Match match);
		bool AddMatch(Match match);
		bool DeleteMatch(Match match);

		//Season and Ranks
		bool AddSeason(Season season);
		bool UpdateSeason(Season season);
        bool DeleteSeason(Season season);
		bool AddRank(Rank rank);
		bool UpdateRank(Rank rank);
        bool DeleteRank(Rank rank);
        bool AddPlayerRank(PlayerRank playerRank);
        bool UpdatePlayerRank(PlayerRank playerRank);
        bool DeletePlayerRank(PlayerRank playerRank);


	}
}
