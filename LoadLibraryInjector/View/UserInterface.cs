using LoadLibraryInjector.Injection;
using LoadLibraryInjector.Native;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoadLibraryInjector.View
{
	public static class UserInterface
	{
		public static UserInterfaceData GetUserInterfaceData(IList<ProcessEntry32> entries)
		{
			Application.EnableVisualStyles();

			using (var form = new UserForm())
			{
				string dllPath = GetDllPath();

				foreach (var entry in entries)
				{
					string fileName = $"{entry.szExeFile} ({entry.th32ProcessID})";
					form.AddProcessListItem(fileName);
				}

				ProcessEntry32 processEntry = default;

				if (form.ShowDialog() == DialogResult.OK)
				{
					processEntry = entries[form.GetSelectedProcessIndex()];
				}

				return new UserInterfaceData(dllPath, processEntry);
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