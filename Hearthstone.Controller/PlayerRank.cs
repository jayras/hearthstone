using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hearthstone.Controller
{
    public class PlayerRank : IComparable , IEquatable<PlayerRank>
    {
        public int ID { get; set; }
        public Season Season { get; set; }
        public Player Player { get; set; }
        public Rank Rank { get; set; }
        public DateTime DateAchieved { get; set; }

        public override string ToString()
        {
            return Player.ID + "\t" + Season.ID + "\t" + Rank.ID + "\t" + Rank.Stars + "\t" + DateAchieved.ToString("yyyy-MM-dd");
        }

        public int CompareTo(object obj)
        {
            if (obj is PlayerRank)
            {
                PlayerRank objComp = obj as PlayerRank;

                string thisString = this.ToString();

                return thisString.CompareTo(objComp.ToString());
            }
            else
            {
                return this.ToString().CompareTo(obj.ToString());
            }
        }
        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0; 
        }

        public bool Equals(PlayerRank other)
        {
            return ((this.Player.ID == other.Player.ID) && (this.Season.ID == other.Season.ID) && (this.Rank.ID == other.Rank.ID) && (this.Rank.Stars == other.Rank.Stars) && (this.DateAchieved == other.DateAchieved));
        }
    }
}
