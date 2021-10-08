using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using System.Collections.Generic;

namespace Server.Gumps
{
	public class OSUCharBGGump : Gump
	{
		private List<int> current_backgrounds;


		public OSUCharBGGump(PlayerMobile m, int last_background = 0, List<int> selected_backgrounds = null)
			: base(0, 0)
		{
			
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;


			var backgrounds = Backgrounds.GetAllBackgrounds();
			current_backgrounds = selected_backgrounds ?? new List<int>();

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddLabel(113, 99, 1153, @"Characteristicas 4");
			AddHtml(109, 128, 608, 126, @"instruçoes!!!! Seu saldo atual é: " + m.osu_char_info.background_bonus_points, (bool)false, (bool)true);

			int btn_x = 110, btn_y = 330, y_offset = 15;
			int lbl_x = 140, lbl_y = btn_y, x_offset = 130;

			for (int i = 0; i < backgrounds.Length -2; i += 2)
			{
				for (int j = 0; j < 2; j++)
				{
					AddButton(btn_x + (j * x_offset), btn_y + (i * y_offset), current_backgrounds.Contains(i + j) ? 9904 : 9903, current_backgrounds.Contains(i + j) ? 9903 : 9904, (i + j), GumpButtonType.Reply, 0);
					AddLabel(lbl_x + (j * x_offset), lbl_y + (i * y_offset), 1153, backgrounds[i + j].Name);
				}
			}

			AddLabel(446, 333, 1153, backgrounds[last_background].Name);
			AddHtml(446, 390, 573, 346, "<p>" + backgrounds[last_background].FullDescription + "</p>", (bool)false, (bool)true);
			
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
					m.SendGump(new OSUSkillsGump(m));
					break;
				default:
					var all_backgrounds = Backgrounds.GetAllBackgrounds();
					var selected = all_backgrounds[info.ButtonID];

					if (selected.HasThisBackground(m))
					{
						current_backgrounds.Remove(info.ButtonID);
					}
					else
					{
						current_backgrounds.Add(info.ButtonID);
					}
					selected.AttemptPurchase(m);
					m.SendGump(new OSUCharBGGump(m, info.ButtonID, current_backgrounds));
					break;
			}
		}
	}
}
