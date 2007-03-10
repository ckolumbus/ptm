using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace PTM.View
{
	internal sealed class IconsManager
	{
		private IconsManager()
		{
		} //IconsManager

		public const int DefaultTaskIconId = 1;
		public const int IdleTaskIconId = 0;
		private static ImageList iconsList = new ImageList();
		private static Hashtable iconsCommonTasks = new Hashtable();

		internal static ImageList IconsList
		{
			get { return iconsList; }
		} //IconsList

		internal static Hashtable CommonTaskIconsTable
		{
			get { return (Hashtable) iconsCommonTasks.Clone(); }
		}

		private static void LoadIconsFromResources()
		{
			ResourceManager resourceManager = new ResourceManager("PTM.View.Controls.Icons", Assembly.GetExecutingAssembly());

			Icon resIcon;
			int i = 0;
			do
			{
				resIcon = (Icon) resourceManager.GetObject(
				                 	"Icon" + i.ToString(CultureInfo.InvariantCulture));
				if (resIcon != null)
				{
					iconsList.Images.Add(resIcon);
					iconsCommonTasks.Add(i, resIcon);
				} //if
				i++;
			} while (resIcon != null);

		} //LoadIconsFromResources
		public static void Initialize()
		{
			LoadIconsFromResources();
		}

/*
		internal static int GetIconFromFile(string fileName)
		{
			if (iconsCommonTasks.Contains(fileName))
			{
				return (int) iconsCommonTasks[fileName];
			}
			else
			{
				Icon icon = GetFileIcon(fileName);
				iconsList.Images.Add(icon);
				//iconsCommonTasks.Add(fileName, iconsList.Images.Count - 1);
				return iconsList.Images.Count - 1;
			} //if-else
		} //GetIconFromFile
*/
		/*
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
*/

	} //end of class IconsManager
} //end of namespace