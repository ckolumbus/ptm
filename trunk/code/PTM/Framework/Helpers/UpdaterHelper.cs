using System;
using System.Xml;

namespace PTM.Framework.Helpers
{
	/// <summary>
	/// Summary description for UpdaterHelper.
	/// </summary>
	public sealed class UpdaterHelper
	{
		private UpdaterHelper()
		{
		}

//		[DllImport("WININET", CharSet=CharSet.Auto)]
//		static extern bool InternetGetConnectedState(
//			out InternetConnectionState lpdwFlags, 
//			int dwReserved);

//		[Flags]
//		enum InternetConnectionState: int
//		{
//			INTERNET_CONNECTION_MODEM = 0x1,
//			INTERNET_CONNECTION_LAN = 0x2,
//			INTERNET_CONNECTION_PROXY = 0x4,
//			INTERNET_RAS_INSTALLED = 0x10,
//			INTERNET_CONNECTION_OFFLINE = 0x20,
//			INTERNET_CONNECTION_CONFIGURED = 0x40
//		}


//		public static bool IsConnectedToInternet()
//		{
//			InternetConnectionState Desc;
//			return InternetGetConnectedState(out Desc, 0);
//		}


		public struct UpdateInfo
		{
			public string ThisVersion;
			public string CurrentVersion;
			public string ThisInternalVersion;
			public string CurrentInternalVersion;
			public bool UpdateAvailable;
		}

		public static UpdateInfo CheckFromUpdates()
		{
			UpdateInfo info = new UpdateInfo();
			info.ThisInternalVersion = ConfigurationHelper.GetInternalVersionString();
			info.ThisVersion = ConfigurationHelper.GetVersionString();
			info.UpdateAvailable = false;

			Configuration config;
			config = ConfigurationHelper.GetConfiguration(ConfigurationKey.CheckForUpdates);
			if (Convert.ToInt32(config.Value) == 0)
			{
				return info;
			}
			else if (Convert.ToInt32(config.Value) == 1)
			{
//				if (!IsConnectedToInternet())
//					return info;
				XmlDocument doc = new XmlDocument();
				try
				{

					doc.Load(@"http://ptm.sourceforge.net/info.xml");
					info.CurrentVersion = doc.SelectSingleNode(@"/root/LiteCurrentVersion").InnerText;
					info.CurrentInternalVersion = doc.SelectSingleNode(@"/root/LiteCurrentInternalVersion").InnerText;
					if (string.CompareOrdinal(info.CurrentInternalVersion, info.ThisInternalVersion) > 0)
					{
						info.UpdateAvailable = true;
					}
				}
				catch (Exception ex)
				{
					ex = ex; //do nothing
				}
			}
			return info;
		}
	}
}