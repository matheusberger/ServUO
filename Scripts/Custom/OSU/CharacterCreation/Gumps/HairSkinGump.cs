using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Gumps
{
	public class HairSkinGump : Gump
	{

		public HairSkinGump(PlayerMobile m)
			: base(0, 0)
		{
			m.CloseGump(typeof(CivilizationGump));
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			this.AddPage(0);
			this.AddBackground(54, 31, 400, 383, 9270);
			this.AddBackground(71, 192, 364, 202, 3500);
			this.AddImage(4, 10, 10440);
			this.AddImage(423, 10, 10441);
			this.AddImage(183, 50, 29);
			//this.AddImage(215, 80, 9000);
			this.AddLabel(194, 48, 2010, @"Stat Points Left: ");
			this.AddLabel(116, 82, 1149, @"Strength");
			this.AddLabel(116, 117, 1149, @"Dexterity");
			this.AddLabel(116, 152, 1149, @"Intelligence");
			this.AddLabel(324, 82, 1149, @"Hit Points");
			this.AddLabel(337, 117, 1149, @"Stamina");
			this.AddLabel(356, 152, 1149, @"Mana");

			this.AddButton(404, 46, 1150, 1152, 0, GumpButtonType.Reply, 0);

			this.AddButton(96, 85, 5600, 5604, 1, GumpButtonType.Reply, 0);
			this.AddButton(96, 120, 5600, 5604, 2, GumpButtonType.Reply, 0);
			this.AddButton(96, 155, 5600, 5604, 3, GumpButtonType.Reply, 0);
			this.AddButton(415, 85, 5600, 5604, 4, GumpButtonType.Reply, 0);
			this.AddButton(415, 120, 5600, 5604, 5, GumpButtonType.Reply, 0);
			this.AddButton(415, 155, 5600, 5604, 6, GumpButtonType.Reply, 0);
			this.AddButton(75, 85, 9764, 9765, 7, GumpButtonType.Reply, 0);
			this.AddButton(75, 120, 9764, 9765, 8, GumpButtonType.Reply, 0);
			this.AddButton(75, 155, 9764, 9765, 9, GumpButtonType.Reply, 0);
			this.AddButton(395, 85, 9764, 9765, 10, GumpButtonType.Reply, 0);
			this.AddButton(395, 120, 9764, 9765, 11, GumpButtonType.Reply, 0);
			this.AddButton(395, 155, 9764, 9765, 12, GumpButtonType.Reply, 0);
			this.AddHtml(99, 219, 307, 147, @"You may now spend your initial Stat Bonus Points as you wish. While in the creation chamber, " +
						 " you can reopen this gump by using the .StatPoints command.", (bool)true, (bool)true);

		}
		public static int GetValue(int feat)
		{
			if (feat == 1)
				return 1;

			if (feat == 2)
				return 3;

			if (feat == 3)
				return 6;

			return 0;
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			PlayerMobile m = sender.Mobile as PlayerMobile;

			if (m == null)
				return;

			switch (info.ButtonID)
			{

				case 0:
					{
						break;
					}

			}

			if (m.HasGump(typeof(AvatarDescriptionGump)))
				m.SendGump(new AvatarDescriptionGump(m));

			if (info.ButtonID != 0)
			{
				m.SendGump(new CivilizationGump(m));
			}
		}
	}
}
