using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server.Gumps
{
	public class OSUSkillsGump : Gump
	{
		public enum JobID
		{
			Artificer = 111,
			Academic = 222,
			Mage = 333,
			Warrior = 444
		}

		public struct Job
		{
			public Job(int id, string name, string description)
			{
				this.id = id;
				this.name = name;
				this.description = description;
			}

			public int id;
			public string name;
			public string description;
		}

		public struct JobList
		{
			public static Job Artificer = new Job(111, "Artíficer", "olocobixo o cara é o magaiver");
			public static Job Academic = new Job(222, "Acadêmico", "olocobixo o cara é o megamente");
			public static Job Mage = new Job(333, "Mago", "olocobixo o cara é o dumbledore");
			public static Job  Warrior = new Job(444, "Guerreiro", "olocobixo o cara é o he-man");
		}

		private int first_bonus = 500;
		private int second_bonus = 200;
		private int third_bonus = 200;
		private int fourth_bonus = 100;

		private HashSet<JobID> available_jobs;
		private HashSet<JobID> available_sec1_jobs;
		private HashSet<JobID> available_sec2_jobs;
		private HashSet<JobID> available_ter_jobs;

		private Dictionary<int, JobID> current_jobs;

		public OSUSkillsGump(PlayerMobile m, Dictionary<int, JobID> setup = null)
			: base(0, 0)
		{
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			available_jobs = GetAllPossibleJobs();
			current_jobs = setup ?? new Dictionary<int, JobID>();
			SetRemainingJobs();

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, "Criacao de Personagem");
			AddHtml(109, 128, 608, 126, "Olha as instrunção, bença:", false, true);
			
			AddLabel(113, 99, 1153, "Skills");

			int selected_job = -1;
			if (current_jobs.ContainsKey(first_bonus))
			{
				 selected_job = (int)current_jobs[first_bonus];
			}

			AddButton(111, 331, JobList.Artificer.id == selected_job ? 9904 : 9903, JobList.Artificer.id == selected_job ? 9903 : 9904, JobList.Artificer.id, GumpButtonType.Reply, 0);
			AddLabel(140, 332, 1153, JobList.Artificer.name);
			AddButton(111, 371, JobList.Academic.id == selected_job ? 9904 : 9903, JobList.Academic.id == selected_job ? 9903 : 9904, JobList.Academic.id, GumpButtonType.Reply, 0);
			AddLabel(140, 372, 1153, JobList.Academic.name);
			AddButton(111, 411, JobList.Mage.id == selected_job ? 9904 : 9903, JobList.Mage.id == selected_job ? 9903 : 9904, JobList.Mage.id, GumpButtonType.Reply, 0);
			AddLabel(140, 412, 1153, JobList.Mage.name);
			AddButton(111, 451, JobList.Warrior.id == selected_job ? 9904 : 9903, JobList.Warrior.id == selected_job ? 9903 : 9904, JobList.Warrior.id, GumpButtonType.Reply, 0);
			AddLabel(140, 452, 1153, JobList.Warrior.name);

			string current_description = "";
			//Job desc
			if (current_jobs.ContainsKey(first_bonus))
			{
				switch(current_jobs[first_bonus])
				{
					case JobID.Artificer:
						current_description = JobList.Artificer.description;
						break;
					case JobID.Academic:
						current_description = JobList.Academic.description;
						break;
					case JobID.Mage:
						current_description = JobList.Mage.description;
						break;
					case JobID.Warrior:
						current_description = JobList.Warrior.description;
						break;
				}
			}
			AddHtml(481, 339, 495, 249, current_description, false, true);


			AddLabel(478, 614, 1153, "Secundaria 1");

			int lbl_x = 478, btn_x = 450, y_pos = 647, y_offset = 33;
			
			JobID[] jobIDs = new JobID[available_sec1_jobs.Count];
			available_sec1_jobs.CopyTo(jobIDs);
			
			var jobList = new List<Job>();
			foreach (var id in jobIDs)
			{
				switch(id)
				{
					case JobID.Artificer:
						jobList.Add(JobList.Artificer);
						break;
					case JobID.Academic:
						jobList.Add(JobList.Academic);
						break;
					case JobID.Mage:
						jobList.Add(JobList.Mage);
						break;
					case JobID.Warrior:
						jobList.Add(JobList.Warrior);
						break;
				}
			}

			for (int i = 0; i < jobIDs.Length; i++)
			{
				AddButton(btn_x, y_pos + (i * y_offset), 9903, 9904, 11*(i+1), GumpButtonType.Reply, 0);
				AddLabel(lbl_x, y_pos + (i * y_offset), 1153, jobList[i].name);
			}

			
			AddLabel(478, 680, 1153, "");
			AddLabel(478, 713, 1153, "");
			AddButton(450, 680, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(450, 713, 9903, 9904, 0, GumpButtonType.Reply, 0);

			AddLabel(667, 614, 1153, "Secundaria 2");
			AddLabel(667, 647, 1153, "");
			AddLabel(667, 680, 1153, "");
			AddButton(639, 647, 9903, 9904, 0, GumpButtonType.Reply, 0);
			AddButton(639, 680, 9903, 9904, 0, GumpButtonType.Reply, 0);

			AddLabel(866, 614, 1153, "Tercearia");
			AddLabel(867, 647, 1153, "");
			AddButton(839, 647, 9903, 9904, 0, GumpButtonType.Reply, 0);

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
					//check if current_jobs has main, secondary and terciary roles before going next
					break;
				default:
					if (available_sec1_jobs.Contains((JobID)info.ButtonID))
					{
						available_sec1_jobs = GetAllPossibleJobs();
						available_sec1_jobs.Remove((JobID)info.ButtonID);
						current_jobs[first_bonus] = (JobID)info.ButtonID;
					}
					m.SendGump(new OSUSkillsGump(m, current_jobs));
					break;
			}
		}

		public void SetRemainingJobs()
		{
			available_sec1_jobs = new HashSet<JobID>();
			available_sec2_jobs = new HashSet<JobID>();
			available_ter_jobs = new HashSet<JobID>();

			if (current_jobs.ContainsKey(first_bonus))
			{
				available_sec1_jobs = available_jobs;
				available_sec1_jobs.Remove(current_jobs[first_bonus]);

				if (current_jobs.ContainsKey(second_bonus))
				{
					available_sec2_jobs = available_sec1_jobs;
					available_sec2_jobs.Remove(current_jobs[second_bonus]);

					if (current_jobs.ContainsKey(third_bonus))
					{
						available_ter_jobs = available_sec2_jobs;
						available_ter_jobs.Remove(current_jobs[third_bonus]);

						JobID[] last = new JobID[available_ter_jobs.Count];
						available_ter_jobs.CopyTo(last);

						current_jobs[fourth_bonus] = last[0];
					}
				}
			}
		}

		public HashSet<JobID> GetAllPossibleJobs()
		{
			HashSet<JobID> set_jobs = new HashSet<JobID>();

			set_jobs.Add(JobID.Artificer);
			set_jobs.Add(JobID.Academic);
			set_jobs.Add(JobID.Mage);
			set_jobs.Add(JobID.Warrior);

			return set_jobs;
		}
	}
}
