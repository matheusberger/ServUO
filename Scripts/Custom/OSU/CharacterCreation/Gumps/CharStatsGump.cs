using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

using OSU;
using OSU.Civilizations;

namespace Server.Gumps
{
	public struct CharStats
	{
		public CharStats(int max_p)
		{
			max_points = max_p;
			remaining_points = max_points;

			change_factor = 2;

			bonus_str = 0;
			bonus_dex = 0;
			bonus_int = 0;

			bonus_hitpoints = 0;
			bonus_stamina = 0;
			bonus_mana = 0;
		}

		public int max_points;
		public int remaining_points;
		//How many points to change at once
		public int change_factor;

		public int bonus_str;
		public int bonus_dex;
		public int bonus_int;

		public int bonus_hitpoints;
		public int bonus_stamina;
		public int bonus_mana;

		public void ProcessChanges(int index)
		{
			switch (index)
			{
				case -1:
					ChangeSTR(-1);
					break;
				case 1:
					ChangeSTR(1);
					break;
				case -2:
					ChangeDEX(-1);
					break;
				case 2:
					ChangeDEX(1);
					break;
				case -3:
					ChangeINT(-1);
					break;
				case 3:
					ChangeINT(1);
					break;
				case -4:
					ChangeHIT(-1);
					break;
				case 4:
					ChangeHIT(1);
					break;
				case -5:
					ChangeSTAMINA(-1);
					break;
				case 5:
					ChangeSTAMINA(1);
					break;
				case -6:
					ChangeMANA(-1);
					break;
				case 6:
					ChangeMANA(1);
					break;
			}
		}

		private bool UpdateRemainingPoints(int multiplier)
		{
			bool success = false;

			int tmp_points = remaining_points;
			tmp_points -= multiplier * change_factor;

			if(0 <= tmp_points && tmp_points <= 50)
			{
				remaining_points = tmp_points;
				success = true;
			}

			return success;
		}

		private void ChangeSTR(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_str += multiplier * change_factor;
				bonus_str = bonus_str < 0 ? 0 : bonus_str;
			}
		}

