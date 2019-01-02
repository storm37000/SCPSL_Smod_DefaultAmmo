using System.Threading;
using Smod2;
using Smod2.API;
using Smod2.Events;

namespace defaultammo
{
	class playerjoindelaythread
	{
		public playerjoindelaythread(Main plugin, Player Player)
		{
			Thread.Sleep(100);
//			  plugin.Debug("=:BEFORE:= Player: " + Player.Name);
//			  plugin.Debug(" Ammo(Epsilon-11): " + Player.GetAmmo((AmmoType)0));
//			  plugin.Debug(" Ammo(MP7, Logicer): " + Player.GetAmmo((AmmoType)1));
//			  plugin.Debug(" Ammo(COM15, P90): " + Player.GetAmmo((AmmoType)2));
			foreach (int ind in System.Enum.GetValues(typeof(AmmoType)))
			{
				int ammo = plugin.GetConfigInt((Role)Player.TeamRole.Role + "__AMMO_" + ind);
				if (ammo >= 0)
				{
					Player.SetAmmo((AmmoType)ind, ammo);
				}
			}
//			  plugin.Debug("=:AFTER:= Player: " + Player.Name);
//			  plugin.Debug(" Ammo(Epsilon-11): " + Player.GetAmmo((AmmoType)0));
//			  plugin.Debug(" Ammo(MP7, Logicer): " + Player.GetAmmo((AmmoType)1));
//			  plugin.Debug(" Ammo(COM15, P90): " + Player.GetAmmo((AmmoType)2));
		}
	}
}