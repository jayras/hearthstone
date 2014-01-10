using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Deck
	{
		public int ID { get; set; }
		public Player Player { get; set; }
		public HeroClass Class { get; set; }
		public string Name { get; set; }
		public string DeckURL { get; set; }
		public List<Card> Cards { get; set; }
		public string Version { get; set; }
        public string PickDeckString { get { return Name + " (" + Class.ClassName + ")";  } }

		public Deck()
		{
			Cards = new List<Card>();
		}

        public override string ToString()
        {
            return ID + "\t" + Player.ID + "\t" + Class.ID + "\t" + Name + "\t" + DeckURL + "\t" + Version;
        }
	}
}
