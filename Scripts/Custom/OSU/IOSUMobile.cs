using System;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
	public interface IOSUMobile
	{
		Mobile ShieldingMobile { get; set; }
		int RideBonus { get; }
		int Intimidated { get; set; }
		int Level { get; set; }
		int RageFeatLevel { get; set; }
		int ManeuverDamageBonus { get; set; }
		int ManeuverAccuracyBonus { get; set; }
		int TechniqueLevel { get; set; }
		double ShieldValue { get; set; }
		void RemoveShieldOfSacrifice();
		//void DisableManeuver();
		string GetPersonalPronoun();
		string GetReflexivePronoun();
		string GetPossessivePronoun();
		string GetPossessive();
		string Technique { get; set; }
		bool Fizzled { get; set; }
		bool Enthralled { get; set; }
		//bool CanUseMartialPower { get; }
		//bool CanUseMartialStance { get; }
		bool CleaveAttack { get; set; }
		bool CanDodge { get; }
		//bool IsAllyOf(Mobile mob);
		bool Evaded();
		bool Dodged();
		bool Snatched();
		//bool DeflectedProjectile();
		bool IsTired();
		bool IsTired(bool message);
		bool CanSummon();
		Feats Feats { get; set; }
		//CombatStyles CombatStyles { get; set; }
		DateTime NextFeatUse { get; set; }
		FeatList OffensiveFeat { get; set; }
		//BaseCombatManeuver CombatManeuver { get; set; }
		FeatList CurrentSpell { get; set; }
		Timer CrippledTimer { get; set; }
		Timer DazedTimer { get; set; }
		Timer TrippedTimer { get; set; }
		Timer StunnedTimer { get; set; }
		Timer DismountedTimer { get; set; }
		Timer BlindnessTimer { get; set; }
		Timer DeafnessTimer { get; set; }
		Timer MutenessTimer { get; set; }
		Timer DisabledLegsTimer { get; set; }
		Timer DisabledLeftArmTimer { get; set; }
		Timer DisabledRightArmTimer { get; set; }
		Timer FeintTimer { get; set; }
		Timer HealingTimer { get; set; }
		Timer AuraOfProtection { get; set; }
		Timer JusticeAura { get; set; }
		Timer Sanctuary { get; set; }
		Timer RageTimer { get; set; }
		Timer ManeuverBonusTimer { get; set; }
		Timer FreezeTimer { get; set; }
		//BaseStance Stance { get; set; }
		Mobile ShieldedMobile { get; set; }
		DateTime NextRage { get; set; }
		bool Deserialized { get; set; }
	}
}