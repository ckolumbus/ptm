using System.Diagnostics;

namespace PTM.View
{
	public class Logger
	{
		private Logger()
		{
		} //Logger

		public static void Write(string message)
		{
			Debug.WriteLine(message);
		} //Write
	} // end of class Logger
} //end of namespace