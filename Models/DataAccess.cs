using WillCal.Models;
using System.Text.Json;

namespace WillCal.Models
{
    public class DataAccess
    {
        private static List<CalData> willcal = new List<CalData>();

        public DataAccess() {
            DataAccess.LoadData();
        //    CalData tmp = new CalData();
        //    tmp.LineNum = 1;
        //    tmp.EventName = "test";
        //    tmp.dtEvent = new DateTime(2022, 04, 30);
        //    willcal.Add(tmp);
            
        }

        public static string toJSON() {
            string JSONData = "";
            foreach(CalData c in  willcal) {
                JSONData += JsonSerializer.Serialize(c) + "\n";
            }
            return JSONData;
        }
        
        public static void fromJSON(string tmp) {
            string[] JSONData = tmp.Split("\n");
            willcal.Clear();
            foreach(string s in JSONData) {
                if(s.Length > 4) {
                    Console.WriteLine(s);
                    CalData? newCalData = JsonSerializer.Deserialize<CalData>(s)!;
                    if(newCalData != null)
                        willcal.Add(newCalData);
                }
            }
        }

        public static List<CalData> GetCalandar() {
            return willcal;
        }

        public static List<CalData> GetEventsByDate(DateTime dt) {
            List<CalData> results = new();
            var query = from cd in willcal
                where cd.dtEvent.ToShortDateString() == dt.ToShortDateString() && cd.Repeats == false
                select cd;
            foreach(var cd in query) {
                results.Add(cd);
            }
            query = from cd in willcal
                where cd.dtEvent.ToString("MMdd") == dt.ToString("MMdd") && cd.RepeatBlock == "year" && cd.Repeats == true
                && ((cd.RepeatTag == true && cd.dtEvent.Year <= dt.Year) || cd.RepeatTag == false)
                select cd;
            foreach(var cd in query) {
                if(cd.RepeatTag == true) {
                    var eyear = cd.dtEvent.Year;
                    var tyear = dt.Year;
                    string addition = eyear==tyear?"":" (" + (tyear - eyear) + ")";
                    cd.EventName = cd.EventName.Substring(0, (cd.EventName.IndexOf(" (")>=0?cd.EventName.IndexOf(" ("):cd.EventName.Length)) + addition;
                }
                results.Add(cd);
            }
            return results;
        }

        public static async void SaveData() {
            await File.WriteAllTextAsync("calendar.dat", toJSON());
        }

        public static void LoadData() {
            string tmp = File.ReadAllText("calendar.dat");
            fromJSON(tmp);
        }

        public static string AddEvent(CalData data) {
            willcal.Add(data);
            SaveData();
            ThisMonth.PopulateMonth();
            //foreach(string s in test)
            //Console.WriteLine(data.EventName);
            return "Privacy";
        }
    }
}