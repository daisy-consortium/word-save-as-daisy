using System;
using System.Windows.Forms;

namespace DaisyWord2007AddIn
{
	public interface IPluginEventsHandler
	{
		void OnStop(string message);
		bool AskForTranslatingSubdocuments();
		void OnError(string errorMessage);
		void OnStop(string message, string title);
	}

	public class PluginEventsQuiteHandler : IPluginEventsHandler
	{
		public void OnStop(string message)
		{
			Console.WriteLine("ERROR : "  + message);
		}

		public bool AskForTranslatingSubdocuments()
		{
			return false;
		}

		public void OnError(string errorMessage)
		{
			Console.WriteLine("ERROR : " + errorMessage);
		}

		public void OnStop(string message, string title)
		{
			OnStop(message);
		}
	}

	public class PluginEventsUIHandler : IPluginEventsHandler
	{
		public void OnStop(string message)
		{
			OnStop(message,"SaveAsDAISY");
		}

		public bool AskForTranslatingSubdocuments()
		{
			DialogResult dialogResult = MessageBox.Show("Do you want to translate the current document along with sub documents?", "SaveAsDAISY", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			return dialogResult == DialogResult.Yes;
		}

		public void OnError(string errorMessage)
		{
			MessageBox.Show(errorMessage, "SaveAsDAISY", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void OnStop(string message, string title)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}
	}
}