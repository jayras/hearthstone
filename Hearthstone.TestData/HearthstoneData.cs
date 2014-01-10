using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone.Controller;
using System.IO;

namespace Hearthstone.TestData
{
    public class HearthstoneData : IHearthstoneData
    {
        private FileData _fileData;

        public HearthstoneData()
        {
            _fileData = new FileData();
        }

        public Season CurrentSeason
        {
            get { return _fileData.Seasons.First(s => s.Start <= DateTime.Today && s.End >= DateTime.Today); }
        }

        public IReadOnlyList<Season> AllSeasons
        {
            get { return _fileData.Seasons.AsReadOnly(); }
        }

        public IReadOnlyList<Rank> Ranks
        {
            get { return _fileData.AllRanks.AsReadOnly(); }
        }

        public IReadOnlyList<HeroClass> HeroClasses
        {
            get { return _fileData.HeroClasses.AsReadOnly(); }
        }

        public IReadOnlyList<Match> Matches
        {
            get { return _fileData.Matches.AsReadOnly(); }
        }

        public IReadOnlyList<Deck> Decks
        {
            get { return _fileData.Decks.AsReadOnly(); }
        }

        public List<Player> GetAllPlayers()
        {
            return _fileData.Players;
        }

        public Player GetPlayer(int playerID)
        {
            return _fileData.Players.First(p => p.ID == playerID);
        }

        public bool AddPlayer(Player player)
        {
            if (_fileData.Players.Any(p => p.ID == player.ID))
            {
                return UpdatePlayer(player);
            }
            else
            {
                return _fileData.AddPlayer(player);
            }
        }

        public bool UpdatePlayer(Player player)
        {
            return _fileData.UpdatePlayer(player);
        }

        public bool UpdateDeck(Deck deck)
        {
            return _fileData.UpdateDeck(deck);
        }

        public bool DeleteDeck(Deck deck)
        {
            return _fileData.DeleteDeck(deck);
        }

        public bool AddDeck(Deck deck)
        {
            return _fileData.AddDeck(deck);
        }

        public bool UpdateMatch(Match match)
        {
            return _fileData.UpdateMatch(match);
        }

        public bool AddMatch(Match match)
        {
            return _fileData.AddMatch(match);
        }

        public bool DeleteMatch(Match match)
        {
            return _fileData.RemoveMatch(match);
        }

        public bool AddSeason(Season season)
        {
            return _fileData.AddSeason(season);
        }

        public bool UpdateSeason(Season season)
        {
            return _fileData.UpdateSeason(season);
        }

        public bool DeleteSeason(Season season)
        {
            return _fileData.RemoveSeason(season);
        }

        public bool AddRank(Rank rank)
        {
            return _fileData.AddRank(rank);
        }

        public bool UpdateRank(Rank rank)
        {
            return _fileData.UpdateRank(rank);
        }

        public bool DeleteRank(Rank rank)
        {
            return _fileData.RemoveRank(rank);
        }

        public bool DeletePlayerRank(PlayerRank playerRank)
        {
            return _fileData.RemovePlayerRank(playerRank);
        }

        public bool UpdatePlaerRank(PlayerRank playerRank)
        {
            return _fileData.UpdatePlayerRank(playerRank);
        }

        public bool AddPlayerRank(PlayerRank playerRank)
        {
            return _fileData.AddPlayerRank(playerRank);
        }

        public int NextSeasonID { get { return AllSeasons.Max(s => s.ID) + 1; } }
        public int NextRankID { get { return Ranks.Max(r => r.ID) + 1; } }
        public int NextHeroClassID { get { return HeroClasses.Max(h => h.ID) + 1; } }
        public int NextMatchID { get { return Matches.Max(m => m.ID) + 1; } }
        public int NextDeckID { get { return Decks.Max(d => d.ID) + 1; } }
        public int NextPlayerRankID {
            get
            {
                if (AllPlayerRanks.Count > 0)
                {
                    return AllPlayerRanks.Max(p => p.ID);
                }
                else
                {
                    return 1;
                }
            } }


        public IReadOnlyList<PlayerRank> AllPlayerRanks
        {
            get { return _fileData.AllPlayerRanks; }
        }

        public bool UpdatePlayerRank(PlayerRank playerRank)
        {
            return _fileData.UpdatePlayerRank(playerRank);
        }
    }
}
