namespace WillCal.Models
{
    public class TaskData
    {
        public Guid guid { get; set; } = new Guid();
        public string TaskName { get; set; } = "";
        public DateTime TaskDT { get; set; }
        public bool TaskDone { get; set; } = false;
        public Guid? parentGuid { get; set; } = null;
        public List<Guid> childGuid { get; set; } = new List<Guid>();
        public bool Repeats { get; set; } = false;
        public string RepeatBlock { get; set; } = "day";
        public int RepeatFreq { get; set; } = 7;
        
    }
}