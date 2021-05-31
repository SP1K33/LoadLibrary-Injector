namespace LoadLibraryInjector.App
{
	public enum InjectionResult : uint
	{
		Success = 0,
		ProcessNotFound,
		OpenProcessError,
		HookFunctionsFail,
		AllocationError,
		SetLoadLibraryPathError,
		LoadLibraryAddressNotFound,
		CallLoadLibraryError,
		RestoreHooksFail
	}
}