		private void ChangeDEX(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_dex += multiplier * change_factor;
				bonus_dex = bonus_dex < 0 ? 0 : bonus_dex;
			}
		}

		private void ChangeINT(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_int += multiplier * change_factor;
				bonus_int = bonus_int < 0 ? 0 : bonus_int;
			}
		}

		private void ChangeHIT(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_hitpoints += multiplier * change_factor;
				bonus_hitpoints = bonus_hitpoints < 0 ? 0 : bonus_hitpoints;
			}
		}

		private void ChangeSTAMINA(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_stamina += multiplier * change_factor;
				bonus_stamina = bonus_stamina < 0 ? 0 : bonus_stamina;
			}
		}

		private void ChangeMANA(int multiplier)
		{
			if (UpdateRemainingPoints(multiplier))
			{
				bonus_mana += multiplier * change_factor;
				bonus_mana = bonus_mana < 0 ? 0 : bonus_mana;
			}
		}
	}
	
	public struct AvatarDesc
	{
		public AvatarDesc(bool female, int age)
		{
			this.female = female;
			selected_age = age;
			if (female)
			{
				switch (selected_age)
				{
					case 0:
						base_avatar_code = 40519;
						current_avatar_code = 40519;
						max_avatar_code = 40598;
						break;
					case 1:
						base_avatar_code = 40599;
						current_avatar_code = 40599;
						max_avatar_code = 40648;
						break;
					case 2:
						base_avatar_code = 40650;
						current_avatar_code = 40650;
						max_avatar_code = 40679;
						break;
					default:
						base_avatar_code = 40359;
						current_avatar_code = 40359;
						max_avatar_code = 40438;
						break;
				}
			}
			else
			{
				switch (selected_age)
				{
					case 0:
						base_avatar_code = 40359;
						current_avatar_code = 40359;
						max_avatar_code = 40438;
						break;
					case 1:
						base_avatar_code = 40439;
						current_avatar_code = 40439;
						max_avatar_code = 40488;
						break;
					case 2:
						base_avatar_code = 40489;
						current_avatar_code = 40489;
						max_avatar_code = 40518;
						break;
					default:
						base_avatar_code = 40359;
						current_avatar_code = 40359;
						max_avatar_code = 40438;
						break;
				}
			}
		}

		private bool female;
		public int selected_age;

		public int base_avatar_code;
		public int current_avatar_code;

		private int max_avatar_code;

		public void UpdateAvatarCode(int i)
		{
			int tmp_avatar_code = current_avatar_code;
			tmp_avatar_code += i;
			if (base_avatar_code <= tmp_avatar_code && tmp_avatar_code <= max_avatar_code)
			{
				current_avatar_code = tmp_avatar_code;
			}
		}
	}

	public class CharStatsGump : Gump
	{

		private CharStats char_stats;
		private Civilization civ;
		private AvatarDesc avatar_info;

		public CharStatsGump(PlayerMobile m, CharStats current_stats, AvatarDesc current_desc)
			: base(0, 0)
		{	
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			m.CloseGump(typeof(CharStatsGump));

			civ = CharacterCreationSystem.temp_char.civilization;
			char_stats = current_stats;
			avatar_info = current_desc;

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, @"Criacao de Personagem");
			AddHtml(109, 128, 608, 126, @"<p>Distribui teus pontos, escolhe um avatar, e escreve a tua descrição</p>", (bool)false, (bool)true);
			AddLabel(113, 99, 1153, @"Caracteristicas Parte 2");
			
			AddLabel(125, 405, 1153, @"Forca");
			AddLabel(250, 405, 1153, "" + (civ.base_str + char_stats.bonus_str));

			AddLabel(125, 445, 1153, @"Destreza");
			AddLabel(250, 445, 1153, "" + (civ.base_dex + char_stats.bonus_dex));

			AddLabel(125, 485, 1153, @"Inteligencia");
			AddLabel(250, 485, 1153, "" + (civ.base_int + char_stats.bonus_int));

			AddLabel(125, 525, 1153, @"Hit Points");
			AddLabel(250, 525, 1153, "" + (civ.base_hitpoints + char_stats.bonus_hitpoints));

			AddLabel(125, 565, 1153, @"Estamina");
			AddLabel(250, 565, 1153, "" + (civ.base_stamina + char_stats.bonus_stamina));

			AddLabel(125, 605, 1153, @"Mana");
			AddLabel(250, 605, 1153, "" + (civ.base_mana + char_stats.bonus_mana));

			AddLabel(120, 341, 1153, @"Você ainda tem " + char_stats.remaining_points + " pontos para distribuir");

			//Subtract and Add buttons
			//STR
			AddButton(280, 404, 9909, 9910, -1, GumpButtonType.Reply, 0);
			AddButton(319, 404, 9903, 9904, 1, GumpButtonType.Reply, 0);
			//DEX
			AddButton(280, 444, 9909, 9910, -2, GumpButtonType.Reply, 0);
			AddButton(319, 444, 9903, 9904, 2, GumpButtonType.Reply, 0);
			//INT
			AddButton(280, 484, 9909, 9910, -3, GumpButtonType.Reply, 0);
			AddButton(319, 484, 9903, 9904, 3, GumpButtonType.Reply, 0);
			//HITPOINTS
			AddButton(280, 524, 9909, 9910, -4, GumpButtonType.Reply, 0);
			AddButton(319, 524, 9903, 9904, 4, GumpButtonType.Reply, 0);
			//STAMINA
			AddButton(280, 564, 9909, 9910, -5, GumpButtonType.Reply, 0);
			AddButton(319, 564, 9903, 9904, 5, GumpButtonType.Reply, 0);
			//MANA
			AddButton(280, 604, 9909, 9910, -6, GumpButtonType.Reply, 0);
			AddButton(319, 604, 9903, 9904, 6, GumpButtonType.Reply, 0);


			//Avatar stuff
			AddLabel(450, 340, 1153, @"Avatar");

			AddButton(450, 387, 9903, 9904, 20, GumpButtonType.Reply, 0);
			AddLabel(474, 385, 1153, @"Jovem");

			AddButton(450, 417, 9903, 9904, 30, GumpButtonType.Reply, 0);
			AddLabel(474, 415, 1153, @"Maduro(a)");

			AddButton(450, 447, 9903, 9904, 40, GumpButtonType.Reply, 0);
			AddLabel(474, 445, 1153, @"Velho(a)");

			//Avatar image with side buttons
			AddImage(724, 336, avatar_info.current_avatar_code);
			AddButton(691, 425, 9909, 9910, -10, GumpButtonType.Reply, 0);
			AddButton(983, 425, 9903, 9904, 10, GumpButtonType.Reply, 0);
			
			
			//Character Description
			AddLabel(450, 559, 1153, @"Descricao fisica");
			AddTextEntry(450, 594, 561, 141, 1153, 0, @"Escreva aqui a descrição física do seu personagem");

			//Forward Button
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
					if (char_stats.remaining_points > 0)
					{
						m.SendMessage("Você ainda não distribuiu todos os pontos");
						m.SendGump(new CharStatsGump(m, char_stats, avatar_info));
					}
					else
					{
						//update temp char in CharacterCreationSystem with the status, age, description and avatar;
						//go to next creation step
					}
					break;
				case -10:
					avatar_info.UpdateAvatarCode(-1);
					break;
				case 10:
					avatar_info.UpdateAvatarCode(1);
					break;
				case 20:
					avatar_info = new AvatarDesc(m.Female, 0);
					break;
				case 30:
					avatar_info = new AvatarDesc(m.Female, 1);
					break;
				case 40:
					avatar_info = new AvatarDesc(m.Female, 2);
					break;
				default:
					//update bonus status
					char_stats.ProcessChanges(info.ButtonID);
					break;
			}

			//show updated gump
			m.SendGump(new CharStatsGump(m, char_stats, avatar_info));
		}

	}
}

/*
Mulheres
Jovens
40519
40598

Maduras
40599
40648

Velhas
40650
40679
///////////////////////////////////
Homens
Jovens
40359
40438

maduros
40439
40488

velhos
40489
40518*/