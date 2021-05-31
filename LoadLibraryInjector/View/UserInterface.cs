using LoadLibraryInjector.App;
using LoadLibraryInjector.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using LoadLibraryInjector.Injector;

namespace LoadLibraryInjector.View
{
	public static class UserInterface
	{
		public static UserInterfaceData GetUserInterfaceData(IEnumerable<Process> processes)
		{
			Application.EnableVisualStyles();

			using (var form = new UserForm())
			{
				string dllPath = GetDllPath();

				var x86Processes = ProcessUtils.GetX86Processes(processes);

				foreach (var process in x86Processes)
				{
					string fileName = $"{process.MainModule.ModuleName} ({process.Id})";
					form.AddProcessListItem(fileName);
				}

				IntPtr processHandle = IntPtr.Zero;

				if (form.ShowDialog() == DialogResult.OK)
				{
					var selectedProcess = x86Processes[form.GetSelectedProcessIndex()];
					processHandle = selectedProcess.Handle;
				}

				return new UserInterfaceData(dllPath, processHandle);
			}
		}

		private static string GetDllPath()
		{
			string result = string.Empty;

			using (var dialog = new OpenFileDialog
			{
				Multiselect = false,

				Title = "Select dll",
				Filter = "dll file (*.dll) |*.dll",
				InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
			})
			{
				if (dialog.ShowDialog() == DialogResult.OK)
					result = dialog.FileName;
			}

			return result;
		}

		public static void ShowInjectionResult(InjectionResult result)
		{
			MessageBox.Show($@"Injection result: {Enum.GetName(typeof(InjectionResult), result)}", @"LoadLibrary Injector", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static void ShowUserDataError()
		{
			MessageBox.Show(@"Dll path or process is invalid", @"LoadLibrary Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowElevatedError()
		{
			MessageBox.Show(@"Run as administrator", @"LoadLibrary Injector", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}