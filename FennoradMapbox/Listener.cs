using System;
using System.Threading.Tasks;

namespace FennoradMapbox
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
