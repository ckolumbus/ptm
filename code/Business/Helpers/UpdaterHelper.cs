using System;
using System.Net;
using System.Xml;

namespace PTM.Business.Helpers
{
	/// <summary>
	/// Summary description for UpdaterHelper.
	/// </summary>
	public sealed class UpdaterHelper
	{
		private UpdaterHelper()
		{
		}
		
		public void CheckFromUpdates()
		{
			string thisVersion = ConfigurationHelper.GetInternalVersionString();
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(@"http://svn.sourceforge.net/viewvc/*checkout*/ptm/trunk/info.xml");
				string curVersion = doc.SelectSingleNode(@"CurrentVersion").InnerText;				
			}
			catch(WebException)
			{
				//do nothing
			}
			
		}
	}
}
