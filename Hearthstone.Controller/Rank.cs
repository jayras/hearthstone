using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone.Controller
{
	public class Rank
	{
		public int ID { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public int Stars { get; set; }
		public int? LegendRank { get; set; }
		public string ImageLink { get; set; }
		public DateTime ExpirationDate { get; set; }

    //    #Ranks
    //    #ID	Number	Name	ImageLink	ExpirationDate

        public override string ToString()
        {
            return ID + "\t" + Number + "\t" + Name + "\t" + ImageLink + "\t" + ExpirationDate.ToString("yyyy-MM-dd");
        }
	}
}
