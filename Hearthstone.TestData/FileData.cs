using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hearthstone.Controller;

namespace Hearthstone.TestData
{
	class FileData
	{
        private static string _dataPath = @"C:\VSTS\2013\Hearthstone\TestData";
        private static string _playerPath = Path.Combine(_dataPath, "Player.txt");
        private static string _deckFilePath = Path.Combine(_dataPath, "Deck.txt");
        private static string _matchPath = Path.Combine(_dataPath, "Matches.txt");
        private static string _rankPath= Path.Combine(_dataPath, "Ranks.txt");
        private static string _seasonPath = Path.Combine(_dataPath, "Seasons.txt");
        private static string _playerRankPath = Path.Combine(_dataPath, "PlayerRanks.txt");


		internal List<HeroClass> HeroClasses;
		internal List<Player> Players;
		internal List<Season> Seasons;
		internal List<Rank> AllRanks;
		internal List<Deck> Decks;
		internal List<Match> Matches;
        internal List<PlayerRank> AllPlayerRanks;

		public FileData()
		{
			HeroClasses = new List<HeroClass>();
			Players = new List<Player>();
			Seasons = new List<Season>();
			AllRanks = new List<Rank>();
			Decks = new List<Deck>();
			Matches = new List<Match>();
            AllPlayerRanks = new List<PlayerRank>();

			LoadHeroClasses();
			LoadPlayers();
			LoadRanks();
			LoadSeasons();
            LoadPlayerRanks();
			LoadDecks();
			LoadMatches();
		    LoadPlayerClassLevels();
		}

		private void LoadMatches()
		{
			if (!File.Exists(_matchPath))
			{
				throw new ArgumentException("Cannot load Match File.");
			}

			using (StreamReader matchFile = new StreamReader(_matchPath))
			{
				string matchLine;
				while ((matchLine = matchFile.ReadLine()) != null)
				{
					if (matchLine.StartsWith("#"))
					{
						continue;
					}

					string[] matchData = matchLine.Split('\t');
					if (matchData.Length >= 16)
					{
						Match thisMatch = new Match();
						thisMatch.ID = int.Parse(matchData[0]);
						thisMatch.Player = Players.First(p => p.ID == int.Parse(matchData[1]));
						thisMatch.HeroClass = HeroClasses.First(c => c.ID == int.Parse(matchData[2]));
						thisMatch.Deck = Decks.First(d => d.ID == int.Parse(matchData[3]));
						thisMatch.Type = (MatchType)int.Parse(matchData[4]);
						thisMatch.OpponentClass = HeroClasses.First(c => c.ID == int.Parse(matchData[5]));
						thisMatch.OpponentName = matchData[6];
						thisMatch.OpponentRank = AllRanks.First(r => r.ID == int.Parse(matchData[7]));
						thisMatch.StartingRank = AllRanks.First(r => r.ID == int.Parse(matchData[8]));
						thisMatch.StartingRank.Stars = int.Parse(matchData[9]);
						thisMatch.EndingRank = AllRanks.First(r => r.ID == int.Parse(matchData[10]));
						thisMatch.EndingRank.Stars = int.Parse(matchData[11]);
						thisMatch.HaveCoin = ParseBool(matchData[12]);
						thisMatch.HaveWon = ParseBool(matchData[13]);
						thisMatch.Rounds = int.Parse(matchData[14]);
						thisMatch.DidConcede = ParseBool(matchData[15]);
						if (matchData.Length == 17 && !String.IsNullOrWhiteSpace(matchData[16]))
						{
							thisMatch.Notes = matchData[16];
						}
						else
						{
							thisMatch.Notes = "";
						}

						if (!Matches.Any(m => m.ID == thisMatch.ID))
						{
							Matches.Add(thisMatch);
						}
					}
				}
			}
		}

		private void LoadDecks()
		{
			
			if (!File.Exists(_deckFilePath))
			{
				throw new ArgumentException("Cannot load Deck File.");
			}

			using (StreamReader deckFile = new StreamReader(_deckFilePath))
			{
				string deckLine;
				while ((deckLine = deckFile.ReadLine()) != null)
				{
					if (deckLine.StartsWith("#"))
					{
						continue;
					}

					string[] deckData = deckLine.Split('\t');
					if (deckData.Length >= 6)
					{
						Deck thisDeck = new Deck();
						thisDeck.ID = int.Parse(deckData[0]);
						thisDeck.Player = Players.First(p => p.ID == int.Parse(deckData[1]));
						thisDeck.Class = HeroClasses.First(c => c.ID == int.Parse(deckData[2]));
						thisDeck.Name = deckData[3];
						thisDeck.DeckURL = deckData[4];
						thisDeck.Version = deckData[5];

						if (!Decks.Contains(thisDeck))
						{
							Decks.Add(thisDeck);
						}
					}
				}
			}
		}

