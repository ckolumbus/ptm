using System.Diagnostics;

namespace PTM.View
{
	public class Logger
	{
		private Logger()
		{
		}

		public static void Write(string message)
		{
			Debug.WriteLine(message);
		}
	}
}
