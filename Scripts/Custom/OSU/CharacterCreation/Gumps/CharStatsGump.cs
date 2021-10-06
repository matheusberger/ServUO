using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Gumps
{
	public class CharStatsGump : Gump
	{
		private int remaining_points = 50;

		private int bonus_str = 0;
		private int bonus_dex = 0;
		private int bonus_int = 0;

		private int bonus_hitpoints = 0;
		private int bonus_stamina = 0;
		private int bonus_mana = 0;

		public CharStatsGump(PlayerMobile m)
			: base(0, 0)
		{
			m.CloseGump(typeof(CharStatsGump));
			
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddHtml(109, 128, 608, 126, @"<p>Distribui teus pontos, escolhe um avatar, e escreve a tua descrição</p>", (bool)false, (bool)true);
			AddLabel(113, 99, 1153, @"Caracteristicas Parte 2");
			AddButton(319, 404, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(319, 444, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(319, 484, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(319, 524, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(319, 564, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(319, 604, 9903, 9904, 0, GumpButtonType.Reply, 0);
			
			AddLabel(125, 405, 1153, @"Forca");
			AddLabel(250, 405, 1153, @"10");

			AddLabel(125, 445, 1153, @"Destreza");
			AddLabel(250, 445, 1153, @"10");

			AddLabel(125, 485, 1153, @"Inteligencia");
			AddLabel(250, 485, 1153, @"10");

			AddLabel(125, 525, 1153, @"Hit Points");
			AddLabel(250, 525, 1153, @"10");

			AddLabel(125, 565, 1153, @"Estamina");
			AddLabel(250, 565, 1153, @"10");

			AddLabel(125, 605, 1153, @"Mana");
			AddLabel(250, 605, 1153, @"10");

			AddLabel(120, 341, 1153, @"Você ainda tem " + remaining_points + " pontos para distribuir");
			AddButton(280, 404, 9909, 9910, 0, GumpButtonType.Reply, 0);
			AddButton(280, 444, 9909, 9910, 0, GumpButtonType.Reply, 0);
			AddButton(280, 484, 9909, 9910, 0, GumpButtonType.Reply, 0);
			AddButton(280, 524, 9909, 9910, 0, GumpButtonType.Reply, 0);
			AddButton(280, 564, 9909, 9910, 0, GumpButtonType.Reply, 0);
			AddButton(280, 604, 9909, 9910, 0, GumpButtonType.Reply, 0);


			//Avatar stuff
			AddLabel(450, 340, 1153, @"Avatar");

			AddButton(450, 387, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddLabel(474, 385, 1153, @"Jovem");

			AddButton(450, 417, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddLabel(474, 415, 1153, @"Maduro(a)");

			AddButton(450, 447, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddLabel(474, 445, 1153, @"Velho(a)");

			//Avatar image with side buttons
			AddButton(983, 425, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddImage(724, 336, 40400);
			AddButton(691, 425, 9909, 9910, 0, GumpButtonType.Reply, 0);
			
			
			//Character Description
			AddLabel(450, 559, 1153, @"Descricao fisica");
			AddTextEntry(450, 594, 561, 141, 1153, 0, @"Escreva aqui a descrição física do seu personagem");

			//Forward Button
			AddButton(1055, 761, 4005, 4007, 0, GumpButtonType.Reply, 0);
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
		}
	}
}

