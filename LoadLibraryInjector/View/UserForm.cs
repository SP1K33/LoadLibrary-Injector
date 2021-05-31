using System;
using System.Windows.Forms;

namespace LoadLibraryInjector.View
{
	public partial class UserForm : Form
	{
		public UserForm()
		{
			InitializeComponent();
			TopMost = true;
		}

		public int GetSelectedProcessIndex()
		{
			return ProcessListBox.SelectedIndex;
		}

		public void AddProcessListItem(string process)
		{
			ProcessListBox.Items.Add(process);
		}
	}
}
