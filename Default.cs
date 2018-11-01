using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Events;
using Smod2.EventHandlers;
using System;

namespace DefaultAmmo
{
    [PluginDetails(
        author = "storm37000",
        name = "Default Ammo",
        description = "Sets custom default ammo amount.",
        id = "s37k.defaultammo",
        version = "1.0.1",
        SmodMajor = 3,
        SmodMinor = 1,
        SmodRevision = 21
        )]
    class DefaultAmmoPlugin : Plugin
    {
        public override void OnDisable()
        {
            this.Info(this.Details.name + " has been disabled.");
        }
		public override void OnEnable()
		{
			bool SSLerr = false;
			this.Info(this.Details.name + " has been enabled.");
			string hostfile = "http://pastebin.com/raw/9VQi53JQ";
			string[] hosts = new System.Net.WebClient().DownloadString(hostfile).Split('\n');
			while (true)
			{
				try
				{
					string host = hosts[0];
					if (SSLerr) { host = hosts[1]; }
					ushort version = ushort.Parse(this.Details.version.Replace(".", string.Empty));
					ushort fileContentV = ushort.Parse(new System.Net.WebClient().DownloadString(host + this.Details.name + ".ver"));
					if (fileContentV > version)
					{
						this.Info("Your version is out of date, please visit the Smod discord and download the newest version");
					}
					break;
				}
				catch (System.Exception e)
				{
					if (SSLerr == false)
					{
						SSLerr = true;
						continue;
					}
					this.Error("Could not fetch latest version txt: " + e.Message);
					break;
				}
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
            this.AddEventHandler(typeof(IEventHandlerSpawn), new EventHandler(this), Priority.Highest);
        }
    }
}
