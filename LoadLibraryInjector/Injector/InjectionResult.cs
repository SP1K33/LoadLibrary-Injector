namespace LoadLibraryInjector.App
{
	public enum InjectionResult : uint
	{
		Success = 0,
		HookFunctionsFail,
		AllocationError,
		SetLoadLibraryPathError,
		LoadLibraryAddressNotFound,
		CallLoadLibraryError,
		RestoreHooksFail
	}
}