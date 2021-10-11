using System;
using System.Linq;
using System.Collections.Generic;

using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

using OSU;

namespace OSU
{
	public enum JobID
	{
		None = 0,
		Artificer = 111,
		Academic = 222,
		Mage = 333,
		Warrior = 444
	}
}

namespace Server.Gumps
{
	public class OSUSkillsGump : Gump
	{
		public struct Job
		{
			public Job(JobID id, string name, string description)
			{
				this.id = id;
				this.name = name;
				this.description = description;
			}

			public JobID id;
			public string name;
			public string description;
		}

		public struct JobList
		{
			public static Job Artificer = new Job(JobID.Artificer, "Artíficer", "olocobixo o cara é o magaiver");
			public static Job Academic = new Job(JobID.Academic, "Acadêmico", "olocobixo o cara é o megamente");
			public static Job Mage = new Job(JobID.Mage, "Mago", "olocobixo o cara é o dumbledore");
			public static Job  Warrior = new Job(JobID.Warrior, "Guerreiro", "olocobixo o cara é o he-man");
		}

		private List<Job> available_sec_a_jobs;
		private List<Job> available_sec_b_jobs;
		private Job terciary_job;

		private JobID[] job_choices;

		public OSUSkillsGump(PlayerMobile m, JobID[] setup = null)
			: base(0, 0)
		{
			this.Closable = false;
			this.Disposable = false;
			this.Dragable = true;
			this.Resizable = false;

			int index = 0;
			job_choices = setup ?? new JobID[4];
			//foreach (var choice in job_choices)
			//{
			//	Console.WriteLine($"{index} choice: {choice}");
			//	index++;
			//}

			SetAvailableASecondaries();
			SetAvailableBSecondaries();
			SetTerciary();

			AddPage(0);
			AddImage(12, 12, 40322);
			AddLabel(328, 45, 1153, "Criacao de Personagem");
			AddHtml(109, 128, 608, 126, "Olha as instrunção, bença:", false, true);

			DrawMainJob();
			DrawSecondaryAJob();
			DrawSecondaryBJob();

			AddLabel(866, 614, 1153, "Tercearia");
			if (job_choices[2] != JobID.None)
			{
				AddLabel(867, 647, 1153, terciary_job.name);
				//AddButton(839, 647, 9904, 9904, 0, GumpButtonType.Reply, 0);
			}


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
					if (job_choices[3] != JobID.None)
					{
						CharacterCreationSystem.SetSkillsCap(job_choices);
						m.SendMessage("Skills set!");
					}
					break;
				case 11:
					job_choices[1] = available_sec_a_jobs[0].id;
					break;
				case 22:
					job_choices[1] = available_sec_a_jobs[1].id;
					break;
				case 33:
					job_choices[1] = available_sec_a_jobs[2].id;
					break;
				case 12:
					job_choices[2] = available_sec_b_jobs[0].id;
					break;
				case 24:
					job_choices[2] = available_sec_b_jobs[1].id;
					break;
				default:
					job_choices[0] = (JobID)info.ButtonID;
					break;
			}


			if (info.ButtonID != 999)
			{
				m.CloseGump(typeof(OSUSkillsGump));
				m.SendGump(new OSUSkillsGump(m, job_choices), true);
			}
		}

