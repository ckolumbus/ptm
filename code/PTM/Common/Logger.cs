using System;
using System.Diagnostics;

namespace PTM.Common
{
	public class Logger
	{
		private Logger()
		{
		} //Logger

        public static void WriteMessage(string message)
		{
			Debug.WriteLine("PTM:" + message);
			Console.WriteLine("PTM:" + message);
		} //Write
        
        public static  void WriteException(Exception ex)
        {
            WriteMessage("--->EXCEPTION");
            WriteMessage("Message: " + ex.Message);
            WriteMessage("StackTrace: " + ex.StackTrace);
            if(ex.InnerException!=null)
            {
                WriteMessage("--->INNER EXCEPTION");
                WriteMessage("Message: " + ex.InnerException.Message);
                WriteMessage("StackTrace: " + ex.InnerException.StackTrace);
            }            
        }
	} // end of class Logger
} //end of namespace