using Smod2;
using Smod2.API;
using Smod2.Attributes;

namespace defaultammo
{
	[PluginDetails(
		author = "storm37000",
		name = "Default Ammo",
		description = "Sets custom default ammo amount.",
		id = "s37k.defaultammo",
		version = "1.0.4",
		SmodMajor = 3,
		SmodMinor = 2,
		SmodRevision = 0
		)]
	class Main : Plugin
	{
		public override void OnDisable()
		{
			this.Info(this.Details.name + " has been disabled.");
		}
		public override void OnEnable()
		{
			this.Info(this.Details.name + " has been enabled.");
			string[] hosts = { "https://storm37k.com/addons/", "http://74.91.115.126/addons/" };
			ushort version = ushort.Parse(this.Details.version.Replace(".", string.Empty));
			bool fail = true;
			string errorMSG = "";
			foreach (string host in hosts)
			{
				using (UnityEngine.WWW req = new UnityEngine.WWW(host + this.Details.name + ".ver"))
				{
					while (!req.isDone) { }
					errorMSG = req.error;
					if (string.IsNullOrEmpty(req.error))
					{
						ushort fileContentV = 0;
						if (!ushort.TryParse(req.text, out fileContentV))
						{
							errorMSG = "Parse Failure";
							continue;
						}
						if (fileContentV > version)
						{
							this.Error("Your version is out of date, please visit the Smod discord and download the newest version");
						}
						fail = false;
						break;
					}
				}
			}
			if (fail)
			{
				this.Error("Could not fetch latest version txt: " + errorMSG);
			}
		}

		public override void Register()
		{
			foreach (int role in System.Enum.GetValues(typeof(Role)))
			{
				foreach (int ammo in System.Enum.GetValues(typeof(AmmoType)))
				{
					this.AddConfig(new Smod2.Config.ConfigSetting((Role)role + "__AMMO_" + ammo, -1, Smod2.Config.SettingType.NUMERIC, true, ""));
				}
			}
			this.AddEventHandlers(new EventHandler(this));
		}
	}
}