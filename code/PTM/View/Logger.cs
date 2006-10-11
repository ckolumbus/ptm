using System.Diagnostics;

namespace PTM.View
{
	internal class Logger
	{
		private Logger()
		{
		} //Logger

		internal static void Write(string message)
		{
			Debug.WriteLine(message);
		} //Write
	} // end of class Logger
} //end of namespace