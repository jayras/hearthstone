using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Season
	{
		public int ID { get; set; }
		public int Number { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public string Description { get; set; }
		public List<Rank> Ranks { get; set; }

		public Season()
		{
			Ranks = new List<Rank>();
		}

        public override string ToString()
        {
            return ID + "\t" + Start.ToString("yyyy-MM-dd") + "\t" + End.ToString("yyyy-MM-dd") + "\t" + Description;
        }
	}
}