		private void LoadRanks()
		{
			if (!File.Exists(_rankPath))
			{
				throw new ArgumentException("Missing Season and/or Rank file(s).");
			}

			using (StreamReader rankStream = new StreamReader(_rankPath))
			{
				string rankLine;
				while ((rankLine = rankStream.ReadLine()) != null)
				{
					if (rankLine.StartsWith("#"))
					{
						continue;
					}

					string[] rankData = rankLine.Split('\t');
					if (rankData.Length >= 4)
					{
						Rank thisRank = new Rank();
						thisRank.ID = int.Parse(rankData[0]);
						thisRank.Number = int.Parse(rankData[1]);
						thisRank.Name = rankData[2];
						thisRank.ImageLink = rankData[3].ToLower() == "null" ? "" : rankData[3];
						if (rankData.Length >= 5 && rankData[4].ToLower() != "null")
						{
							thisRank.ExpirationDate = DateTime.Parse(rankData[4]);
						}
						else
						{
							thisRank.ExpirationDate = DateTime.MaxValue;
						}
						if (!AllRanks.Any(r => r.ID == thisRank.ID))
						{
							AllRanks.Add(thisRank);
						}
					}
				}
			}
		}

		private void LoadSeasons()
		{
			if (!File.Exists(_seasonPath))
			{
				throw new ArgumentException("Missing Season and/or Rank file(s).");
			}

			if (AllRanks.Count == 0)
			{
				LoadRanks();
			}

			using (StreamReader seasonFile = new StreamReader(_seasonPath))
			{
				string seasonLine;
				while ((seasonLine = seasonFile.ReadLine()) != null)
				{
					if (seasonLine.StartsWith("#"))
					{
						continue;
					}
					string[] seasonData = seasonLine.Split('\t');

					if (seasonData.Length >= 5 )
					{
						Season thisSeason = new Season();
						thisSeason.ID = int.Parse(seasonData[0]);
						thisSeason.Number = int.Parse(seasonData[1]);
						thisSeason.Start = DateTime.Parse(seasonData[2]);
						thisSeason.End = DateTime.Parse(seasonData[3]);
						thisSeason.Description = seasonData[4];
						thisSeason.Ranks = AllRanks.Where(r => r.ExpirationDate > thisSeason.Start).ToList();
                        Seasons.Add(thisSeason);
					}

				}
			}

		}

		private void LoadHeroClasses()
		{
			string heroClassPath = Path.Combine(_dataPath, "HeroClasses.txt");
			if (!File.Exists(heroClassPath))
			{
				throw new ArgumentException("HeroClass file does not exist.");
			}

			using (StreamReader classFile = new StreamReader(heroClassPath))
			{
				string classEntry;
				while ((classEntry = classFile.ReadLine()) != null)
				{
					if (classEntry.StartsWith("#"))
					{
						continue;
					}

					string[] classData = classEntry.Split('\t');
					if (classData.Length >= 4)
					{
						HeroClass thisClass = new HeroClass();
						thisClass.ID = int.Parse(classData[0]);
						thisClass.ClassName = classData[1];
						thisClass.HeroName = classData[2];
						thisClass.IsNPC = ParseBool(classData[3]);

						if (!HeroClasses.Any(h => h.ID == thisClass.ID))
						{
							HeroClasses.Add(thisClass);
						}
					}
				}
			}

		}

        private void LoadPlayerRanks()
        {
            if (!File.Exists(_playerRankPath))
            {
                throw new ArgumentException("Player Rank File does not exist.");
            }

            using (StreamReader playerRankFile = new StreamReader(_playerRankPath))
            {
                string playerRankLine;
                while ((playerRankLine = playerRankFile.ReadLine()) != null)
                {
                    if (playerRankLine.StartsWith("#") || String.IsNullOrWhiteSpace(playerRankLine))
                    {
                        continue;
                    }

                    string[] playerRankData = playerRankLine.Split('\t');
                    if (playerRankData.Length >= 6)
                    {
                        PlayerRank playerRank = new PlayerRank();
                        playerRank.ID = int.Parse(playerRankData[0]);
                        playerRank.Player = Players.First(p => p.ID == int.Parse(playerRankData[1]));
                        playerRank.Season = Seasons.First(s => s.ID == int.Parse(playerRankData[2]));
                        playerRank.Rank = AllRanks.First(r => r.ID == int.Parse(playerRankData[3]));
                        playerRank.Rank.Stars = int.Parse(playerRankData[4]);
                        playerRank.DateAchieved = DateTime.Parse(playerRankData[5]);

                        AllPlayerRanks.Add(playerRank);
                    }
                }
            }
        }

