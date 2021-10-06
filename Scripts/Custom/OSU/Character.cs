using System;
using Server.Mobiles;
using OSU;
using OSU.Civilizations;

namespace OSU
{
	public class OSUCharacter
	{
		private PlayerMobile base_char;

		//leveling point system info

		//how many points the player currently has in each category
		private int current_combat_points = 50;
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

		private int str;
		private int dex;
		private int inteligence;

		private int hitpoints;
		private int stamina;
		private int mana;

		//char details
		private int char_weight;
		private int char_height;

		private int char_avatar;
		private string char_physical_des;

		private int hairstyle;
		private int skin_tone;
		private int clothes;

		//rp system
		public Civilization civilization;
		private int disguise;

		private Languages[] known_languages;
		private OSUSkill[] skill_list;

		//backgrounds
		private int[] faults;
		private int[] qualities;
	}
}