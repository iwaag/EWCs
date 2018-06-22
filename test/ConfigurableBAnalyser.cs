using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace AGDev {
	
	public class FloatAnswerListenerBridge {
		public AnswerListener<float> listener;
		public FloatCallback callback;
		public FloatAnswerListenerBridge(AnswerListener<float> _listener) {
			listener = _listener;
			callback = (float answer) => {
				_listener.OnAnswerUpdate(answer);
			};
		}
	}
	
}