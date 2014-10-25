using System;
using System.Collections.Generic;
using MyHobby.Models;

namespace MyHobby.ApiModels
{
    public class SessionDTO
    {
        private Session _session;
        private int _regCount;

        public SessionDTO(Session session)
        {
            _session = session;
        }

        public int Id { get { return _session.Id; } }
        public DateTime StartDate { get { return _session.StartDate; } }
        public DateTime EndDate { get { return _session.EndDate; } }
        public DateTime Deadline { get { return _session.Deadline; } }
        public double Cost { get { return _session.Cost; } }
        public string CostNotes { get { return _session.CostNotes; } }
        public int MaxStudents { get { return _session.MaxStudents; } }

        public int RegCount 
        {
            get             
            {
                if (_session.Registrations != null)
                {
                    return _session.Registrations.Count;
                }

                return _regCount;
            }
            set
            {
                _regCount = value;
            }
        }

        public string Duration
        {
            get
            {
                TimeSpan duration = _session.EndDate - _session.StartDate;
                if (duration.Days > 0)
                {
                    return duration.Days + "days"; // 3days
                }
                else if (duration.Hours > 0)
                {
                    string durStr = duration.Hours.ToString();
                    int mins = duration.Minutes % 60;
                    if (mins == 30)
                    {
                        durStr += ".5 hrs"; // 1.5hr
                    }
                    else if (mins > 0)
                    {
                        durStr += "h" + mins + "m"; // 1h30m
                    }
                    else if (mins == 0)
                    {
                        durStr += duration.Hours == 1 ? "hr" : "hrs";
                    }

                    return durStr;
                }
                else if (duration.Minutes > 0)
                {
                    return duration.Minutes + "mins";
                }

                return string.Empty;
            }
        }

        public List<RegistrationDTO> Registrations
        {
            get
            {
                return _session.Registrations.ConvertAll(r => new RegistrationDTO(r));
            }
        }
    }
}