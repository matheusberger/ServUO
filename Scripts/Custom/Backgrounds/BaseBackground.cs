using System;
using Server;
using System.IO;
using System.Text;
using Server.Network;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;
using Server.Engines.XmlSpawner2;
using System.Collections;
using System.Collections.Generic;

namespace Server.BackgroundInfo
{	
	public abstract class BaseBackground : object
	{
		//public static string WikiFolder = Server.Misc.StatusPage.LiveServer == true ? @"\Wiki\data\pages\backgrounds\" : @"\Backgrounds\";
		
		private int m_Level;
		public int Level{ get{ return m_Level; } set{ m_Level = value; } }
		
		public abstract int Cost{ get; }
		public abstract string Name{ get; }
		public abstract BackgroundList ListName{ get; }
		public abstract string Description{ get; }
		public abstract string FullDescription{ get; }
		public static bool OldPages = false;
		
		public virtual bool MeetsOurRequirements( PlayerMobile m )
		{
            XmlBackground.CleanUp(m, ListName);
			return MeetsOurRequirements( m, false );
		}
		
		public virtual bool MeetsOurRequirements( PlayerMobile m, bool message )
		{
            XmlBackground.CleanUp(m, ListName);
			return true;
		}
		
		public bool TestBackgroundForPurchase( PlayerMobile m, BackgroundList opposite )
		{
            XmlBackground.CleanUp(m, opposite);
			return TestBackgroundForPurchase( m, opposite, false );
		}
		
		public bool TestBackgroundForPurchase( PlayerMobile m, BackgroundList opposite, bool message )
		{
            XmlBackground.CleanUp(m, opposite);
			if( m.osu_char_info.Backgrounds.BackgroundDictionary[opposite].Level > 0 )
			{
				if( message )
					m.SendMessage( "You already have the " + Backgrounds.Catalogue[opposite].Name + " background. " +
					              "Remove it first if you wish to acquire the " + Name + " background." );
				
				return false;
			}
			
			return true;
		}
		
		public bool HasAnotherLookBackground( PlayerMobile m )
		{
			return HasAnotherLookBackground( m, false );
		}
		
		public bool HasAnotherLookBackground( PlayerMobile m, bool message )
		{
            XmlBackground.CleanUp(m, ListName);
            
            bool warned = false;
			
			if( ListName != BackgroundList.Attractive )
				warned = !TestBackgroundForPurchase( m, BackgroundList.Attractive, message );
			
			if( !warned && ListName != BackgroundList.GoodLooking )
				warned = !TestBackgroundForPurchase( m, BackgroundList.GoodLooking, message );
			
			if( !warned && ListName != BackgroundList.Gorgeous )
				warned = !TestBackgroundForPurchase( m, BackgroundList.Gorgeous, message );
			
			if( !warned && ListName != BackgroundList.Homely )
				warned = !TestBackgroundForPurchase( m, BackgroundList.Homely, message );
			
			if( !warned &&  ListName != BackgroundList.Ugly )
				warned = !TestBackgroundForPurchase( m, BackgroundList.Ugly, message );
			
			if( !warned && ListName != BackgroundList.Hideous )
				warned = !TestBackgroundForPurchase( m, BackgroundList.Hideous, message );
			
			return warned;
		}
		
		public bool HasThisBackground( PlayerMobile m )
		{
			return (m.osu_char_info.Backgrounds.BackgroundDictionary[ListName].Level > 0);
		}
		
		public void AttemptPurchase( PlayerMobile m )
		{
            XmlBackground.CleanUp(m, ListName);
            
            if( !MeetsOurRequirements(m, true) )
				return;

			m.osu_char_info.background_bonus_points -= Cost;

			Level = 1;
			m.SendMessage("You have acquired the " + Name + " background.");
			OnAddedTo(m);
		}

        public virtual bool CanAcquireThisBackground( PlayerMobile m )
        {
            XmlBackground.CleanUp(m, ListName);
            return true;
        }

        public virtual bool CanRemoveThisBackground( PlayerMobile m )
        {
            XmlBackground.CleanUp(m, ListName);
            return true;
        }
		
		public virtual void OnAddedTo( PlayerMobile m )
		{
		}
		
		public virtual void OnRemovedFrom( PlayerMobile m )
		{
		}
		
		public static string GetFullDescription( BaseBackground background )
		{
			string description = "<b>" + background.Name + "</b><br><br>" +
				"<i>Description:</i> " +
				background.Description + "<br><br>" +
				"<i>CP Cap Offset:</i> " + background.Cost.ToString();
			
			return description;
		}
		
		public static string GetWebpageDescription( BaseBackground background )
		{		
			string description = "<p id=\"featName\">" + background.Name + "</p>" +
				"<span id=\"boldTopic\">Description:</span> " +
				background.Description + "<br><br>" +
				"<span id=\"boldTopic\">CP Cap Offset:</span> " + background.Cost.ToString();
			
			return description;
		}
	}
}
