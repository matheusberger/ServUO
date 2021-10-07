using System;
using OSU;

namespace OSU
{
	namespace Civilizations
	{
		public enum Languages
		{
			Lang1,
			Lang2,
			Lang3
		}

		public struct CivState
		{
			public CivState(bool destroyed, bool atWar, bool atAliance)
			{
				this.destroyed = destroyed;
				this.atWar = atWar;
				this.atAliance = atAliance;
			}

			public readonly bool destroyed;
			public readonly bool atWar;
			public readonly bool atAliance;
		}

		public class Civilization
		{
			public Civilization(string name, string title, int max_height, int min_height, int max_weight, int min_weight,
								int base_str, int base_dex, int base_int, int base_hitpoints, int base_stamina, int base_mana,
								string description, int[] impossible_skills, Languages[] languages, CivState state)
			{
				this.name = name;
				this.title = title;

				this.max_height = max_height;
				this.min_height = min_height;

				this.max_weight = max_weight;
				this.min_weight = min_weight;

				this.base_str = base_str;
				this.base_dex = base_dex;
				this.base_int = base_int;

				this.base_hitpoints = base_hitpoints;
				this.base_stamina = base_stamina;
				this.base_mana = base_mana;

				this.description = description;

				this.impossible_skills = impossible_skills;
				this.known_languages = languages;

				this.current_state = state;
			}

			public readonly string name;
			public readonly string title;

			public readonly int max_height;
			public readonly int min_height;

			public readonly int max_weight;
			public readonly int min_weight;

			public readonly int base_str;
			public readonly int base_dex;
			public readonly int base_int;

			public readonly int base_hitpoints;
			public readonly int base_stamina;
			public readonly int base_mana;

			public readonly string description;

			public readonly int[] impossible_skills = { 0, 1, 2 };
			public readonly Languages[] known_languages = {Languages.Lang1, Languages.Lang2 };

			public readonly CivState current_state;
		}


		public struct CivilizationImpossibleSkills
		{
			public static int[] AludiaImpossible = { 0, 1 };
			public static int[] DurahImpossible = { 1, 3 };
		}

		public struct CivilizationLanguages
		{
			public static Languages[] AludiaLangs = { Languages.Lang1, Languages.Lang2 };
			public static Languages[] DurahLangs = { Languages.Lang1, Languages.Lang3 };
		}

		public struct CivilizationDescriptions
		{
			public  static string AludiaDesc = @"<p>Os Aludes são humanos provindos do continente de Linarin "
												+ "Centenas de anos atrás, uma pequena vila apareceu as beiras do rio Vidore, "
												+ "que na velha língua significa “sem vida”. Aquela era uma área considerada inútil "
												+ "aos olhos dos Théranos pois animais não habitavam sua floresta, nem peixes, "
												+ "o seu rio. Porém foi lá que uma dúzia de pessoas resolveu se estabelecer. A "
												+ "verdade é que depois de terem sido avistados lá, relatos foram documentados sobre "
												+ "eventos estranhos de natureza desconhecida. Demorou muito tempo para os Théranos "
												+ "entenderem a magia, apesar de ser algo que os moradores daquela pequena vila "
												+ "já mestravam.  A vila se chamou de Aludia. Com os anos ela foi crescendo, prosperando, "
												+ "e virou o centro de Linarin.<br> <br>" 
												+ "Linguagens Faladas - Comum, Aulure (Velha lingua) <br>" 
												+ "Skills desativadas - Parrying - Hiding - Animal Taming <br>" 
												+ "Aludia nao tem aliancas com nenhum outro povo <br>"
												+ "Aludia nao esta em guerra com nenhum outro povo </p>";

			public static string DurahDesc = @"<p>Durah Moria was founded by Durin at the end of the Ages"
												+ "of the Stars. During his reign, the precious metal "
												+ "mithril was discovered in the mines, and some of the"
												+ "major structures of Moria were built: Durin's Bridge,"
												+ "the Second Hall, the Endless Stair and Durin's "
												+ "Tower. Durin died before the end of the First Age. "
												+ "He was buried in the royal tombs of Khazad-dûm. </p>";
		}

		public struct Civilizations
		{
			//Parameter order is:
			// name, title, max height, min height, max weight, min weight
			// base str, base dex, base int, base hitpoints, base stamina, base mana,
			// description
			// impossible skills to learn
			// languages this civilization speaks
			// civilization state
			public static Civilization Aludia = new Civilization("Aludia", "Os Aludes", 190, 160, 100, 60,
																10, 20, 30, 50, 40, 60,
																CivilizationDescriptions.AludiaDesc,
																CivilizationImpossibleSkills.AludiaImpossible,
																CivilizationLanguages.AludiaLangs,
																new CivState(false, false, false));


			public static Civilization Durah = new Civilization("Durah", "Os Durah", 190, 180, 110, 80,
																10, 19, 12, 150, 100, 100,
																CivilizationDescriptions.DurahDesc,
																CivilizationImpossibleSkills.DurahImpossible,
																CivilizationLanguages.DurahLangs,
																new CivState(false, false, false));



			//public static Civilization Civ3;
			//public static Civilization Civ4;
			//public static Civilization Civ5;
			//public static Civilization UnkownCiv;
			//ADD MORE CIVILIZATIONS HERE
		}
	}
}