using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using PTM.Addin;
using PTM.Data;
using PTM.View;

namespace PTM.Framework.Helpers
{
	/// <summary>
	/// Descripción breve de AddinHelper.
	/// </summary>
	public class AddinHelper
	{
		public AddinHelper()
		{
		}
		
		public static void AddAddinAssembly(string path)
		{
			if(path.Length>255)
				throw new ApplicationException("The length from an addin path cant be greater than 255");
			if(GetAddinDescription(path).Length==0)
				throw new ApplicationException("No add-in types found.");
			DbHelper.ExecuteNonQuery("INSERT INTO Addins (path) values (?)", new string[] {"Path"},
			                         new object[] {path});
		}
		
		public static string GetAddinDescription(string path)
		{
			StringBuilder sb = new StringBuilder();
			try
			{
				Assembly addinAssembly = System.Reflection.Assembly.LoadFile(path);
				
				Type[] addinTypes;
				addinTypes = addinAssembly.GetTypes();
		
				foreach (Type addinType in addinTypes)
				{
					if(addinType.IsSubclassOf(typeof(TabPageAddin)))
					{
						TabPageAddin tabPageAddin = (TabPageAddin) addinAssembly.CreateInstance(addinType.ToString());
						if(sb.Length!=0)
							sb.Append(" , ");
						sb.Append(tabPageAddin.Text);
					}
				}
			}
			catch(Exception ex)
			{
				Logger.Write("Error loading the addin from " + path);
				Logger.Write(ex.Message);
				return "Loading Error!";
			}
			return sb.ToString();
		}
		
		public static void DeleteAddinAssembly(string path)
		{
			DbHelper.ExecuteNonQuery("DELETE FROM Addins WHERE path = ?", new string[] {"Path"},
				new object[] {path});
		}
		
		public static ArrayList GetAddins()
		{
			ArrayList addinsList = new ArrayList();
			ArrayList addins = DbHelper.ExecuteGetRows("Select Path from Addins");
			foreach (Hashtable addin in addins)
			{
				string path = addin["Path"].ToString();
				addinsList.Add(path);
			}
			return addinsList;
		}
		
		public static ArrayList GetTabPageAddins()
		{
			ArrayList tabPageAddins = new ArrayList();
			ArrayList addins = GetAddins();
			foreach (string path in addins)
			{
				Assembly addinAssembly;
				try
				{
					addinAssembly = System.Reflection.Assembly.LoadFile(path);
					Type[] addinTypes;
					addinTypes = addinAssembly.GetTypes();
				
					foreach (Type addinType in addinTypes)
					{
						if(addinType.IsSubclassOf(typeof(TabPageAddin)))
						{
							TabPageAddin tabPageAddin = (TabPageAddin) addinAssembly.CreateInstance(addinType.ToString());
							tabPageAddins.Add(tabPageAddin);
						}
					}
				}
				catch(Exception ex)
				{
					Logger.Write("Error loading the addin from " + path);
					Logger.Write(ex.Message);
					//MessageBox.Show("Error loading the addin from " + path, "PTM " + ConfigurationHelper.GetVersionString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					continue;
				}
			}
			return tabPageAddins;
		}
	}
}
