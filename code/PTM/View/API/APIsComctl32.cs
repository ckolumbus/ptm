namespace System.Runtime.InteropServices.APIs
{
	internal class APIsComctl32
	{
		#region GetMajorVersion

		internal static int GetMajorVersion()
		{
			APIsStructs.DLLVERSIONINFO2 pdvi = new APIsStructs.DLLVERSIONINFO2();
			pdvi.info1.cbSize = Marshal.SizeOf(typeof (APIsStructs.DLLVERSIONINFO2));
			DllGetVersion(ref pdvi);
			return pdvi.info1.dwMajorVersion;
		}

		#endregion

		#region DllGetVersion

		[DllImport("Comctl32.dll", CharSet=CharSet.Auto)]
		private static extern int DllGetVersion(
			ref APIsStructs.DLLVERSIONINFO2 pdvi);

		#endregion
	}
}