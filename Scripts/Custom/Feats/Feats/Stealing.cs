using System;
using Server;
using System.IO;
using System.Text;
using Server.Network;
using Server.Mobiles;

namespace Server.FeatInfo
{
	public class Stealing : BaseFeat
	{
		public override string Name{ get{ return "Stealing"; } }
		public override FeatList ListName{ get{ return Mobiles.FeatList.Stealing; } }
		public override FeatCost CostLevel{ get{ return FeatCost.High; } }
		
		public override SkillName[] AssociatedSkills{ get{ return new SkillName[]{ SkillName.Stealing }; } }
		public override FeatList[] AssociatedFeats{ get{ return new FeatList[]{ FeatList.Cutpurse }; } }
		
		public override FeatList[] Requires{ get{ return new FeatList[]{ FeatList.Snooping }; } }
		public override FeatList[] Allows{ get{ return new FeatList[]{ FeatList.PetStealing, FeatList.Cutpurse, FeatList.Lockpicking, 
				FeatList.PlantEvidence }; } }
		
		public override string FirstDescription{ get{ return "This skill will give you some knowledge in the Stealing skill, which will " +
					"allow you to attempt to steal objects from people's backpacks. [20% skill]"; } }
		public override string SecondDescription{ get{ return "[50% skill]"; } }
		public override string ThirdDescription{ get{ return "[100% skill]"; } }

		public override string FirstCommand{ get{ return "None"; } }
		public override string SecondCommand{ get{ return "None"; } }
		public override string ThirdCommand{ get{ return "None"; } }
		
		public override string FullDescription{ get{ return GetFullDescription(this); } }
		
		public static void Initialize(){ WriteWebpage(new Stealing()); }
		
		public Stealing() {}
	}
}
