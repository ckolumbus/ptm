using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

namespace PTM.View
{
	internal sealed class IconsManager
	{
		private IconsManager()
		{
		} //IconsManager

		private static ImageList iconsList = new ImageList();
		//private static ArrayList iconsArray = new ArrayList();
		private static Hashtable iconsCommonTasks = new Hashtable();

		internal static ImageList IconsList
		{
			get { return iconsList; }
		} //IconsList

		internal static Icon GetCommonTaskIcon(int iconId)
		{
			return (Icon) iconsCommonTasks[iconId];
		}
		
		internal static int AddIconFromFile(string fileName)
		{
			if (iconsCommonTasks.Contains(fileName))
			{
				return (int) iconsCommonTasks[fileName];
			}
			else
			{
				Icon icon = GetFileIcon(fileName);
				iconsList.Images.Add(icon);
				iconsCommonTasks.Add(fileName, iconsList.Images.Count - 1);
				return iconsList.Images.Count - 1;
			} //if-else
		} //AddIconFromFile

		private static Icon GetFileIcon(string fileName)
		{
			//Icon ri;
			try
			{
				return IconHelper.GetFileIcon(fileName, IconHelper.IconSize.Small, false);
			}
			catch
			{
				return IconHelper.GetFileIcon(Application.ExecutablePath, IconHelper.IconSize.Small, false);
			} //try-catch
//			if(ri!= null) return ri;
//			ri = IconHandler.IconFromExtension(fileName, IconSize.Small);
//			if(ri!= null) return ri;
//			ri = IconHandler.IconFromExtensionShell(fileName, IconSize.Small);
//			if(ri!= null) return ri;
//			return null;
		} //GetFileIcon
		
		
		private static void LoadIconsFromResources()
		{
			
			ResourceManager resourceManager = new ResourceManager("PTM.View.Controls.Icons", System.Reflection.Assembly.GetExecutingAssembly());

			Icon resIcon;
			int i = 1;
			do
			{
				resIcon = (Icon) resourceManager.GetObject(
					"Icon" + i.ToString(CultureInfo.InvariantCulture));
				if (resIcon != null)
				{				
					iconsList.Images.Add(resIcon);
					iconsCommonTasks.Add(i - 1, resIcon);
				} //if
				i++;
			} while (resIcon != null);
		} //LoadIconsFromResources
		public static void Initialize()
		{
			LoadIconsFromResources();
		}
	} //end of class IconsManager
} //end of namespace