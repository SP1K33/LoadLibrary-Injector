namespace LoadLibraryInjector.App
{
	public enum InjectionResult : uint
	{
		Access = 0,
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