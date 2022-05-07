namespace WillCal.Models
{
    public class CalData
    {
        public int LineNum { get; set; }
        public DateTime dtEvent { get; set; }
        public string EventName { get; set; } = "";
        public bool Repeats { get; set; } = false;
        public string RepeatBlock { get; set; } = "day";
        public int RepeatFreq { get; set; } = 1;
        public bool RepeatTag { get; set; } = true;
        public bool Alarm { get; set; } = false;
        public DateTime? dtAlarm { get; set; } = null;
        
    }
}