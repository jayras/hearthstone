using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Player
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public Dictionary<HeroClass, int> HeroLevels { get; set; }

		public Player()
		{
			HeroLevels = new Dictionary<HeroClass, int>();
		}

        public override string ToString()
        {
            return ID.ToString() + "\t" + Name;
        }
	}
}
