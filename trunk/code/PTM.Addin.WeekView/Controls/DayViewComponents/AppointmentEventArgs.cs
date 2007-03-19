/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections;
using System.Text;

namespace Calendar
{
    public class AppointmentEventArgs : EventArgs
    {
        public AppointmentEventArgs( Appointment appointment )
        {
            m_Appointment = appointment;
        }

        private Appointment m_Appointment;

        public Appointment Appointment
        {
            get { return m_Appointment; }
        }

    }
}
