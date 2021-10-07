using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Misc;

namespace Server.BackgroundInfo
{	
	public class Unenergetic : BaseBackground
	{
		public override int Cost{ get{ return -1000; } }
		public override string Name{ get{ return "Unenergetic"; } }
		public override BackgroundList ListName{ get{ return BackgroundList.Unenergetic; } }
		public override string Description{ get{ return "This flaw will lower both your current Stamina and also your Stamina cap by 5."; } }
		public override string FullDescription{ get{ return GetFullDescription(this); } }
		
		public static void Initialize(){ }

        public override bool CanAcquireThisBackground( PlayerMobile m )
        {
            if( m.osu_char_info.stamina < 10 )
            {
                m.SendMessage( "Your current Stamina is already too low." );
                return false;
            }

            return base.CanAcquireThisBackground( m );
        }
		
		public override void OnAddedTo( PlayerMobile m )
		{
			m.osu_char_info.stamina -= 5;
		}
		
		public override void OnRemovedFrom( PlayerMobile m )
		{
			m.osu_char_info.stamina += 5;
		}
		
		public Unenergetic() {}
	}
}
