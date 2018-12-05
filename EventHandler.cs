using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Threading;

namespace defaultammo
{
    class EventHandler : IEventHandlerSpawn
    {
        private Plugin plugin;

        public EventHandler(Main plugin)
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