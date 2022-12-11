using System;
using System.Threading.Tasks;

namespace Fennorad.Mapbox
{
    public class Listener : IDisposable
    {
        private readonly CallbackAction _Action; 

        public Listener(CallbackAction action)
        {
            _Action = action;
        }

        public void Dispose()
        {
            _ = Remove();
        }

        public async Task Remove()
        {
            await _Action.Remove();
        }
    }
}
