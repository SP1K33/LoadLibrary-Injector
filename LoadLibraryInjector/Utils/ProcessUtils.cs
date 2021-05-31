using System;
using System.Collections.Generic;
using System.Diagnostics;
using LoadLibraryInjector.Native;

namespace LoadLibraryInjector.Utils
{
	public static class ProcessUtils
	{
		public static IList<Process> GetX86Processes(IEnumerable<Process> processes)
		{
			var result = new List<Process>();
			foreach (var process in processes)
			{
				if (process == null)
					continue;

				try
				{
					if (process.IsX86BitProcess())
					{
						result.Add(process);
					}
				}
				catch { /* ignored */ }
			}

			result.Sort((one, another) => String.CompareOrdinal(one.MainModule.ModuleName, another.MainModule.ModuleName));
			return result;
		}

		private static bool IsX86BitProcess(this Process process)
		{
			var version = Environment.OSVersion.Version;

			if (version.Major > 5 || version.Major == 5 && version.Minor >= 1)
			{
				return NativeWrapper.IsWow64Process(process.Handle, out var result) && result;
			}

			return false;
		}
	}
}