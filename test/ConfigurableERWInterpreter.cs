using System.Runtime.InteropServices;
namespace AGDev {
	
	public class NativeConfigurableERWInterpreter : ERWordsInterpreter {
		public BridgeHelperFactoryBridge helperFactory;
		public NativeConfigurableERWInterpreter(System.IntPtr _implPtr, BridgeHelperFactoryBridge _helperFactory) {
			helperFactory = _helperFactory;
			implPtr = _implPtr;
		}
		BehaviorTrigger ERWordsInterpreter.InterpretERWordsAsBehavior(byte[] configuration) {
			//CHECK: faster way?
			System.IntPtr marshalArray = Marshal.AllocHGlobal(configuration.Length);
			Marshal.Copy(configuration, 0, marshalArray, configuration.Length);
			System.IntPtr behaviorTriggerPtr = InterpretERWordsAsBehavior(implPtr, marshalArray, configuration.Length);
			Marshal.FreeHGlobal(marshalArray);
			if (behaviorTriggerPtr == System.IntPtr.Zero)
				return null;
			return new NativeBehaviorTrigger(behaviorTriggerPtr, helperFactory);
		}
		public void SetFAnalyser(ConfigurableFAnalyser fAnalyser) {
			currentFAnlys = fAnalyser;
			SetFAnalyser(implPtr, currentFAnlys.implPtr);
		}
		public void SetGAnalyser(ConfigurableGAnalyser gAnalyser) {
			currentGAnlys = gAnalyser;
			SetGAnalyser(implPtr, currentGAnlys.implPtr);
		}
		public void SetBAnalyser(ConfigurableBAnalyser bAnalyser) {
			currentBAnlys = bAnalyser;
			SetBAnalyser(implPtr, currentBAnlys.implPtr);
		}
		public ConfigurableFAnalyser currentFAnlys;
		public ConfigurableGAnalyser currentGAnlys;
		public ConfigurableBAnalyser currentBAnlys;
		readonly public System.IntPtr implPtr;
		[DllImport("AGDevStdBridge")]
		extern static System.IntPtr InterpretERWordsAsBehavior(System.IntPtr iptr, System.IntPtr erWords, int length);
		[DllImport("AGDevStdBridge")]
		extern static void SetFAnalyser(System.IntPtr iptr, System.IntPtr analyser);
		[DllImport("AGDevStdBridge")]
		extern static void SetGAnalyser(System.IntPtr iptr, System.IntPtr analyser);
		[DllImport("AGDevStdBridge")]
		extern static void SetBAnalyser(System.IntPtr iptr, System.IntPtr analyser);
	}
}