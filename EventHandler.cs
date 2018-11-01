using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Threading;

namespace DefaultAmmo
{
    class EventHandler : IEventHandlerSpawn
    {
        private DefaultAmmoPlugin plugin;

        public EventHandler(DefaultAmmoPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnSpawn(PlayerSpawnEvent ev)
        {
            Thread plyjoindelaythread = new Thread(new ThreadStart(() => new playerjoindelaythread(this.plugin,ev)));
            plyjoindelaythread.Start();
        }
    }
}
