using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Card
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public int ManaCost { get; set; }
		public bool isMinion { get; set; }
		public int? Attack { get; set; }
		public int? Health { get; set; }
		public string CardText { get; set; }
		public Rarity CardRarity { get; set; }
		public HeroClass CardClass { get; set; }

	}
}
