namespace WillCal.Models
{
    public class ThisWeek
    {
        public static DateTime Today { get; set; } = DateTime.Today;
        private static DateTime Fdow { get; set; }
        private static DateTime LastWeek { get; set; }
        private static DateTime NextWeek { get; set; }
        private static int WeekNum { get; set; }
        //private static string[,] weekText { get; set; } = new string[10,2];
        
        public ThisWeek() {
            InitDates();
        }

        private static void InitDates() {
            Fdow = Today.Subtract(new TimeSpan((int)Today.DayOfWeek, 0, 0, 0));
            LastWeek = Fdow.Add(new TimeSpan(7, 0, 0, 0));
            NextWeek = Fdow.Subtract(new TimeSpan(7, 0, 0, 0));
            DateTime dt = new DateTime(Today.Year, 1, 1);
            dt = dt.Subtract(new TimeSpan((int)dt.DayOfWeek, 0, 0, 0));
            TimeSpan ts = Fdow.Subtract(dt);
            WeekNum = (int)(ts.Days / 7) + 1;
            PopulateWeek();
        }

        public static void PopulateWeek() {
            DateTime startDate = new DateTime(Fdow.Ticks);
            for(int i = 0; i < 7; i++) {
                List<CalData> thisDayEvents = DataAccess.GetEventsByDate(startDate);
                CalModelView.calText[i,0] = startDate.ToString("dd") + " - ";
                if(CalModelView.calText[i,0].Substring(0,1) == "0") CalModelView.calText[i,0] = CalModelView.calText[i,0].Substring(1);
                CalModelView.calText[i,1] = startDate.ToString("yyyyMMdd");
                foreach(CalData cd in thisDayEvents) {
                    CalModelView.calText[i,0] += cd.EventName + " / ";
                }
                CalModelView.calText[i,0] = CalModelView.calText[i,0].Substring(0, CalModelView.calText[i,0].Length - 3);
                startDate = startDate.Add(new TimeSpan(1,0,0,0));
            }
            CalModelView.titleText = Today.ToString("Y");
            CalModelView.thisMonth = Today.ToString("yyyMM");
            CalModelView.prevWeek = LastWeek.ToString("yyyy") + (WeekNum - 1).ToString("00");
            CalModelView.nextWeek = NextWeek.ToString("yyyy") + (WeekNum + 1).ToString("00");
            CalModelView.currDate = Today.ToString("yyyyMMdd");
        }

        public static void CalByWeek(string? newWeek) {
            int test = 0;
            if(newWeek != null && newWeek.Length == 6 && Int32.TryParse(newWeek, out test)) {
                int year = Int32.Parse(newWeek.Substring(0,4));
                int week = Int32.Parse(newWeek.Substring(4,2));
                Today = new DateTime(year, 1, 1);
                Today = Today.Subtract(new TimeSpan((int)Today.DayOfWeek, 0, 0, 0));
                Today = Today.Add(new TimeSpan((week - 1) * 7, 0, 0, 0));
                InitDates();
            } else {
                Today = DateTime.Today;
                InitDates();
            }
        }
        
        public static void GetWeekText() {
            return;
        }
    }
}