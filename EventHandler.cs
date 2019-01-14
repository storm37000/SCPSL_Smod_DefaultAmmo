using Smod2.Events;
using Smod2.EventHandlers;
using System.Threading;

namespace defaultammo
{
	class EventHandler : IEventHandlerSpawn
	{
		private Main plugin;

		public EventHandler(Main plugin)
		{
			this.plugin = plugin;
		}

		public void OnSpawn(PlayerSpawnEvent ev) // change to PlayerSetRoleEvent 
		{
			Thread plyjoindelaythread = new Thread(new ThreadStart(() => new playerjoindelaythread(this.plugin, ev.Player)));
			plyjoindelaythread.Start();
		}
	}
}