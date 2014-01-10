using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hearthstone.Controller;
using Hearthstone.TestData;

namespace Hearthstone.Tracker
{
	public partial class Test_Form : Form
	{
		private IHearthstoneData _hearthstoneData;
		private IPlayerData _playerData;

		public Test_Form()
		{
			_hearthstoneData = new HearthstoneData();
            Player player = _hearthstoneData.GetPlayer(1);
			_playerData = new PlayerData(_hearthstoneData, player);
			InitializeComponent();
		}
	}
}
