using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
    public class PlayerData : IPlayerData
    {
        private IHearthstoneData _hearthStoneData;
        private Player _player;

        public PlayerData(IHearthstoneData hearthstoneData, Player player)
        {
            _hearthStoneData = hearthstoneData;
            _player = player;
        }

        public Player Player
        {
            get
            {
                _player = _hearthStoneData.GetPlayer(_player.ID);
                return _player;
            }
        }

        public Season CurrentSeason
        {
            get { return _hearthStoneData.CurrentSeason; }
        }

        public IReadOnlyList<Deck> Decks
        {
            get { return _hearthStoneData.Decks.Where(d => d.Player.ID == _player.ID).ToList().AsReadOnly();  }
        }

        public IReadOnlyList<HeroClass> HeroClasses
        {
            get { return _hearthStoneData.HeroClasses; }
        }

        public IReadOnlyList<PlayerRank> PlayerRanks
        {
            get { return _hearthStoneData.AllPlayerRanks.Where(p => p.Player.ID == _player.ID).ToList().AsReadOnly(); }
        }

        public IReadOnlyList<Match> Matches
        {
            get { return _hearthStoneData.Matches.Where(m => m.Player.ID == _player.ID).ToList().AsReadOnly(); }
        }
    }
}