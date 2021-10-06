using System;

using Server.Mobiles;

using OSU;
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
	}
}