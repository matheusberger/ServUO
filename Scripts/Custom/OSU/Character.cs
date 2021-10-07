using System;
using Server.Mobiles;
using OSU;
using OSU.Civilizations;

namespace OSU
{
	public class OSUCharacter
	{
		public enum Age
		{
			Young,
			Mature,
			Old
		}

		public PlayerMobile base_char;

		//leveling point system info
		//how many points the player currently has in each category
		private int current_combat_points;
		private int current_profession_points;
		private int current_magic_points;
		private int current_hability_points;

		//how many points the player has gathered in all his playthough 
		private int total_combat_points = 100;
		private int total_profession_points;
		private int total_magic_points;
		private int total_hability_points;

		//maximum points a player can gather through each playtrhough:  total_x_points < max_x_points
		private int max_combat_points = 500;
		private int max_profession_points;
		private int max_magic_points;
		private int max_hability_points;

		//char stats
		private int char_level;
		private int max_char_level;
		private int current_char_exp;

		public int str;
		public int dex;
		public int inteligence;

		public int hitpoints;
		public int stamina;
		public int mana;

		//char details
		private int char_weight;
		private int char_height;

		public int char_avatar;
		public string char_physical_des;

		public int hairstyle;
		public int facial_hair;
		public int hair_color; 
		public int skin_tone;
		
		//rp system
		public Civilization civilization;
		private int disguise;

		public Age age;

		public Languages[] known_languages;
		private OSUSkill[] skill_list;

		//backgrounds
		private int[] faults;
		private int[] qualities;
	}
}