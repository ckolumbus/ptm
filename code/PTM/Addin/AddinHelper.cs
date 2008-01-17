using System;
using System.Collections;
using System.Reflection;
using System.Text;
using PTM.Addin;
using PTM.Common;
using PTM.Data;

namespace PTM.Addin
{
	internal class AddinHelper
	{
		private AddinHelper()
		{
		}

        internal static void AddAddinAssembly(string path)
		{
			if (path.Length > 255)
				throw new ApplicationException("The length from an addin path can't be greater than 255");
			if (GetAddinDescription(path) == null)
				throw new ApplicationException("No add-in types found.");
			DbHelper.ExecuteNonQuery("INSERT INTO Addins (path) values (?)", new string[] {"Path"},
			                         new object[] {path});
		}

        internal static void DeleteAddinAssembly(string path)
		{
			DbHelper.ExecuteNonQuery("DELETE FROM Addins WHERE path = ?", new string[] {"Path"},
			                         new object[] {path});
		}

        internal static string[] GetConfiguredAddinsPaths()
		{
			ArrayList addinsList = new ArrayList();
			ArrayList addins = DbHelper.ExecuteGetRows("Select Path from Addins");
			foreach (IDictionary addin in addins)
			{
				string path = addin["Path"].ToString();
				addinsList.Add(path);
			}
			return (string[]) addinsList.ToArray(typeof(string));
		}

        internal static string GetAddinDescription(string path)
        {
            try
            {
                string description = null;
                Assembly addinAssembly = Assembly.LoadFrom(path);
                Type[] addinTypes;
                addinTypes = addinAssembly.GetTypes();

                foreach (Type addinType in addinTypes)
                {
                    if (addinType.IsSubclassOf(typeof(AddinTabPage)))
                    {
                        AddinTabPage pageAddinTabPage = (AddinTabPage)addinAssembly.CreateInstance(addinType.ToString());
                        if (description != null && description != String.Empty)
                            description += " , ";
                        description += pageAddinTabPage.Text;
                    }
                }
                return description;
            }
            catch (Exception ex)
            {
                Logger.WriteMessage("Error loading the addin from " + path);
                Logger.WriteException(ex);
                return "Loading Error!";
            }
        }

        internal static ArrayList GetTabPageAddins()
		{
			ArrayList tabPageAddins = new ArrayList();
			string[] addinsPaths = GetConfiguredAddinsPaths();
            foreach (string path in addinsPaths)
			{
				try
				{
                    Assembly addinAssembly;
                    addinAssembly = Assembly.LoadFrom(path);
					Type[] addinTypes;
					addinTypes = addinAssembly.GetTypes();

					foreach (Type addinType in addinTypes)
					{
						if (addinType.IsSubclassOf(typeof (AddinTabPage)))
						{
							AddinTabPage pageAddinTabPage = (AddinTabPage) addinAssembly.CreateInstance(addinType.ToString());
							tabPageAddins.Add(pageAddinTabPage);
						}
					}
				}
				catch (Exception ex)
				{
					Logger.WriteMessage("Error loading the addin from " + path);
					Logger.WriteException(ex);
					continue;
				}
			}
			return tabPageAddins;
		}

	}
}