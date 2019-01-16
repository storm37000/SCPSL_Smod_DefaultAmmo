using Smod2.Events;
using Smod2.EventHandlers;
using System.Threading;
using Smod2.API;
using System.Collections.Generic;

namespace defaultammo
{
	class EventHandler : IEventHandlerSpawn, IEventHandlerWaitingForPlayers
	{
		private Main plugin;
		private Dictionary<string, int> configs = new Dictionary<string, int>();

		public EventHandler(Main plugin)
		{
			this.plugin = plugin;
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			configs.Clear();

			foreach (int role in System.Enum.GetValues(typeof(Role)))
			{
				foreach (int ammo in System.Enum.GetValues(typeof(AmmoType)))
				{
					if (ammo >= -1)
					{
						try
						{
							configs.Add((Role)role + "__AMMO_" + ammo, plugin.GetConfigInt((Role)role + "__AMMO_" + ammo));
						}
						catch (System.ArgumentException)
						{
						}
					}
				}
			}
		}

		public void OnSpawn(PlayerSpawnEvent ev) // change to PlayerSetRoleEvent 
		{
			Thread plyjoindelaythread = new Thread(new ThreadStart(() => new playerjoindelaythread(configs, ev.Player)));
			plyjoindelaythread.Start();
		}
	}
}