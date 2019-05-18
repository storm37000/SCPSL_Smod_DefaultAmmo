using Smod2.Events;
using Smod2.EventHandlers;
using Smod2.API;
using System.Collections.Generic;
using MEC;

namespace defaultammo
{
	class EventHandler : IEventHandlerSetRole, IEventHandlerWaitingForPlayers
	{
		private Main plugin;
		private Dictionary<string, int> configs = new Dictionary<string, int>();

		public EventHandler(Main plugin)
		{
			this.plugin = plugin;
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (!plugin.UpToDate)
			{
				plugin.outdatedmsg();
			}
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

		IEnumerator<float> setAmmo(Smod2.API.Player Player)
		{
			plugin.Debug("=:BEFORE:= Player: " + Player.Name);
			plugin.Debug(" Ammo(Epsilon-11): " + Player.GetAmmo((AmmoType)0));
			plugin.Debug(" Ammo(MP7, Logicer): " + Player.GetAmmo((AmmoType)1));
			plugin.Debug(" Ammo(COM15, P90): " + Player.GetAmmo((AmmoType)2));
			yield return Timing.WaitForOneFrame;
			foreach (int ind in System.Enum.GetValues(typeof(AmmoType)))
			{
				int ammo = configs[(Role)Player.TeamRole.Role + "__AMMO_" + ind];
				if (ammo >= 0)
				{
					Player.SetAmmo((AmmoType)ind, ammo);
				}
			}
			plugin.Debug("=:AFTER:= Player: " + Player.Name);
			plugin.Debug(" Ammo(Epsilon-11): " + Player.GetAmmo((AmmoType)0));
			plugin.Debug(" Ammo(MP7, Logicer): " + Player.GetAmmo((AmmoType)1));
			plugin.Debug(" Ammo(COM15, P90): " + Player.GetAmmo((AmmoType)2));
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			Timing.RunCoroutine(setAmmo(ev.Player), Segment.FixedUpdate);
		}
	}
}