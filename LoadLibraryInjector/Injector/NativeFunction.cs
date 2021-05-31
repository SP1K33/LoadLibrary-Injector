namespace LoadLibraryInjector.Injector
{
	public class NativeFunction
	{
		public string MethodName { get; }
		public string DllName { get; }

		public NativeFunction(string methodName, string dllName)
		{
			MethodName = methodName;
			DllName = dllName;
		}
	}
}