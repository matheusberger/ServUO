using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Gumps
{
	public class OSUCharBGGump : Gump
	{
		private int current_background;

		public OSUCharBGGump(PlayerMobile m, int selected_background = 0)
			: base(0, 0)
		{
			
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			var backgrounds = Backgrounds.GetAllBackgrounds();
			current_background = selected_background;

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddLabel(113, 99, 1153, @"Characteristicas 4");
			AddHtml(109, 128, 608, 126, @"instruçoes", (bool)false, (bool)true);

			int btn_x = 110, btn_y = 330, y_offset = 15;
			int lbl_x = 140, lbl_y = btn_y, x_offset = 130;

			for (int i = 0; i < backgrounds.Length -2; i += 2)
			{
				for (int j = 0; j < 2; j++)
				{
					AddButton(btn_x + (j * x_offset), btn_y + (i * y_offset), (i + j) == current_background ? 9904 : 9903, (i + j) == current_background ? 9903 : 9904, (i + j), GumpButtonType.Reply, 0);
					AddLabel(lbl_x + (j * x_offset), lbl_y + (i * y_offset), 1153, backgrounds[i + j].Name);
				}
			}

			AddLabel(446, 333, 1153, backgrounds[current_background].Name + " COST: " + backgrounds[current_background].Cost);
			AddHtml(446, 390, 573, 346, "<p>" + backgrounds[current_background].FullDescription + "</p>", (bool)false, (bool)true);
			
			//go next
			AddButton(1055, 761, 4005, 4007, 999, GumpButtonType.Reply, 0);
		}
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			PlayerMobile m = sender.Mobile as PlayerMobile;

			if (m == null)
				return;

			switch (info.ButtonID)
			{
				case 999:
					break;
				default:
					m.SendGump(new OSUCharBGGump(m, info.ButtonID));
					break;
			}
		}
	}
}
