using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fennorad.Mapbox
{
    public class CallbackAction 
    {
        private readonly IJSObjectReference _Runtime;
        private readonly string _EventType;
        private readonly Delegate _Delegate;
        private readonly Type _Type;

        public CallbackAction(IJSObjectReference runtime, string eventType, Delegate @delegate, Type type)
        {
            _Runtime = runtime;
            _EventType = eventType;
            _Delegate = @delegate;
            _Type = type;
        }

        public CallbackAction(IJSObjectReference runtime, string eventType, Delegate @delegate)
        {
            _Runtime = runtime;
            _EventType = eventType;
            _Delegate = @delegate;
        }

        public async Task Remove()
        {
            // TODO: Need to determine if it is a popup or map event to remove. 
            //await _Runtime.InvokeVoidAsync("Mapbox.off", _EventType);

            await Task.CompletedTask;
        }

        [JSInvokable]
        public void Invoke(string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                _Delegate.DynamicInvoke();
                return;
            }

            var response = JsonSerializer.Deserialize(args, _Type);

            _Delegate.DynamicInvoke(response);
        }

        [JSInvokable]
        public void InvokeWithoutArgs()
        {
            _Delegate.DynamicInvoke();
        }
    }
}
