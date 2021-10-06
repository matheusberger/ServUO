using System;

using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

using OSU;
using OSU.Civilizations;

namespace Server.Items
{
	class CharacterCreationStone: Item
	{
		[Constructable]
		public CharacterCreationStone() : base(0xED4)
		{
			Movable = false;
			Hue = 1201;
			Name = "Start Character Creation";
		}

		public CharacterCreationStone(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile m)
		{
			PlayerMobile from = m as PlayerMobile;
			CharacterCreationSystem.WipeAllTraits(from);

			from.SendGump(new CivilizationGump(from));
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
