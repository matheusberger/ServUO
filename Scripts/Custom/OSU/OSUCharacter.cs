using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Engines.XmlSpawner2;

using OSU;
using OSU.Civilizations;

namespace OSU
{
	public class OSUCharacter : IOSUMobile
	{
		public enum Age
		{
			Young,
			Mature,
			Old
		}

		public void InitOSUChar(PlayerMobile m)
		{
			this.base_char = m;
			m.osu_char_info = this;

			current_combat_points = 0;
			current_profession_points = 0;
			current_magic_points = 0;
			current_hability_points = 0;

			total_combat_points = 0;
			total_profession_points = 0;
			total_magic_points = 0;
			total_hability_points = 0;

			max_combat_points = 0;
			max_profession_points = 0;
			max_magic_points = 0;
			max_hability_points = 0;

			char_level = 1;
			max_char_level = 25;
			current_char_exp = 0;

			str = 1;
			dex = 1;
			inteligence = 1;

			char_weight = 50;
			char_height = 50;

			char_avatar = 0;
			char_physical_des = "";

			known_languages = new Languages[0];
			skill_list = new OSUSkill[0];

			Backgrounds = new Backgrounds();
		}

		public PlayerMobile base_char;

		//leveling point system info
		//how many points the player currently has in each category
		public int current_combat_points;
		public int current_profession_points;
		public int current_magic_points;
		public int current_hability_points;

		//how many points the player has gathered in all his playthough 
		public int total_combat_points;
		public int total_profession_points;
		public int total_magic_points;
		public int total_hability_points;

		//maximum points a player can gather through each playtrhough:  total_x_points < max_x_points
		public int max_combat_points;
		public int max_profession_points;
		public int max_magic_points;
		public int max_hability_points;

		//char stats
		private int char_level;
		private int max_char_level;
		private int current_char_exp;

		public int str;
		public int dex;
		public int inteligence;

		public int hitpoints;
		public int stamina;
		public int mana;

		//char details
		private int char_weight;
		private int char_height;

		public int char_avatar;
		public string char_physical_des;

		public int hairstyle;
		public int facial_hair;
		public int hair_color; 
		public int skin_tone;
		
		//rp system
		public Civilization civilization;
		private int disguise;

		public Age age;

		public Languages[] known_languages;
		private OSUSkill[] skill_list;

		//backgrounds
		public Backgrounds Backgrounds;

		public int background_bonus_points;

		#region Stuff we need to make Feats work

		private bool m_IsVampire;
		[CommandProperty(AccessLevel.Administrator)]
		public bool IsVampire { get { return m_IsVampire; } set { m_IsVampire = value; } }

		#endregion

		#region  IOSUMobile
		private Feats m_Feats;
		private int m_FeatSlots;

		private FeatList m_SpecialAttack;
		private FeatList m_OffensiveFeat;
		private FeatList m_FightingStance;
		private FeatList m_CurrentSpell;

		private Mobile m_ShieldingMobile;
		private Mobile m_ShieldedMobile;
		private double m_ShieldValue;

		private int m_Intimidated;
		private int m_RageFeatLevel;

		private int m_ManeuverDamageBonus;
		private int m_ManeuverAccuracyBonus;

		private string m_Technique;
		private int m_TechniqueLevel;

		private bool m_Fizzled;
		private bool m_CleaveAttack;
		private int m_LightPenalty;
		private int m_MediumPenalty;
		private int m_HeavyPenalty;

		private int m_ArmourPieces;
		private int m_LightPieces;
		private int m_MediumPieces;
		private int m_HeavyPieces;
		private DateTime m_NextFeatUse;

		public DateTime NextSummoningAllowed;
		private DateTime m_NextRage;

		private bool m_Deserialized;
		public bool Deserialized { get { return m_Deserialized; } set { m_Deserialized = value; } }

