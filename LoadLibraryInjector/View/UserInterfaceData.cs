using System;

namespace LoadLibraryInjector.View
{
	public readonly struct UserInterfaceData
	{
		public UserInterfaceData(string dllPath, IntPtr processHandle)
		{
			DllPath = dllPath;
			ProcessHandle = processHandle;
		}

		public IntPtr ProcessHandle { get; }
		public string DllPath { get; }

		public bool IsValid => ProcessHandle != IntPtr.Zero && !string.IsNullOrEmpty(DllPath);
	}
}