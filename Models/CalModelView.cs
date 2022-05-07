namespace WillCal.Models
{
    public class CalModelView
    {
        public static string[,] calText { get; set; } = new string[42,2];
        public static string titleText { get; set; } = "";
        public static string thisMonth { get; set; } = "";
        public static string nextMonth { get; set; } = "";
        public static string prevMonth { get; set; } = "";
        public static string thisWeek { get; set; } = "";
        public static string nextWeek { get; set; } = "";
        public static string prevWeek { get; set; } = "";
        public static string currDate { get; set; } = "";
        public static List<TaskData> tasks = new List<TaskData>();
    }
}