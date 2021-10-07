using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

using OSU;

namespace Server.Gumps
{
	public class HairSkinGump : Gump
	{
		int current_hair = 0;
		int current_facial_hair;

		int current_hair_color;
		int current_skin_tone;

		List<int> hair_list;
		List<int> facial_hair_list;

		List<int> hair_color_list;
		List<int> skin_tone_list;

		public HairSkinGump(PlayerMobile m, int selected_hair = 50700, int selected_facial_hair = 50801, int selected_hair_color = 0x30, int selected_skin_tone = 1002)
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
			current_hair_color = selected_hair_color;
			current_skin_tone = selected_skin_tone;

			InitHairList(m.Female);
			InitColorList();

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddHtml(109, 128, 608, 126, @"", (bool)false, (bool)true);
			AddLabel(113, 99, 1153, @"Caracteristicas Parte 3");
			AddHtml(120, 380, 224, 30, @"", (bool)false, (bool)false);


			//facial hair selection
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


			//hair selection
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


			//hair color selection
			AddLabel(818, 330, 1153, @"Cor de Cabelo");

			btn_x = 787;
			btn_y = 375;
			btn_y_offset = 30;

			int label_x = 815,  label_y = 375,  label_offset = 30;

			for (int i = 0; i < hair_color_list.Count; i++)
			{
				AddButton(btn_x, btn_y + (i * btn_y_offset), current_hair_color == hair_color_list[i] ? 9904 : 9903, current_hair_color == hair_color_list[i] ? 9903 : 9904, hair_color_list[i], GumpButtonType.Reply, 0);
				AddLabel(label_x, label_y + (i * label_offset), hair_color_list[i], "Cor dos cabelos");
			}

			//skin tone stuff
			AddLabel(950, 330, 1153, @"Cor de Pele");

			btn_x = 915;
			btn_y = 375;
			btn_y_offset = 30;

			label_x = 945;
			label_y = 375;
			label_offset = 30;

			for (int i = 0; i < skin_tone_list.Count; i++)
			{
				AddButton(btn_x, btn_y + (i * btn_y_offset), current_skin_tone == skin_tone_list[i] ? 9904 : 9903, current_skin_tone == skin_tone_list[i] ? 9903 : 9904, skin_tone_list[i], GumpButtonType.Reply, 0);
				AddLabel(label_x, label_y + (i * label_offset), skin_tone_list[i], "Cor de pele");
			}

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

				case 50700:
				case 50701:
				case 52477:
				case 52481:
				case 52522:
				case 52524:
				case 52531:
					current_hair = info.ButtonID;
					m.HairItemID = 0;
					m.HairItemID = current_hair;
					//CharacterCreationSystem.PreviewHair(current_hair);
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair, current_hair_color, current_skin_tone));
					break;
				case 50801:
				case 52352:
				case 52367:
					current_facial_hair = info.ButtonID;
					m.FacialHairItemID = current_facial_hair;
					//CharacterCreationSystem.PreviewFacialHair(current_facial_hair);
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair, current_hair_color, current_skin_tone));
					break;
				case 184:
				case 143:
				case 382:
				case 341:
				case 996:
				case 895:
				case 923:
				case 998:
				case 930:
				case 446:
				case 450:
					current_hair_color = info.ButtonID;
					CharacterCreationSystem.PreviewHairColor(current_hair_color);
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair, current_hair_color, current_skin_tone));
					break;
				case 1002:
				case 1007:
				case 1016:
				case 1030:
				case 1034:
				case 1037:
				case 1043:
				case 1050:
				case 1052:
				case 1058:
				case 1021:
					current_skin_tone = info.ButtonID;
					CharacterCreationSystem.PreviewSkinTone(current_skin_tone);
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair, current_hair_color, current_skin_tone));
					break;
				default:
					m.SendGump(new HairSkinGump(m, current_hair, current_facial_hair, current_hair_color, current_skin_tone));
					break;
			}
		}

		private void InitColorList()
		{
			hair_color_list = new List<int>();
			skin_tone_list = new List<int>();

			hair_color_list.Add(184);
			hair_color_list.Add(143);
			hair_color_list.Add(382);
			hair_color_list.Add(341);
			hair_color_list.Add(996);
			hair_color_list.Add(895);
			hair_color_list.Add(923);
			hair_color_list.Add(998);
			hair_color_list.Add(930);
			hair_color_list.Add(446);
			hair_color_list.Add(450);

			skin_tone_list.Add(1002);
			skin_tone_list.Add(1007);
			skin_tone_list.Add(1016);
			skin_tone_list.Add(1030);
			skin_tone_list.Add(1034);
			skin_tone_list.Add(1037);
			skin_tone_list.Add(1043);
			skin_tone_list.Add(1050);
			skin_tone_list.Add(1052);
			skin_tone_list.Add(1058);
			skin_tone_list.Add(1021);
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
				//hair_list.Add(52469);
				hair_list.Add(50700);
				hair_list.Add(50701);
				hair_list.Add(0x203D);
				hair_list.Add(0x2044);
				hair_list.Add(0x2045);
				hair_list.Add(0x204A);

				//facial_hair_list.Add(52353);
				facial_hair_list.Add(50801);
				facial_hair_list.Add(52352);
				facial_hair_list.Add(52367);
			}

			current_hair = current_hair == 0 ? hair_list[0] : current_hair;
		}
	}
}
