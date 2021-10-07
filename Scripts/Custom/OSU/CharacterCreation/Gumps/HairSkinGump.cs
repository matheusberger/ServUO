using System.Collections.Generic;
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
		int current_hair = 0;
		int current_facial_hair;

		List<int> hair_list;
		List<int> facial_hair_list;

		public HairSkinGump(PlayerMobile m, int selected_hair = 0, int selected_facial_hair = 0)
			: base(0, 0)
		{
			m.CloseGump(typeof(HairSkinGump));

			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			m.DisplayPaperdollTo(m);

			current_hair = selected_hair;
			current_facial_hair = selected_facial_hair;
			InitHairList(m.Female);

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddHtml(109, 128, 608, 126, @"", (bool)false, (bool)true);
			AddLabel(113, 99, 1153, @"Caracteristicas Parte 3");
			AddHtml(120, 380, 224, 30, @"", (bool)false, (bool)false);


			//facial hair
			if (!m.Female)
			{
				AddLabel(674, 330, 1153, @"Pelos Faciais");

				int image_x = 608, image_y = 318, image_offset = 120;
				int button_x = 664, button_y = 385, button_offset = 125;

				for (int i = 0; i < facial_hair_list.Count; i++)
				{
					AddImage(image_x, image_y + (i * image_offset), facial_hair_list[i]);
					AddButton(button_x, button_y + (i * button_offset), facial_hair_list[i] == current_facial_hair ? 9904 : 9903, i == current_facial_hair ? 9903 : 9904, facial_hair_list[i], GumpButtonType.Reply, 0);
				}
			}


			//hair
			AddLabel(479, 330, 1153, @"Cabelos");

			int img_x = 395, img_y = 320, img_x_offset = 100, img_y_offset = 130;
			int btn_x = 445, btn_y = 385, btn_x_offset = 100, btn_y_offset = 125;

			for (int i = 0; i < hair_list.Count; i+=2)
			{
				for (int j = 0; j < 2; j++)
				{
					AddImage(img_x + (j * img_x_offset), img_y, hair_list[i + j]);
					AddButton(btn_x + (j * btn_x_offset), btn_y, current_hair == hair_list[i + j] ? 9904 : 9903, current_hair == hair_list[i + j] ? 9903 : 9904, hair_list[i + j], GumpButtonType.Reply, 0);
				}

				img_y += img_y_offset;
				btn_y += btn_y_offset;
			}

			//AddImage(395, 319, 52504);
			//AddImage(395, 448, 52502);
			//AddImage(395, 578, 52523);
			//AddImage(485, 319, 52531);
			//AddImage(485, 448, 52532);
			//AddImage(485, 578, 52554);

			//AddButton(445, 385, 9903, 9904, 0, GumpButtonType.Reply, 0);
			//AddButton(445, 510, 9903, 9904, 0, GumpButtonType.Reply, 0);
			//AddButton(445, 635, 9903, 9904, 0, GumpButtonType.Reply, 0);
			//AddButton(541, 385, 9903, 9904, 0, GumpButtonType.Reply, 0);
			//AddButton(541, 510, 9903, 9904, 0, GumpButtonType.Reply, 0);
			//AddButton(541, 635, 9903, 9904, 0, GumpButtonType.Reply, 0);



			//hair collor stuff
			AddLabel(818, 330, 1153, @"Cor de Cabelo");

			AddButton(787, 377, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 407, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 437, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 467, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 497, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 527, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 557, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 587, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 617, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 647, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(787, 677, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddLabel(815, 375, 48, @"Cor de Cabelo");
			AddLabel(815, 405, 143, @"Cor de Cabelo");
			AddLabel(815, 435, 38, @"Cor de Cabelo");
			AddLabel(815, 465, 341, @"Cor de Cabelo");
			AddLabel(815, 495, 1050, @"Cor de Cabelo");
			AddLabel(815, 525, 895, @"Cor de Cabelo");
			AddLabel(815, 555, 923, @"Cor de Cabelo");
			AddLabel(815, 585, 1001, @"Cor de Cabelo");
			AddLabel(815, 615, 1007, @"Cor de Cabelo");
			AddLabel(815, 645, 446, @"Cor de Cabelo");
			AddLabel(815, 675, 450, @"Cor de Cabelo");

			//skin tone stuff
			AddLabel(950, 330, 1153, @"Cor de Pele");

			AddButton(915, 378, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 408, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 438, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 468, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 498, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 528, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 558, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 588, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 618, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 648, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(915, 678, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddLabel(943, 378, 1002, @"Cor de Pele");
			AddLabel(943, 408, 1007, @"Cor de Pele");
			AddLabel(943, 438, 1016, @"Cor de Pele");
			AddLabel(943, 468, 1030, @"Cor de Pele");
			AddLabel(943, 498, 1034, @"Cor de Pele");
			AddLabel(943, 528, 1037, @"Cor de Pele");
			AddLabel(943, 558, 1043, @"Cor de Pele");
			AddLabel(943, 588, 1050, @"Cor de Pele");
			AddLabel(943, 618, 1052, @"Cor de Pele");
			AddLabel(943, 648, 1058, @"Cor de Pele");
			AddLabel(943, 678, 1021, @"Cor de Pele");

			AddLabel(125, 340, 1153, @"Nome");
			AddTextEntry(121, 380, 224, 30, 1153, 0, "Digite seu nome aqui");

			AddLabel(126, 442, 1153, @"Peso");
			AddTextEntry(121, 482, 224, 30, 1153, 0, "Digite seu peso aqui");

			AddLabel(128, 548, 1153, @"Altura");
			AddTextEntry(123, 586, 224, 30, 1153, 0, "Digite sua altura");

			//next step button
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
					//go to next
					break;
				default:
					current_hair = hair_list.Contains(info.ButtonID) ? info.ButtonID : current_hair;
					current_facial_hair = facial_hair_list.Contains(info.ButtonID) ? info.ButtonID : current_facial_hair;
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair));
					break;
			}
		}

		private void InitHairList(bool female)
		{
			hair_list = new List<int>();
			facial_hair_list = new List<int>();

			if (female)
			{
				hair_list.Add(52504);
				hair_list.Add(52502);
				hair_list.Add(52523);
				hair_list.Add(52531);
				hair_list.Add(52532);
				hair_list.Add(52554);
			}
			else
			{
				hair_list.Add(52469);
				hair_list.Add(52477);
				hair_list.Add(52481);
				hair_list.Add(52522);
				hair_list.Add(52524);
				hair_list.Add(52531);

				facial_hair_list.Add(52353);
				facial_hair_list.Add(52352);
				facial_hair_list.Add(52367);
			}

			current_hair = current_hair == 0 ? hair_list[0] : current_hair;
		}
	}
}
