using LoadLibraryInjector.App;
using LoadLibraryInjector.Native;
using System;
using System.Text;

namespace LoadLibraryInjector.Injector
{
	public static class LoadLibraryInjector
	{
		public static InjectionResult Inject(IntPtr processHandle, string dllPath)
		{
			if (!FunctionHooker.HookFunctions(processHandle))
				return InjectionResult.HookFunctionsFail;

			if (!AllocateLibrarySize(processHandle, new IntPtr(dllPath.Length), out var allocatedAddress))
				return InjectionResult.AllocationError;

			if (!SetLoadLibraryPath(processHandle, dllPath, allocatedAddress))
				return InjectionResult.SetLoadLibraryPathError;

			if (!GetLoadLibraryAddress(out var loadLibraryAddress))
				return InjectionResult.LoadLibraryAddressNotFound;

			if (!CallRemoteLoadLibrary(processHandle, loadLibraryAddress, allocatedAddress))
				return InjectionResult.CallLoadLibraryError;

			if (!FunctionHooker.RestoreHooks(processHandle))
				return InjectionResult.RestoreHooksFail;

			NativeWrapper.CloseHandle(processHandle);

			return InjectionResult.Success;
		}

		private static bool AllocateLibrarySize(IntPtr processHandle, IntPtr size, out IntPtr address)
		{
			address = NativeWrapper.VirtualAllocEx(processHandle, IntPtr.Zero, size, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

			if (address == IntPtr.Zero)
				return false;

			return true;
		}

		private static bool SetLoadLibraryPath(IntPtr processHandle, string dllPath, IntPtr allocatedAddress)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(dllPath);

			if (!NativeWrapper.WriteProcessMemory(processHandle, allocatedAddress, bytes, (int)bytes.Length, out _))
				return false;

			return true;
		}

		private static bool GetLoadLibraryAddress(out IntPtr loadLibraryAddress)
		{
			loadLibraryAddress = IntPtr.Zero;

			var kernel32Handle = NativeWrapper.GetModuleHandle("Kernel32.dll");

			if (kernel32Handle == IntPtr.Zero)
				return false;

			loadLibraryAddress = NativeWrapper.GetProcAddress(kernel32Handle, "LoadLibraryA");

			if (loadLibraryAddress == IntPtr.Zero)
				return false;

			return true;
		}

		private static bool CallRemoteLoadLibrary(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr allocatedProcessAddress)
		{
			var threadHandle = NativeWrapper.CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryAddress, allocatedProcessAddress, 0, IntPtr.Zero);

			if (threadHandle == IntPtr.Zero)
				return false;

			NativeWrapper.CloseHandle(threadHandle);

			return true;
		}
	}
}