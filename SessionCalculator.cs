using System;

namespace SessionVolumeProfile.Sessions
{
    public enum SessionType
    {
        Daily,
        Weekly,
        Monthly,
        Asia,
        London,
        NewYork
    }

    public class SessionInfo
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SessionType Type { get; set; }

        public bool Contains(DateTime time)
        {
            return time >= Start && time < End;
        }
    }

    public static class SessionCalculator
    {
        public static SessionInfo GetSession(DateTime serverTime, SessionType type)
        {
            switch (type)
            {
                case SessionType.Daily:
                    return Daily(serverTime);

                case SessionType.Weekly:
                    return Weekly(serverTime);

                case SessionType.Monthly:
                    return Monthly(serverTime);

                case SessionType.Asia:
                    return Asia(serverTime);

                case SessionType.London:
                    return London(serverTime);

                case SessionType.NewYork:
                    return NewYork(serverTime);

                default:
                    return Daily(serverTime);
            }
        }

        private static SessionInfo Daily(DateTime t)
        {
            var start = t.Date;

            return new SessionInfo
            {
                Type = SessionType.Daily,
                Start = start,
                End = start.AddDays(1)
            };
        }

        private static SessionInfo Weekly(DateTime t)
        {
            int diff = (7 + ((int)t.DayOfWeek - (int)DayOfWeek.Monday)) % 7;

            var start = t.Date.AddDays(-diff);

            return new SessionInfo
            {
                Type = SessionType.Weekly,
                Start = start,
                End = start.AddDays(7)
            };
        }

        private static SessionInfo Monthly(DateTime t)
        {
            var start = new DateTime(t.Year, t.Month, 1);

            return new SessionInfo
            {
                Type = SessionType.Monthly,
                Start = start,
                End = start.AddMonths(1)
            };
        }

        private static SessionInfo Asia(DateTime t)
        {
            var start = t.Date.AddHours(0);
            var end = t.Date.AddHours(9);

            if (t < start)
            {
                start = start.AddDays(-1);
                end = end.AddDays(-1);
            }

            return new SessionInfo
            {
                Type = SessionType.Asia,
                Start = start,
                End = end
            };
        }

        private static SessionInfo London(DateTime t)
        {
            var start = t.Date.AddHours(8);
            var end = t.Date.AddHours(17);

            if (t < start)
            {
                start = start.AddDays(-1);
                end = end.AddDays(-1);
            }

            return new SessionInfo
            {
                Type = SessionType.London,
                Start = start,
                End = end
            };
        }

        private static SessionInfo NewYork(DateTime t)
        {
            var start = t.Date.AddHours(13);
            var end = t.Date.AddHours(22);

            if (t < start)
            {
                start = start.AddDays(-1);
                end = end.AddDays(-1);
            }

            return new SessionInfo
            {
                Type = SessionType.NewYork,
                Start = start,
                End = end
            };
        }
    }
}
