using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ZP.BHS.Zombie
{
    /// <summary>
    /// This class contains Zombie Data.
    /// </summary>
    public static class ZombieData// Add Attack Range.
    {
        static private string _dataPath = "Assets/Resources/Prefabs/BHS/ZombieDataCSV.csv";
        static public string DataString = File.ReadAllText(_dataPath);
        static public List<Dictionary<string, string>> ZombieDatas = ParseCsvFile(DataString);

        /// <summary>
        /// Returns Single Dictionary From Dictionary List By Using ZombieType.
        /// </summary>
        /// <param name="zombieType">Input Zombie Type To Find Data.</param>
        /// <returns></returns>
        static public Dictionary<string, string> CallDataByType(ZombieType zombieType)
        {
            Dictionary<string, string> outDic = new Dictionary<string,string>();

            for (int i = 0; i < ZombieDatas.Count; i++)
            {
                if (ZombieDatas[i]["ZombieType"] == zombieType.ToString())
                {
                    outDic = ZombieDatas[i];
                }
            }
            return outDic;
        }

        /// <summary>
        /// Return List of Dictionary By Using csv Style string.
        /// </summary>
        /// <param name="csvContent">String Want to Parse</param>
        /// <returns></returns>
        private static List<Dictionary<string, string>> ParseCsvFile(string csvContent)
        {
            List<Dictionary<string, string>> outdata = new List<Dictionary<string, string>>();

            // 줄 단위로 분리.
            string[] lines = csvContent.Split('\n');

            // 첫 열을 키로 사용.
            if (lines.Length <= 1) { return null; } // 데이터가 없을 경우 빈 리스트를 반환.

            // 첫 열을 쉼표로 나누어 키로 저장.
            string[] headers = lines[0].Split(',');

            // 각 줄마다 데이터를 쪼개고 저장.
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue; // 빈 줄은 건너뜁니다.

                // 쉼표로 필드를 구분. 큰따옴표 안의 쉼표는 무시.
                string[] fields = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                Dictionary<string, string> entry = new Dictionary<string, string>();

                for (int j = 0; j < headers.Length; j++)
                {
                    // 키는 헤더 이름, 값은 해당 필드의 값.
                    // 각 값의 큰따옴표를 제거하고, 공백을 제거.
                    if (j == fields.Length)
                    {
                        break;
                    }
                    entry[headers[j].Trim()] = fields[j].Trim('"').Trim();
                }
                outdata.Add(entry);
            }
            return outdata;
        }
    }

    /// <summary>
    /// Zombie Type.
    /// </summary>
    public enum ZombieType
    {
        WalkerZombie = 0,
        RunnerZombie = 1,
        CrawlerZombie = 2,
    }
}
