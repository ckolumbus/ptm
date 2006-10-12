/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections;
using System.Text;
using DayView;

namespace Calendar
{
    public class ResolveAppointmentsEventArgs : EventArgs
    {
        public ResolveAppointmentsEventArgs(DateTime start, DateTime end)
        {
            m_StartDate = start;
            m_EndDate = end;
        		//COMP2003: Not compatible
            //m_Appointments = new List<Appointment>();
        		m_Appointments = new Appointments();
        }

        private DateTime m_StartDate;

        public DateTime StartDate
        {
            get { return m_StartDate; }
            set { m_StartDate = value; }
        }

        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }
			
    	//COMP2003: Not compatible
       // private List<Appointment> m_Appointments;
    	 private Appointments m_Appointments;

    	//COMP2003: Not compatible
        //public List<Appointment> Appointments
    	public Appointments Appointments
        {
            get { return m_Appointments; }
            set { m_Appointments = value; }
        }
    }

    public delegate void ResolveAppointmentsEventHandler(object sender, ResolveAppointmentsEventArgs args);
}