        internal bool RemovePlayerRank(PlayerRank playerRank)
        {
            if (AllPlayerRanks.Contains(playerRank))
            {
                AllPlayerRanks.Remove(playerRank);
                return RemovePlayerRank(playerRank);
            }

            return true;
        }

        internal bool AddPlayerRank(PlayerRank playerRank)
        {
            if (!RemovePlayerRank(playerRank))
            {
                return false;
            }

            AllPlayerRanks.Add(playerRank);

            using (StreamWriter playerRankFile = new StreamWriter(_playerRankPath,false))
            {
                playerRankFile.WriteLine("#Player Ranks");
                playerRankFile.WriteLine("#ID\tPlayerID\tSeasonID\tRankID\tStars\tDateAchieved");

                foreach (PlayerRank thisPlayerRank in AllPlayerRanks.OrderBy(p => p.ID))
                {
                    playerRankFile.WriteLine(thisPlayerRank.ToString());
                }

                playerRankFile.Flush();
            }

            return true;
        }

        internal bool UpdatePlayerRank(PlayerRank playerRank)
        {
            return AddPlayerRank(playerRank);
        }

		private static bool ParseBool(string p)
		{
			string lowerP = p.Trim().ToLower().Replace(@"""","").Replace("'","");
			return (lowerP == "true" || lowerP == "1" || lowerP == "yes");
		}

		private void LoadPlayers()
		{
			if (!File.Exists(_playerPath))
			{
				throw new ArgumentException("Player File missing.");
			}

			using (StreamReader inputPlayer = new StreamReader(_playerPath))
			{
				string inputLine;
				while ((inputLine = inputPlayer.ReadLine()) != null)
				{
					if (inputLine.StartsWith("#"))
					{
						continue;
					}
					string[] incomingPlayer = inputLine.Split('\t');
					if (incomingPlayer.Length >= 2)
					{
						Player thisPlayer = new Player();
						thisPlayer.ID = int.Parse(incomingPlayer[0]);
						thisPlayer.Name = incomingPlayer[1];
						Players.Add(thisPlayer);
					}
				}
			}
		}

		private void LoadPlayerClassLevels()
		{
			string playerClassLevelPath = Path.Combine(_dataPath, "PlayerHeroLevel.txt");
			if (!File.Exists(playerClassLevelPath))
			{
				throw new ArgumentException("Player Class Level File Misssing.");
			}

			if (Players == null || Players.Count == 0)
			{
				throw new ArgumentException("Players not loaded.");
			}
			using (StreamReader inputLevel = new StreamReader(playerClassLevelPath))
			{
				string inputLine;
				while ((inputLine = inputLevel.ReadLine()) != null)
				{
					if (inputLine.StartsWith("#"))
					{
						continue;
					}

					string[] classLevels = inputLine.Split('\t');
					if (classLevels.Length >= 3)
					{
						int playerID = 0;
						int classID = 0;
						int level = 0;
						if (Int32.TryParse(classLevels[0], out playerID) && Int32.TryParse(classLevels[1], out classID) && Int32.TryParse(classLevels[2], out level))
						{
							if (Players.Any(p => p.ID == playerID))
							{
								Player thisPlayer = Players.First(p => p.ID == playerID);
								HeroClass hero = HeroClasses.First(h => h.ID == classID);
								if (thisPlayer.HeroLevels.ContainsKey(hero))
								{
									thisPlayer.HeroLevels[hero] = level;
								}
								else
								{
									thisPlayer.HeroLevels.Add(hero, level);
								}
							}
						}
					}
				}
			}
		}

        internal bool UpdatePlayer(Player player)
        {
            return AddPlayer(player);
        }

        internal bool AddPlayer(Player player)
        {
            if (Players.Any(p => p.ID == player.ID))
            {
                Players.Remove(Players.First(p => p.ID == player.ID));
            }

            Players.Add(player);

            using (StreamWriter playerFile = new StreamWriter(_playerPath,false))
            {
                playerFile.WriteLine("#Player");
                playerFile.WriteLine("#ID\tName");
                foreach (Player thisPlayer in Players.OrderBy(p => p.ID))
                {
                    playerFile.WriteLine(thisPlayer.ToString());
                }

                playerFile.Flush();
            }

            return true;
        }

        internal bool DeleteDeck(Deck deck)
        {
            if (Decks.Any(d => d.ID == deck.ID))
            {
                Decks.Remove(Decks.First(d => d.ID == deck.ID));
                return DeleteDeck(deck);
            }

            return true;
        }


        internal bool UpdateDeck(Deck deck)
        {
            return AddDeck(deck);
        }

        internal bool AddDeck(Deck deck)
        {
            if (Decks.Any(d => d.ID == deck.ID))
            {
                Decks.Remove(Decks.First(d => d.ID == deck.ID));
            }

            Decks.Add(deck);

            using (StreamWriter deckFile = new StreamWriter(_deckFilePath,false))
            {
                deckFile.WriteLine("#Deck");
                deckFile.WriteLine("#ID\tPlayerID\tClassID\tName\tURL\tVersion");
                foreach (Deck thisDeck in Decks.OrderBy(d => d.ID))
                {
                    deckFile.WriteLine(thisDeck.ToString());
                }

                deckFile.Flush();
            }

            return true;
        }

        internal bool AddMatch(Match match)
        {
            if (Matches.Any(m => m.ID == match.ID))
            {
                return false;
            }

            Matches.Add(match);

            using (StreamWriter matchFile = new StreamWriter(_matchPath, false))
            {
                string matchFormat = "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}";
                StringBuilder thisString = new StringBuilder();
                thisString.AppendFormat(matchFormat, "#ID", "PlayerID", "HeroClassID", "DeckID", "MatchTypeID", "OpponentClassID", "OpponentName", "OpponentRankID", "StartingRankID", "StartingRankStars", "EndingRankID", "EndingRankStars", "HaveCoin", "HaveWon", "Rounds", "DidConcede", "Notes");
                matchFile.WriteLine(thisString.ToString());

                foreach (Match thisMatch in Matches.OrderBy(m => m.ID))
                {
                    matchFile.WriteLine(thisMatch.ToString());
                }

                matchFile.Flush();
            }

            return true;
        }

        internal bool RemoveMatch(Match match)
        {
            if (Matches.Any(m => m.ID == match.ID))
            {
                Matches.Remove(Matches.First(m => m.ID == match.ID));
                return RemoveMatch(match);
            }

            return true;
        }

        internal bool UpdateMatch(Match match)
        {
            if (!RemoveMatch(match))
            {
                return false;
            }

            return AddMatch(match);
        }

        internal bool RemoveSeason(Season season)
        {
            if (Seasons.Any(s => s.ID == season.ID))
            {
                Seasons.Remove(Seasons.First(s => s.ID == season.ID));
                return RemoveSeason(season);
            }
            return true;
        }

        internal bool UpdateSeason(Season season)
        {
            if (!RemoveSeason(season))
            {
                return false;
            }

            Seasons.Add(season);
            
            using (StreamWriter seasonFile = new StreamWriter(_seasonPath,false))
            {
                seasonFile.WriteLine("#Seasons");
                seasonFile.WriteLine("#ID\tNumber\tStart\tEnd\tDescription");

                foreach (Season thisSeason in Seasons.OrderBy(s=> s.ID))
                {
                    seasonFile.WriteLine(thisSeason.ToString());
                }

                seasonFile.Flush();
            }
            return true;
        }

        internal bool AddSeason(Season season)
        {
            return UpdateSeason(season);
        }

        internal bool RemoveRank(Rank rank)
        {
            if (AllRanks.Any(r => r.ID == rank.ID))
            {
                AllRanks.Remove(AllRanks.First(r => r.ID == rank.ID));
                return RemoveRank(rank);
            }

            return true;
        }

        internal bool AddRank(Rank rank)
        {
            if (!RemoveRank(rank))
            {
                return false;
            }

            using (StreamWriter rankFile = new StreamWriter(_rankPath,false))
            {
                rankFile.WriteLine("#Ranks");
                rankFile.WriteLine("#ID\tNumber\tName\tImageLink\tExpirationDate");

                foreach (Rank thisRank in AllRanks.OrderBy(r => r.ID))
                {
                    rankFile.WriteLine(thisRank.ToString());
                }

                rankFile.Flush();
            }

            return true;
        }

        internal bool UpdateRank(Rank rank)
        {
            return AddRank(rank);
        }
    }
}
