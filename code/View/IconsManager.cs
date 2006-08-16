using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace PTM.View
{
	public sealed class IconsManager
	{
		private IconsManager()
		{
		}

		private static ImageList iconsList = new ImageList();
		private static ArrayList iconsArray = new ArrayList();
		private static Hashtable iconsMapTable = new Hashtable();

		public static ImageList IconsList
		{
			get { return iconsList; }
		}


		public static Icon GetIcon(string key)
		{
			return (Icon)iconsArray[(int) iconsMapTable[key]];
		}

		public static int GetIndex(string key)
		{
			return (int) iconsMapTable[key];
		}

		public static int AddIcon(string key, Icon icon)
		{
			if (iconsMapTable.Contains(key))
				return (int) iconsMapTable[key];
			else
			{
				iconsList.Images.Add(icon);
				iconsArray.Add(icon);
				iconsMapTable.Add(key, iconsList.Images.Count - 1);
				return iconsList.Images.Count - 1;
			}
		}

		public static int AddIconFromFile(string fileName)
		{
			if (iconsMapTable.Contains(fileName))
				return (int) iconsMapTable[fileName];
			else
			{
				Icon icon = GetFileIcon(fileName);
				iconsList.Images.Add(icon);
				iconsArray.Add(icon);
				iconsMapTable.Add(fileName, iconsList.Images.Count - 1);
				return iconsList.Images.Count - 1;
			}
		}

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
			}
//			if(ri!= null) return ri;
//			ri = IconHandler.IconFromExtension(fileName, IconSize.Small);
//			if(ri!= null) return ri;
//			ri = IconHandler.IconFromExtensionShell(fileName, IconSize.Small);
//			if(ri!= null) return ri;
//			return null;
		}
	}
}