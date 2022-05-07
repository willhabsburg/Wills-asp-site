using System.Text.Json;

namespace WillCal.Models
{
    public class TaskAccess
    {
        private static Guid currentTask { get; set; } = Guid.Empty;

        public TaskAccess() {
        }

        public static string toJSON() {
            string JSONData = "";
            foreach(TaskData c in  CalModelView.tasks) {
                JSONData += JsonSerializer.Serialize(c) + "\n";
            }
            return JSONData;
        }
        
        public static void fromJSON(string tmp) {
            string[] JSONData = tmp.Split("\n");
            CalModelView.tasks.Clear();
            foreach(string s in JSONData) {
                if(s.Length > 4) {
                    TaskData? newTaskData = JsonSerializer.Deserialize<TaskData>(s)!;
                    if(newTaskData != null)
                        CalModelView.tasks.Add(newTaskData);
                }
            }
        }

        public static string[] getCurrentTask() {
            string[] tmp = new string[9];
            if(currentTask != Guid.Empty) {
                foreach(TaskData td in CalModelView.tasks) {
                    if(td.guid.Equals(currentTask)) {
                        tmp[0] = td.guid.ToString();
                        tmp[1] = td.TaskName;
                        tmp[2] = td.TaskDT.ToString();
                        tmp[3] = td.TaskDone.ToString();
                        tmp[4] = td.parentGuid.ToString()??"";
                        tmp[5] = "";
                        tmp[6] = td.Repeats.ToString();
                        tmp[7] = td.RepeatBlock;
                        tmp[8] = td.RepeatFreq.ToString();
                    }
                }
            } else {
                tmp[2] = DateTime.Now.ToString("yyyy-MM-ddT23:59");
            }
            return tmp;
        }

        public static async void SaveData() {
            await File.WriteAllTextAsync("tasks.dat", toJSON());
        }

        public static void LoadData() {
            if(!File.Exists("tasks.dat")) {
                TaskData td = new TaskData();
                td.TaskName = "test task";
                td.TaskDT = DateTime.Today;
                CalModelView.tasks.Add(td);
                SaveData();
            }
            string tmp = File.ReadAllText("tasks.dat");
            fromJSON(tmp);
        }

        public static void AddEvent(TaskData data) {
            Console.WriteLine(data.TaskName);
            CalModelView.tasks.Add(data);
            SaveData();
        }
    }
}