		#region TIMERS
		private Timer m_RageTimer;
		public Timer m_StunnedTimer;
		private Timer m_TrippedTimer;
		private Timer m_CrippledTimer;
		private Timer m_DazedTimer;
		private Timer m_AuraOfProtection;
		private Timer m_JusticeAura;
		private Timer m_Sanctuary;
		public Timer m_BlindnessTimer;
		public Timer m_DeafnessTimer;
		public Timer m_MutenessTimer;
		public Timer m_DismountedTimer;
		public Timer m_DisabledLegsTimer;
		public Timer m_DisabledLeftArmTimer;
		public Timer m_DisabledRightArmTimer;
		public Timer m_PetrifiedTimer;
		public Timer m_FeintTimer;
		public Timer m_HealingTimer;
		private Timer m_BloodOfXorgoth;
		private Timer m_ManeuverBonusTimer;
		public Timer m_KOPenalty;
		private Timer m_FreezeTimer;
		public Timer FreezeTimer { get { return m_FreezeTimer; } set { m_FreezeTimer = value; } }
		public Timer CrippledTimer { get { return m_CrippledTimer; } set { m_CrippledTimer = value; } }
		public Timer DazedTimer { get { return m_DazedTimer; } set { m_DazedTimer = value; } }
		public Timer TrippedTimer { get { return m_TrippedTimer; } set { m_TrippedTimer = value; } }
		public Timer StunnedTimer { get { return m_StunnedTimer; } set { m_StunnedTimer = value; } }
		public Timer DismountedTimer { get { return m_DismountedTimer; } set { m_DismountedTimer = value; } }
		public Timer BlindnessTimer { get { return m_BlindnessTimer; } set { m_BlindnessTimer = value; } }
		public Timer DeafnessTimer { get { return m_DeafnessTimer; } set { m_DeafnessTimer = value; } }
		public Timer MutenessTimer { get { return m_MutenessTimer; } set { m_MutenessTimer = value; } }
		public Timer DisabledLegsTimer { get { return m_DisabledLegsTimer; } set { m_DisabledLegsTimer = value; } }
		public Timer DisabledLeftArmTimer { get { return m_DisabledLeftArmTimer; } set { m_DisabledLeftArmTimer = value; } }
		public Timer DisabledRightArmTimer { get { return m_DisabledRightArmTimer; } set { m_DisabledRightArmTimer = value; } }
		public Timer FeintTimer { get { return m_FeintTimer; } set { m_FeintTimer = value; } }
		public Timer HealingTimer { get { return m_HealingTimer; } set { m_HealingTimer = value; } }
		public Timer AuraOfProtection { get { return m_AuraOfProtection; } set { m_AuraOfProtection = value; } }
		public Timer JusticeAura { get { return m_JusticeAura; } set { m_JusticeAura = value; } }
		public Timer Sanctuary { get { return m_Sanctuary; } set { m_Sanctuary = value; } }
		public Timer RageTimer { get { return m_RageTimer; } set { m_RageTimer = value; } }
		public Timer BloodOfXorgoth { get { return m_BloodOfXorgoth; } set { m_BloodOfXorgoth = value; } }
		public Timer ManeuverBonusTimer { get { return m_ManeuverBonusTimer; } set { m_ManeuverBonusTimer = value; } }

		#endregion

		public Feats Feats
		{
			get
			{
				if (m_Feats == null)
					m_Feats = new Feats();

				return m_Feats;
			}
			set { }
		}

		public Mobile ShieldingMobile
		{
			get { return m_ShieldingMobile; }
			set { m_ShieldingMobile = value; }
		}

		public Mobile ShieldedMobile { get { return m_ShieldedMobile; } set { m_ShieldedMobile = value; } }


