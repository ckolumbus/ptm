/* Developed by Ertan Tike (ertan.tike@moreum.com) */

using System;
using System.Collections;
using System.Text;
using System.Drawing;

namespace Calendar
{
    public class Appointment
    {
        public Appointment()
        {
            color = Color.White;
            m_BorderColor = Color.Blue;
            m_Title = "New Appointment";
        }

        private DateTime m_StartDate;

        public DateTime StartDate
        {
            get
            {
                return m_StartDate;
            }
            set
            {
                m_StartDate = value;
                OnStartDateChanged();

            }
        }
        protected virtual void OnStartDateChanged()
        {
        }

        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get
            {
                return m_EndDate;
            }
            set
            {
                m_EndDate = value;
                OnEndDateChanged();
            }
        }
        protected virtual void OnEndDateChanged()
        {
        }

        private bool m_Locked = false;

        [System.ComponentModel.DefaultValue(false)]
        public bool Locked
        {
            get { return m_Locked; }
            set
            {
                m_Locked = value;
                OnLockedChanged();
            }
        }

        protected virtual void OnLockedChanged()
        {
        }

        private Color color = Color.White;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private Color textColor = Color.Black;

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        private Color m_BorderColor = Color.Blue;

        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
            }
        }

        private string m_Title = "";

        [System.ComponentModel.DefaultValue("")]
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                OnTitleChanged();
            }
        }

        private object tag;
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        protected virtual void OnTitleChanged()
        {
        }

        //internal int m_ConflictCount;
    	
    	 public Rectangle Rectangle;
    }
}
