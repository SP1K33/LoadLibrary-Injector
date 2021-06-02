using LoadLibraryInjector.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace LoadLibraryInjector.Utils
{
	public static class ProcessUtils
	{
		private static bool IsX86BitProcess(IntPtr processHandle)
		{
			var version = Environment.OSVersion.Version;

			if (version.Major > 5 || version.Major == 5 && version.Minor >= 1)
			{
				return NativeWrapper.IsWow64Process(processHandle, out var result) && result;
			}

			return false;
		}

		/// <summary>
		/// IntPtr -> Process Handle
		/// </summary>
		/// <returns></returns>
		public static IDictionary<ProcessEntry32, IntPtr> GetX86Processes()
		{
			var result = new Dictionary<ProcessEntry32, IntPtr>();

			ProcessEntry32 processEntry32 = new ProcessEntry32
			{
				dwSize = (UInt32)Marshal.SizeOf(typeof(ProcessEntry32))
			};

			IntPtr snapshotHandle = NativeWrapper.CreateToolhelp32Snapshot((uint)SnapshotFlags.Process, 0);

			if (NativeWrapper.Process32First(snapshotHandle, ref processEntry32))
			{
				do
				{
					var processHandle = NativeWrapper.OpenProcess(ProcessAccessFlags.All, false,
						(int)processEntry32.th32ProcessID);

					if (IsX86BitProcess(processHandle))
					{
						result.Add(processEntry32, processHandle);
					}

				} while (NativeWrapper.Process32Next(snapshotHandle, ref processEntry32));
			}

			NativeWrapper.CloseHandle(snapshotHandle);

			return result;
		}

		public static IList<ProcessEntry32> SortEntries(IDictionary<ProcessEntry32, IntPtr> processes)
		{
			var entries = processes.Keys.ToList();
			entries.Sort((one, another) => String.CompareOrdinal(one.szExeFile, another.szExeFile));
			return entries;
		}
	}
}