		private void DrawMainJob()
		{
			AddLabel(113, 99, 1153, "Skills");

			JobID selected_job = (JobID)(-1);
			if (job_choices[0] != JobID.None)
			{
				selected_job = job_choices[0];
			}

			AddButton(111, 331, JobList.Artificer.id == selected_job ? 9904 : 9903, JobList.Artificer.id == selected_job ? 9903 : 9904, (int)JobList.Artificer.id, GumpButtonType.Reply, 0);
			AddLabel(140, 332, 1153, JobList.Artificer.name);
			AddButton(111, 371, JobList.Academic.id == selected_job ? 9904 : 9903, JobList.Academic.id == selected_job ? 9903 : 9904, (int)JobList.Academic.id, GumpButtonType.Reply, 0);
			AddLabel(140, 372, 1153, JobList.Academic.name);
			AddButton(111, 411, JobList.Mage.id == selected_job ? 9904 : 9903, JobList.Mage.id == selected_job ? 9903 : 9904, (int)JobList.Mage.id, GumpButtonType.Reply, 0);
			AddLabel(140, 412, 1153, JobList.Mage.name);
			AddButton(111, 451, JobList.Warrior.id == selected_job ? 9904 : 9903, JobList.Warrior.id == selected_job ? 9903 : 9904, (int)JobList.Warrior.id, GumpButtonType.Reply, 0);
			AddLabel(140, 452, 1153, JobList.Warrior.name);

			string current_description = "";
			if (job_choices[0] != JobID.None)
			{
				switch (job_choices[0])
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
		}

		private void DrawSecondaryAJob()
		{
			AddLabel(478, 614, 1153, "Secundaria A");

			if (job_choices[0] != JobID.None)
			{
				int lbl_x = 478, btn_x = 450, y_pos = 647, y_offset = 33;
				int index = 0;
				foreach (var job in available_sec_a_jobs)
				{
					AddButton(btn_x, y_pos + (index * y_offset), job.id == job_choices[1] ? 9904 : 9903, job.id == job_choices[1] ? 9903 : 9904, 11 * (index + 1), GumpButtonType.Reply, 0);
					AddLabel(lbl_x, y_pos + (index * y_offset), 1153, job.name);
					index++;
				}
			}
		}

		private void DrawSecondaryBJob()
		{
			AddLabel(667, 614, 1153, "Secundaria B");

			if (job_choices[1] != JobID.None)
			{
				int lbl_x = 667, btn_x = 639, y_pos = 647, y_offset = 33;
				int index = 0;
				foreach (var job in available_sec_b_jobs)
				{
					AddButton(btn_x, y_pos + (index * y_offset), job.id == job_choices[2] ? 9904 : 9903, job.id == job_choices[2] ? 9903 : 9904, 12 * (index + 1), GumpButtonType.Reply, 0);
					AddLabel(lbl_x, y_pos + (index * y_offset), 1153, job.name);
					index++;
				}
			}
		}

		private void SetAvailableASecondaries()
		{
			available_sec_a_jobs = new List<Job>();
			if (job_choices[0] != JobID.None)
			{
				available_sec_a_jobs = GetAllPossibleJobs();
				var tmp = new List<Job>(available_sec_a_jobs);
				
				foreach (var job in tmp)
				{
					if (job.id == job_choices[0])
					{
						available_sec_a_jobs.Remove(job);
					}
				}
			}
		}

		private void SetAvailableBSecondaries()
		{
			available_sec_b_jobs = new List<Job>();
			if (job_choices[1] != JobID.None)
			{
				available_sec_b_jobs = GetAllPossibleJobs();
				var tmp = new List<Job>(available_sec_b_jobs);

				foreach (var job in tmp)
				{
					if (job.id == job_choices[0] || job.id == job_choices[1])
					{
						available_sec_b_jobs.Remove(job);
					}
				}
			}
		}

		private void SetTerciary()
		{
			if (job_choices[2] != JobID.None)
			{
				var remaining_job = GetAllPossibleJobs();
				var tmp = new List<Job>(remaining_job);

				foreach (var job in tmp)
				{
					if (job.id == job_choices[0] || job.id == job_choices[1] || job.id == job_choices[2])
					{
						remaining_job.Remove(job);
					}
				}

				terciary_job = remaining_job[0];
				job_choices[3] = terciary_job.id;
			}
		}

		private List<Job> GetAllPossibleJobs()
		{
			List<Job> set_jobs = new List<Job>();

			set_jobs.Add(JobList.Artificer);
			set_jobs.Add(JobList.Academic);
			set_jobs.Add(JobList.Mage);
			set_jobs.Add(JobList.Warrior);

			return set_jobs;
		}
	}
}
