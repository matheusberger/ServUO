using System;

using Server.Mobiles;
using Server.Engines.XmlSpawner2;

using OSU;
using OSU.Structs;
using OSU.Civilizations;

namespace OSU
{
	class CharacterCreationSystem
	{
		public static OSUCharacter temp_char;
		
		public static void BeginCreation(PlayerMobile m)
		{
			temp_char = new OSUCharacter();
			temp_char.InitOSUChar(m);
			//XmlAttach.Initialize();
			//XmlAttach.Configure();
		}

		public static void WipeAllTraits(PlayerMobile player)
		{

		}

		public static void ResetTempChar()
		{
			PlayerMobile m = temp_char.base_char;
			temp_char = new OSUCharacter();
			temp_char.InitOSUChar(m);
		}

		public static void SetCharCivilization(Civilization civ)
		{
			temp_char.civilization = civ;
			temp_char.known_languages = civ.known_languages;
		}

		public static void SetCharStats(CharStats stats, AvatarDesc desc)
		{
			temp_char.str = temp_char.civilization.base_str + stats.bonus_str;
			temp_char.str = temp_char.civilization.base_dex + stats.bonus_dex;
			temp_char.str = temp_char.civilization.base_int + stats.bonus_int;
			
			temp_char.str = temp_char.civilization.base_hitpoints + stats.bonus_hitpoints;
			temp_char.str = temp_char.civilization.base_stamina + stats.bonus_stamina;
			temp_char.str = temp_char.civilization.base_mana + stats.bonus_mana;

		}

		public static void PreviewHair(int id)
		{
			temp_char.base_char.HairItemID = 0;
			temp_char.base_char.HairItemID = id;
		}

		public static void PreviewFacialHair(int id)
		{
			temp_char.base_char.FacialHairItemID = 0;
			temp_char.base_char.FacialHairItemID = id;
		}

		public static void PreviewHairColor(int id)
		{
			temp_char.base_char.HairHue = id;
		}

		public static void PreviewSkinTone(int id)
		{
			temp_char.base_char.Hue = id;
		}

		public static void SetAppearence(int hair_id, int beard_id, int hair_color_id, int skin_tone)
		{
			temp_char.hairstyle = hair_id;
			temp_char.facial_hair = beard_id;
			temp_char.hair_color = hair_color_id;
			temp_char.skin_tone = skin_tone;
		}
	}
}