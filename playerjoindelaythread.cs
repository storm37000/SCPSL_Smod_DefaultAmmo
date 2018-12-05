using System.Threading;
using Smod2;
using Smod2.API;
using Smod2.Events;

namespace defaultammo
{
    class playerjoindelaythread
    {
        public playerjoindelaythread(Plugin plugin, PlayerSpawnEvent ev)
        {
            Thread.Sleep(100);
            plugin.Debug("=:BEFORE:= Player: " + ev.Player.Name);
            plugin.Debug(" Ammo(Epsilon-11): " + ev.Player.GetAmmo((AmmoType)0));
            plugin.Debug(" Ammo(MP7, Logicer): " + ev.Player.GetAmmo((AmmoType)1));
            plugin.Debug(" Ammo(COM15, P90): " + ev.Player.GetAmmo((AmmoType)2));
            foreach (int ind in System.Enum.GetValues(typeof(AmmoType)))
            {
                int ammo = plugin.GetConfigInt((Role)ev.Player.TeamRole.Role + "__AMMO_" + ind);
                if (ammo >= 0)
                {
                    ev.Player.SetAmmo((AmmoType)ind, ammo);
                }
            }
            plugin.Debug("=:AFTER:= Player: " + ev.Player.Name);
            plugin.Debug(" Ammo(Epsilon-11): " + ev.Player.GetAmmo((AmmoType)0));
            plugin.Debug(" Ammo(MP7, Logicer): " + ev.Player.GetAmmo((AmmoType)1));
            plugin.Debug(" Ammo(COM15, P90): " + ev.Player.GetAmmo((AmmoType)2));
        }
    }
}