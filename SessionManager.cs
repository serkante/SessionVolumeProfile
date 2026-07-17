using System;
using System.Collections.Generic;
using System.Linq;
using cAlgo.API;

namespace SessionVolumeProfile.Sessions
{
    public class SessionManager
    {
        private readonly List<SessionProfile> _sessions;

        public IReadOnlyList<SessionProfile> Sessions => _sessions;

        public int MaximumSessions { get; set; } = 50;

        public SessionType SessionType { get; set; }

        public SessionManager(SessionType type)
        {
            SessionType = type;
            _sessions = new List<SessionProfile>();
        }

        public void Clear()
        {
            _sessions.Clear();
        }

        public SessionProfile Current
        {
            get
            {
                if (_sessions.Count == 0)
                    return null;

                return _sessions[_sessions.Count - 1];
            }
        }

        public void Update(Bars bars, VolumeProfileEngine engine)
        {
            if (bars.Count == 0)
                return;

            var lastTime = bars.OpenTimes.LastValue;

            var info = SessionCalculator.GetSession(lastTime, SessionType);

            if (Current == null)
            {
                CreateSession(info);
            }
            else
            {
                if (Current.Info.Start != info.Start)
                    CreateSession(info);
            }

            Current.LastBarIndex = bars.Count - 1;
        }

        private void CreateSession(SessionInfo info)
        {
            var profile = new SessionProfile
            {
                Info = info,
                Engine = new VolumeProfileEngine(SymbolTickSize)
            };

            _sessions.Add(profile);

            while (_sessions.Count > MaximumSessions)
                _sessions.RemoveAt(0);
        }

        /// <summary>
        /// TickSize ana indikatör tarafından atanacaktır.
        /// </summary>
        public double SymbolTickSize { get; set; }
    }
}
