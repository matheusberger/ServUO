﻿using System;
using Server.Mobiles;
using Server.Misc;

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

		public void InitOSUChar(PlayerMobile m)
		{
			this.base_char = m;
			m.osu_char_info = this;

			current_combat_points = 0;
			current_profession_points = 0;
			current_magic_points = 0;
			current_hability_points = 0;

			total_combat_points = 0;
			total_profession_points = 0;
			total_magic_points = 0;
			total_hability_points = 0;

			max_combat_points = 500;
			max_profession_points = 0;
			max_magic_points = 0;
			max_hability_points = 0;

			char_level = 1;
			max_char_level = 25;
			current_char_exp = 0;

			str = 1;
			dex = 1;
			inteligence = 1;

			char_weight = 50;
			char_height = 50;

			char_avatar = 0;
			char_physical_des = "";

			known_languages = new Languages[0];
			skill_list = new OSUSkill[0];

			Backgrounds = new Backgrounds();
		}

		public PlayerMobile base_char;

		//leveling point system info
		//how many points the player currently has in each category
		public int current_combat_points;
		public int current_profession_points;
		public int current_magic_points;
		public int current_hability_points;

		//how many points the player has gathered in all his playthough 
		public int total_combat_points = 100;
		public int total_profession_points;
		public int total_magic_points;
		public int total_hability_points;

		//maximum points a player can gather through each playtrhough:  total_x_points < max_x_points
		public int max_combat_points = 500;
		public int max_profession_points;
		public int max_magic_points;
		public int max_hability_points;

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
		public Backgrounds Backgrounds;

		public int background_bonus_points;
	}
}