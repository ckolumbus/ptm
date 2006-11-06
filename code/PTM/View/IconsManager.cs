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
		private static ArrayList iconsArray = new ArrayList();
		private static Hashtable iconsMapTable = new Hashtable();

		internal static ImageList IconsList
		{
			get { return iconsList; }
		} //IconsList


		internal static Icon GetIcon(string key)
		{
			return (Icon) iconsArray[(int) iconsMapTable[key]];
		} //GetIcon

		internal static int GetIndex(string key)
		{
			return (int) iconsMapTable[key];
		} //GetIndex

		private static int AddIcon(string key, Icon icon)
		{
			if (iconsMapTable.Contains(key))
			{
				return (int) iconsMapTable[key];
			}
			else
			{
				iconsList.Images.Add(icon);
				iconsArray.Add(icon);
				iconsMapTable.Add(key, iconsList.Images.Count - 1);
				return iconsList.Images.Count - 1;
			} //if-else
		} //AddIcon

		internal static int AddIconFromFile(string fileName)
		{
			if (iconsMapTable.Contains(fileName))
			{
				return (int) iconsMapTable[fileName];
			}
			else
			{
				Icon icon = GetFileIcon(fileName);
				iconsList.Images.Add(icon);
				iconsArray.Add(icon);
				iconsMapTable.Add(fileName, iconsList.Images.Count - 1);
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
					IconsManager.AddIcon(
						(i - 1).ToString(CultureInfo.InvariantCulture), resIcon);
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