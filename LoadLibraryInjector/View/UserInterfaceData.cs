using LoadLibraryInjector.Native;

namespace LoadLibraryInjector.View
{
	public readonly struct UserInterfaceData
	{
		public UserInterfaceData(string dllPath, ProcessEntry32 processEntry)
		{
			DllPath = dllPath;
			ProcessEntry = processEntry;
		}

		public ProcessEntry32 ProcessEntry { get; }
		public string DllPath { get; }

		public bool IsValid => !string.IsNullOrEmpty(ProcessEntry.szExeFile) && !string.IsNullOrEmpty(DllPath);
	}
}