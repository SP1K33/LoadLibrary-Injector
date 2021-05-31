namespace LoadLibraryInjector.Injector
{
	public readonly struct NativeFunction
	{
		public string FunctionName { get; }
		public string DllName { get; }

		public NativeFunction(string functionName, string dllName)
		{
			FunctionName = functionName;
			DllName = dllName;
		}
	}
}