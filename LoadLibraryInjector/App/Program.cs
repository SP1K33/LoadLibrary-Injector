/*

																		The injector is written by SP1K3. 
																		If you are using materials from this source code, please leave credits
																		Enjoy :)


                  __   ____    ___    ___         __    ____   ___    ___    ___    ___  __  __        ____   _  __     __   ____  _____ ______  ____    ___
                 / /  / __ \  / _ |  / _ \       / /   /  _/  / _ )  / _ \  / _ |  / _ \ \ \/ /       /  _/  / |/ / __ / /  / __/ / ___//_  __/ / __ \  / _ \
                / /__/ /_/ / / __ | / // /      / /__ _/ /   / _  | / , _/ / __ | / , _/  \  /       _/ /   /    / / // /  / _/  / /__   / /   / /_/ / / , _/
               /____/\____/ /_/ |_|/____/      /____//___/  /____/ /_/|_| /_/ |_|/_/|_|   /_/       /___/  /_/|_/  \___/  /___/  \___/  /_/    \____/ /_/|_|


											                  __                ____   ___   ___   __ __   ____
											                 / /   __ __       / __/  / _ \ <  /  / //_/  |_  /
											                / _ \ / // /      _\ \   / ___/ / /  / ,<    _/_ <
											               /_.__/ \_, /      /___/  /_/    /_/  /_/|_|  /____/
											                     /___/


*/

using LoadLibraryInjector.View;
using System;
using System.Diagnostics;
using System.Security.Principal;

namespace LoadLibraryInjector.App
{
	internal class Program
	{
		private static bool IsElevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

		[STAThread]
		internal static void Main()
		{
			if (!IsElevated)
			{
				UserInterface.ShowElevatedError();
				return;
			}

			var data = UserInterface.GetUserInterfaceData(Process.GetProcesses());

			if (!data.IsValid)
			{
				UserInterface.ShowUserDataError();
			}
			else
			{
				var injectionResult = Injector.LoadLibraryInjector.Inject(data.ProcessHandle, data.DllPath);
				UserInterface.ShowInjectionResult(injectionResult);
			}

		}
	}
}
