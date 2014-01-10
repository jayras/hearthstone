using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Match
	{
		public int ID { get; set; }
		public Player Player { get; set; }
		public HeroClass HeroClass { get; set; }
		public Deck Deck { get; set; }
		public MatchType Type { get; set; }
		public HeroClass OpponentClass { get; set; }
		public string OpponentName { get; set; }
		public Rank OpponentRank { get; set; }
		public Rank StartingRank { get; set; }
		public Rank EndingRank { get; set; }
		public bool HaveCoin { get; set; }
		public bool HaveWon { get; set; }
		public int Rounds { get; set; }
		public bool DidConcede { get; set; }
		public string Notes { get; set; }


        public override string ToString()
        {
            string matchFormat = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}";
            StringBuilder thisString = new StringBuilder();
            thisString.AppendFormat(matchFormat,ID,Player.ID,HeroClass.ID,Deck.ID,Type,OpponentClass.ID,OpponentName,OpponentRank.ID,StartingRank.ID,StartingRank.Stars,EndingRank.ID,EndingRank.Stars,HaveCoin.ToString(),HaveWon,Rounds,DidConcede,Notes);
            return thisString.ToString();
        }

	}
}
