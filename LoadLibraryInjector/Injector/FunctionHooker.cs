﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LoadLibraryInjector.Native;

namespace LoadLibraryInjector.Injector
{
	public static class FunctionHooker
	{

		private static readonly NativeFunction[] NativeFunctions =
		{
			new NativeFunction("NtOpenFile", "ntdll"),
			new NativeFunction("LdrLoadDll", "ntdll"),

			new NativeFunction("VirtualProtectEx", "kernel32"),
			new NativeFunction("VirtualAlloc", "kernel32"),
			new NativeFunction("VirtualAllocEx", "kernel32"),
			new NativeFunction("VirtualProtect", "kernel32"),

			new NativeFunction("FreeLibrary", "kernel32"),
			new NativeFunction("FreeLibrary", "KernelBase"),

			new NativeFunction("LoadLibraryExW", "kernel32"),
			new NativeFunction("LoadLibraryExA", "kernel32"),
			new NativeFunction("LoadLibraryW", "kernel32"),
			new NativeFunction("LoadLibraryA", "kernel32"),
			new NativeFunction("LoadLibraryExA", "KernelBase"),
			new NativeFunction("LoadLibraryExW", "KernelBase"),

			new NativeFunction("CreateProcessW", "kernel32"),
			new NativeFunction("CreateProcessA", "kernel32"),
			new NativeFunction("ResumeThread", "KernelBase")
		};

		/// <summary>
		/// string -> Function name
		/// byte[] -> Original function bytes
		/// </summary>
		private static readonly Dictionary<string, byte[]> OriginalFunctionBytes = new Dictionary<string, byte[]>();

		public static bool HookFunctions(IntPtr processHandle)
		{
			foreach (var function in NativeFunctions)
			{
				if (!HookFunction(processHandle, function.MethodName, function.DllName))
					return false;
			}

			return true;
		}

		public static bool RestoreHooks(IntPtr processHandle)
		{
			foreach (var function in NativeFunctions)
			{
				if (!RestoreHook(processHandle, function.MethodName, function.DllName))
					return false;
			}

			return true;
		}

		private static bool HookFunction(IntPtr processHandle, string functionName, string dllName)
		{
			var originalFunctionAddress = NativeWrapper.GetProcAddress(NativeWrapper.LoadLibrary(dllName), functionName);

			if (originalFunctionAddress == IntPtr.Zero)
				return false;

			byte[] originalFunctionBytes = new byte[6];

			if (!NativeWrapper.ReadProcessMemory(processHandle, originalFunctionAddress, originalFunctionBytes, sizeof(byte) * 6, out _))
				return false;

			OriginalFunctionBytes.Add(functionName, originalFunctionBytes);

			byte[] originalDllBytes = new byte[6];

			GCHandle pinnedArray = GCHandle.Alloc(originalDllBytes, GCHandleType.Pinned);
			IntPtr originalDllBytesPointer = pinnedArray.AddrOfPinnedObject();

			NativeWrapper.memcpy(originalDllBytesPointer, originalFunctionAddress, (UIntPtr)(sizeof(byte) * 6));

			return NativeWrapper.WriteProcessMemory(processHandle, originalFunctionAddress, originalDllBytes, sizeof(byte) * 6, out _);
		}

		private static bool RestoreHook(IntPtr processHandle, string functionName, string dllName)
		{
			IntPtr originalFunctionAddress = NativeWrapper.GetProcAddress(NativeWrapper.LoadLibrary(dllName), functionName);

			if (originalFunctionAddress == IntPtr.Zero)
				return false;

			return NativeWrapper.WriteProcessMemory(processHandle, originalFunctionAddress, OriginalFunctionBytes[functionName], sizeof(byte) * 6, out _);
		}

	}
}