using System;
using System.Collections;
using Calendar;

namespace DayView
{
	/// <summary>
	/// Summary description for Appointments.
	/// </summary>
	public class Appointments : CollectionBase
	{
		public Appointments()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add(Appointment appointment)
		{
			this.List.Add(appointment);
		}

		public int IndexOf(Appointment appointment)
		{
			return this.List.IndexOf(appointment);
		}

		public bool Contains(Appointment appointment)
		{
			return this.List.Contains(appointment);
		}
	}
}
