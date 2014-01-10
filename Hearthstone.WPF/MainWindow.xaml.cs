using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hearthstone.Controller;
using Hearthstone.TestData;

namespace Hearthstone.WPF
{
    // Left: 310 X 1030
    // Bottom: 1615 X 50


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IHearthstoneData _hearthstoneData;
        IPlayerData _player;
        List<Player> _allPlayers;
        public MainWindow()
        {
            _hearthstoneData = new HearthstoneData();
            InitializeComponent();
        }

        private void LoadPlayerStackLoaded(object sender, RoutedEventArgs e)
        {
            _allPlayers = _hearthstoneData.GetAllPlayers();

            PlayerListBox.ItemsSource = _allPlayers;
            PlayerListBox.DisplayMemberPath = "Name";
            PlayerListBox.SelectedItem = null;
        }

        private void PlayerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlayerListBox.SelectedItem == null)
            {
                return;
            }

            LoadPlayerStack.Visibility = Visibility.Collapsed;

            
            _player = new PlayerData(_hearthstoneData,PlayerListBox.SelectedItem as Player);
            PlayerNameLabel.Content = _player.Player.Name;
            
            PlayerRank angryChicken = new PlayerRank();
            angryChicken.ID = _hearthstoneData.NextPlayerRankID;
            angryChicken.Player = _player.Player;
            angryChicken.Season = _player.CurrentSeason;
            angryChicken.Rank = _hearthstoneData.Ranks.First(r => r.Name.ToLower() == "angry chicken");
            angryChicken.Rank.Stars = 1;
            angryChicken.DateAchieved = _player.CurrentSeason.Start;

            if (!(_player.PlayerRanks.Any(p => p.Season.ID == _player.CurrentSeason.ID)))
            {
                _hearthstoneData.AddPlayerRank(angryChicken);
            }

            PlayerRank currentRank = _player.PlayerRanks.First(d => d.ID == _player.PlayerRanks.Max(p => p.ID));
            if (_player.CurrentSeason.ID == currentRank.ID)
            {
                PlayerCurrenkRankLabel.Content = currentRank.Rank.Name + " (" + currentRank.Rank.Stars + " stars)";
            }
            else
            {
                PlayerCurrenkRankLabel.Content = "The Angry Chicken (1 Star)";
            }

            string classLevels = "Name\t\tLevel";
            foreach (HeroClass heroClass in _player.Player.HeroLevels.Keys.OrderBy(c => c.ClassName))
            {
                classLevels += "\n" + heroClass.ClassName + "\t\t" + _player.Player.HeroLevels[heroClass];
            }

            PlayerClassLevelsBlock.TextWrapping = TextWrapping.WrapWithOverflow;
            PlayerClassLevelsBlock.Text = classLevels;

            PlayerDataStack.Visibility = Visibility.Visible;
        }
    }
}
