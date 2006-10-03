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
		
		public struct UpdateInfo
		{
			public string ThisVersion;
			public string CurrentVersion;
			public string ThisInternalVersion;
			public string CurrentInternalVersion;
			public bool UpdateAvailable;
		}
		
		public UpdateInfo CheckFromUpdates()
		{
			UpdateInfo info = new UpdateInfo();
			info.ThisInternalVersion = ConfigurationHelper.GetInternalVersionString();
			info.ThisVersion = ConfigurationHelper.GetVersionString();
			info.UpdateAvailable = false;
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(@"http://svn.sourceforge.net/viewvc/*checkout*/ptm/trunk/info.xml");
				info.CurrentVersion = doc.SelectSingleNode(@"/root/CurrentVersion").InnerText;
				info.CurrentInternalVersion = doc.SelectSingleNode(@"/root/CurrentInternalVersion").InnerText;
				if(string.CompareOrdinal(info.CurrentInternalVersion, info.ThisInternalVersion)> 0)
				{
					info.UpdateAvailable = true;
				}
			}
			catch(Exception ex)
			{
				ex = ex;//do nothing
			}
			return info;
		}
	}
}