		public int RideBonus { get { return 0; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Intimidated
		{
			get { return m_Intimidated; }
			set { m_Intimidated = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int Level
		{
			get { return char_level; }
			set { char_level = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int RageFeatLevel
		{
			get
			{
				if (RageTimer == null)
					return 0;

				return m_RageFeatLevel;
			}
			set { m_RageFeatLevel = value; }
		}

		public int ManeuverDamageBonus { get { return m_ManeuverDamageBonus; } set { m_ManeuverDamageBonus = value; } }
		public int ManeuverAccuracyBonus { get { return m_ManeuverAccuracyBonus; } set { m_ManeuverAccuracyBonus = value; } }
		
		public string Technique { get { return m_Technique; } set { m_Technique = value; } }
		public int TechniqueLevel { get { return m_TechniqueLevel; } set { m_TechniqueLevel = value; } }

		public double ShieldValue
		{
			get { return m_ShieldValue; }
			set { m_ShieldValue = value; }
		}

		public virtual void RemoveShieldOfSacrifice()
		{
			this.ShieldingMobile = null;
			this.ShieldValue = 0.0;
			this.ShieldedMobile = null;
		}

		//public virtual void DisableManeuver()
		//{
		//	BaseCombatManeuver oldManeuver = this.CombatManeuver;
		//	this.CombatManeuver = null;
		//	this.OffensiveFeat = FeatList.None;

		//	if (this.ManeuverBonusTimer == null)
		//	{
		//		this.ManeuverAccuracyBonus = 0;
		//		this.ManeuverDamageBonus = 0;
		//	}

		//	if (oldManeuver.GetType() != this.CombatManeuver.GetType())
		//		this.Send(new MobileStatus(this, this));
		//}

		public virtual string GetPersonalPronoun()
		{
			if (base_char.Female)
				return "she";

			return "he";
		}

		public virtual string GetReflexivePronoun()
		{
			if (base_char.Female)
				return "her";

			return "him";
		}

		public virtual string GetPossessivePronoun()
		{
			if (base_char.Female)
				return "her";

			return "his";
		}

		public virtual string GetPossessive()
		{
			if (base_char.Name.EndsWith("s"))
				return "'";

			return "'s";
		}
		public bool Fizzled { get { return m_Fizzled; } set { m_Fizzled = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Enthralled
		{
			get
			{
				XmlEnthrallment enthrall = XmlAttach.FindAttachment(this, typeof(XmlEnthrallment)) as XmlEnthrallment;

				if (enthrall != null)
					return true;

				return false;
			}

			set
			{
				if (value == false)
				{
					XmlAttachment att = XmlAttach.FindAttachment(this, typeof(XmlEnthrallment));

					if (att != null)
					{
						//this.Emote("*snaps out of a trance*");
						att.Delete();
					}
				}
			}
		}

		//public bool CanUseMartialPower
		//{
		//	get
		//	{
		//		if (!(this.Weapon is Fists) || !CanUseMartialStance)
		//		{
		//			this.SendMessage(60, "You can only use this power with your bare hands, unmounted and unemcumbered by armour.");
		//			this.DisableManeuver();
		//			return false;
		//		}

		//		return true;
		//	}
		//}

		//public bool CanUseMartialStance
		//{
		//	get
		//	{
		//		if (this.Mounted || this.HasArmourPenalties || !(this.Weapon is Fists))
		//			return false;

		//		return true;
		//	}
		//}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool CleaveAttack
		{
			get { return m_CleaveAttack; }
			set { m_CleaveAttack = value; }
		}

		public bool CanDodge
		{
			get
			{
				if (Paralyzed || TrippedTimer != null || HeavyPenalty > 0)
					return false;

				if (LightPieces < 1)
					return true;

				return (this.Feats.GetFeatLevel(FeatList.ArmouredDodge) * 3) > TotalPenalty;
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Paralyzed
		{
			get
			{
				if (this.StunnedTimer != null || this.m_PetrifiedTimer != null || HasParalyzeAtt(base_char))
					return true;

				return Paralyzed;
			}
			set
			{
				Paralyzed = value;
			}
		}

		public static bool HasParalyzeAtt(Mobile m)
		{
			XmlParalyze par = XmlAttach.FindAttachment(m, typeof(XmlParalyze)) as XmlParalyze;

			if (par != null)
				return true;

			return false;
		}

		//public bool IsAllyOf(Mobile mob)
		//{
		//	return BaseAI.AreAllies(this, mob);
		//}

		public bool Evaded()
		{
			if (Paralyzed)
				return false;

			if (TrippedTimer != null)
				return false;

			return EvadedCheck(this);
		}

		public static bool EvadedCheck(IOSUMobile mob)
		{
			if (mob.Feats.GetFeatLevel(FeatList.Evade) > 0)
			{
				int chancetoevade = ((IOSUMobile)mob).Feats.GetFeatLevel(FeatList.Evade) * 10;
				int evaderoll = Utility.RandomMinMax(1, 100);

				if (chancetoevade >= evaderoll)
					return true;
			}
			return false;
		}

		public static bool DodgedCheck(IOSUMobile mob)
		{
			int offset = 0;

			if ((mob.Feats.GetFeatLevel(FeatList.EnhancedDodge) + offset) > 0)
			{
				int chancetoevade = (mob.Feats.GetFeatLevel(FeatList.EnhancedDodge) * 5) + offset;
				int evaderoll = Utility.RandomMinMax(1, 100);

				if (chancetoevade >= evaderoll)
					return true;
			}
			return false;
		}

		public bool Dodged()
		{
			return DodgedCheck(this);
		}

		public static bool SnatchedCheck(IOSUMobile mob)
		{
			if (mob.Feats.GetFeatLevel(FeatList.CatchProjectiles) > 0)
			{
				int chancetoevade = mob.Feats.GetFeatLevel(FeatList.CatchProjectiles) * 10;
				int evaderoll = Utility.Random(100);

				if (chancetoevade >= evaderoll)
					return true;
			}
			return false;
		}

		public bool Snatched()
		{
			return SnatchedCheck(this);
		}

		//public static bool DeflectedProjectileCheck(Mobile mob)
		//{
		//	BaseShield shield = mob.FindItemOnLayer(Layer.TwoHanded) as BaseShield;

		//	if (shield != null)
		//	{
		//		if (shield is BaseShield && ((IKhaerosMobile)mob).Feats.GetFeatLevel(FeatList.DeflectProjectiles) > 0)
		//		{
		//			int chancetoblock = ((IKhaerosMobile)mob).Feats.GetFeatLevel(FeatList.DeflectProjectiles) * 10;
		//			int blockroll = Utility.RandomMinMax(1, 100);

		//			if (chancetoblock >= blockroll)
		//				return true;
		//		}
		//	}
		//	return false;
		//}

		//public bool DeflectedProjectile()
		//{
		//	return DeflectedProjectileCheck(this);
		//}

		public bool IsTired(bool message)
		{
			bool isTired = IsTired();

			if (isTired && message)
				base_char.SendMessage("You are too tired for that.");

			return isTired;
		}

		public bool IsTired()
		{
			if (this.m_KOPenalty != null)
				return true;

			return false;
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int LightPenalty
		{
			get { return m_LightPenalty; }
			set { m_LightPenalty = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int MediumPenalty
		{
			get { return m_MediumPenalty; }
			set { m_MediumPenalty = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int HeavyPenalty
		{
			get { return m_HeavyPenalty; }
			set { m_HeavyPenalty = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int TotalPenalty { get { return (m_LightPenalty + m_MediumPenalty + m_HeavyPenalty); } }

		//public double GetVampiricRegenBonus()
		//{
		//	double extra = (double)GetVampireTimeOffset(GetHour());

		//	if (extra > 0)
		//		return (extra * 2.5);

		//	return 0.0;
		//}

		[CommandProperty(AccessLevel.GameMaster)]
		public int LightPieces
		{
			get { return m_LightPieces; }
			set { m_LightPieces = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int MediumPieces
		{
			get { return m_MediumPieces; }
			set { m_MediumPieces = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int HeavyPieces
		{
			get { return m_HeavyPieces; }
			set { m_HeavyPieces = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public int ArmourPieces
		{
			get { return m_ArmourPieces; }
			set { m_ArmourPieces = value; }
		}

		public virtual bool CanSummon()
		{
			return (DateTime.Compare(DateTime.Now, this.NextSummoningAllowed) > 0 && base_char.Followers < base_char.FollowersMax);
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime NextFeatUse
		{
			get { return m_NextFeatUse; }
			set { m_NextFeatUse = value; }
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public FeatList OffensiveFeat
		{
			get { return m_OffensiveFeat; }
			set { m_OffensiveFeat = value; }
		}

		public FeatList CurrentSpell { get { return m_CurrentSpell; } set { m_CurrentSpell = value; } }
		public DateTime NextRage { get { return m_NextRage; } set { m_NextRage = value; } }

		#endregion
	}
}