using System;

using Server.Mobiles;

using OSU;
using OSU.Structs;
using OSU.Civilizations;

namespace OSU
{
	class CharacterCreationSystem
	{
		public static OSUCharacter temp_char = new OSUCharacter();
		
		public static void WipeAllTraits(PlayerMobile player)
		{

		}

		public static void ResetTempChar()
		{
			temp_char = new OSUCharacter();
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
	}
}