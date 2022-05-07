namespace WillCal.Models
{
    public class ThisMonth
    {
        public static DateTime thisMonth = DateTime.Today;
        public static DateTime nextMonth;
        public static DateTime prevMonth;
        public static DateTime currdate = DateTime.Today;
        public static DataAccess da = new DataAccess();
        public static string[] dowNames = new string[] {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"};
        public static string[] monthNames = new string[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        public static int[] lastDayOfMonth = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        public ThisMonth() {
            thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            nextMonth = thisMonth.AddMonths(1);
            prevMonth = thisMonth.AddMonths(-1);
            PopulateMonth();
        }

        public static void GoNext() {
            CalByMonth(nextMonth.Year.ToString() + nextMonth.Month.ToString());
        }

        public static void GoPrev() {
            CalByMonth(prevMonth.Year.ToString() + prevMonth.Month.ToString());
        }

        public static void CalByMonth(string? newDate) {
            int test = 0;
            if(newDate != null && Int32.TryParse(newDate, out test)) {
                if(newDate.Length == 6 || newDate.Length == 8) {
                    thisMonth = new DateTime(Int32.Parse(newDate.Substring(0,4)), Int32.Parse(newDate.Substring(4,2)), 1);
                    nextMonth = thisMonth.AddMonths(1);
                    prevMonth = thisMonth.AddMonths(-1);
                    PopulateMonth();
                    if(newDate.Length == 8) {
                        currdate = new DateTime(Int32.Parse(newDate.Substring(0,4)), Int32.Parse(newDate.Substring(4,2)), Int32.Parse(newDate.Substring(6,2)));
                    }
                }
            } else {
                thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                nextMonth = thisMonth.AddMonths(1);
                prevMonth = thisMonth.AddMonths(-1);
                PopulateMonth();
            }
        }

        public static void PopulateMonth() {
            int ldolm = LastDayOfMonth(prevMonth);
            int ldotm = LastDayOfMonth(thisMonth);
            int fdotm = (int)thisMonth.DayOfWeek;
            int startNumber = thisMonth.Day - fdotm + (fdotm > 0 ? ldolm : 0);
            DateTime startDate = (new DateTime(thisMonth.Year, thisMonth.Month, 1)) - new TimeSpan(fdotm, 0, 0, 0);
            for(int i = 0; i < 42; i++) {
                List<CalData> thisDayEvents = DataAccess.GetEventsByDate(startDate);
                CalModelView.calText[i,0] = startNumber++.ToString() + " - ";
                CalModelView.calText[i,1] = startDate.ToString("yyyyMMdd");
                foreach(CalData cd in thisDayEvents) {
                    CalModelView.calText[i,0] += cd.EventName + " / ";
                }
                CalModelView.calText[i,0] = CalModelView.calText[i,0].Substring(0, CalModelView.calText[i,0].Length - 3);
                startDate = startDate.Add(new TimeSpan(1,0,0,0));
                if(startNumber > ldolm && i < 10)
                    startNumber = 1;
                if(startNumber > ldotm && i > 10)
                    startNumber = 1;
            }
            CalModelView.titleText = thisMonth.ToString("Y");
            CalModelView.thisMonth = thisMonth.ToString("yyyyMM");
            CalModelView.prevMonth = prevMonth.ToString("yyyyMM");
            CalModelView.nextMonth = nextMonth.ToString("yyyyMM");
            CalModelView.currDate = currdate.ToString("yyyyMMdd");
            TaskAccess.LoadData();
        }

        public static int LastDayOfMonth(DateTime dateToCheck) {
            if(dateToCheck.Month != 2)
                return lastDayOfMonth[dateToCheck.Month - 1];
            DateTime tmp = (new DateTime(dateToCheck.Year, 3, 1)).Subtract(new TimeSpan(1, 0, 0, 0));
            return tmp.Day;
        }

        public static void getMonthText() {
            return;
        }
    }
}