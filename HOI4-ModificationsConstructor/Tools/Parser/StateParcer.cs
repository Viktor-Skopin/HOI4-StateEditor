using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HOI4_ModificationsConstructor
{
    public class StateParcer
    {
        public State ParseState(string text)
        {
            State state = new State();

            string[] firstCollectionBuffer;
            string[] secondCollectionBuffer;
            string[] thirtCollectionBuffer;

            text = Regex.Replace(text, @"#.*$", string.Empty, RegexOptions.Multiline);//Удаление комментариев

            text = Regex.Replace(text, @"^[{}s]{1}.*$", string.Empty, RegexOptions.Multiline);//Удаление первого уровня
            text = Regex.Replace(text, @"^[\s]{1}", string.Empty, RegexOptions.Multiline); // удаление табуляций
            text = Regex.Replace(text, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline); // удаление пустых строк

            state.ID.Value = int.Parse(Regex.Match(text, @"^id\s*=\s*(\d*).*$", RegexOptions.Multiline).Groups[1].Value);//ID
            text = Regex.Replace(text, @"^id\s*=\s*(\d*).*$", string.Empty, RegexOptions.Multiline);//Вырезание ID

            state.Name.Value = Regex.Match(text, @"^name\s*=\s*""(\w*)"".*$", RegexOptions.Multiline).Groups[1].Value;//Name
            text = Regex.Replace(text, @"^name\s*=\s*""(\w*)"".*$", string.Empty, RegexOptions.Multiline);//Вырезание Name

            state.Manpower.Value = int.Parse(Regex.Match(text, @"^manpower\s*=\s*(\d*).*$", RegexOptions.Multiline).Groups[1].Value);//Manpower
            text = Regex.Replace(text, @"^manpower\s*=\s*(\d*).*$", string.Empty, RegexOptions.Multiline);//Вырезание Manpower

            state.Category.Value = Regex.Match(text, @"^state_category\s*=\s*[""]?(\w+)[""]?.*", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value;//StateCategory
            text = Regex.Replace(text, @"^state_category\s*=\s*[""]?(\w+)[""]?.*", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);//Вырезание StateCategory

            string buffer = Regex.Match(text, @"^provinces\s*=\s*{(.*?)}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).Groups[1].Value;
            firstCollectionBuffer = Regex.Matches(buffer, @"\s*(\d+)\s*", RegexOptions.IgnoreCase | RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();//Provinces
            foreach (string line in firstCollectionBuffer)
            {
                state.Provinces.Values.Add(new StateProvince() { Value = int.Parse(line) });
            }
            text = Regex.Replace(text, @"^provinces\s*=\s*{(.*?)}", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);//Вырезание Provinces

            string buildingFactor = Regex.Match(text, @"^buildings_max_level_factor\s*=\s*([\d*|.]*)\D*?", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).Groups[1].Value;
            if (buildingFactor != null && buildingFactor != string.Empty) state.BuildingsFactor.Value = float.Parse(buildingFactor, NumberStyles.Any, CultureInfo.InvariantCulture);//StateCategory
            else state.BuildingsFactor.Value = 1f;
            text = Regex.Replace(text, @"^buildings_max_level_factor\s*=\s*([\d*|.]*)\D*?", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);//Вырезание StateCategory

            string impassable = Regex.Match(text, @"^impassable\s*=\s*(\w{2,3}).*", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value;//Impassable
            if (impassable == "yes") state.IsImpassable.Value = true;
            else state.IsImpassable.Value = false;
            text = Regex.Replace(text, @"^impassable\s*=\s*(\w{2,3}).*", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);//Вырезание Impassable

            buffer = Regex.Match(text, @"^resources={(.*?)}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).Groups[1].Value;
            firstCollectionBuffer = Regex.Matches(buffer, @"\s(\w+)\s*=(\d+)\D*", RegexOptions.IgnoreCase | RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();//Ресурс
            secondCollectionBuffer = Regex.Matches(buffer, @"\s(\w+)\s*=(\d+)\D*", RegexOptions.IgnoreCase | RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[2].Value).ToArray();//Кол-во ресурса
            for (int i = 0; i < firstCollectionBuffer.Length; i++)
            {
                state.Resources.Values.Add(new StateResource()
                {
                    ContentName = firstCollectionBuffer[i],
                    Value = int.Parse(secondCollectionBuffer[i])
                });
            }
            text = Regex.Replace(text, @"^resources\s*=\s*{(.*?)}", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);//Вырезание Resourсes

            text = Regex.Replace(text, @"^[\S]{1}.*$", string.Empty, RegexOptions.Multiline);//Удаление первого уровня
            text = Regex.Replace(text, @"^[\s]{1}", string.Empty, RegexOptions.Multiline); // удаление табуляций
            text = Regex.Replace(text, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline); // удаление пустых строк

            state.History.Owner.Value = Regex.Match(text, @"^owner\s*=\s*([\w|d]{3}).*?", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value;
            text = Regex.Replace(text, @"^owner\s*=\s*[\w|d]{3}.*?", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            firstCollectionBuffer = Regex.Matches(text, @"^victory_points\s*=\s*{\s*(\d+)\s+(\d+[.]*\d*)\s*}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();//Номер провинции
            secondCollectionBuffer = Regex.Matches(text, @"^victory_points\s*=\s*{\s*(\d+)\s+(\d+[.]*\d*)\s*}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[2].Value).ToArray();//Кол-во очков победы
            for (int i = 0; i < firstCollectionBuffer.Length; i++)
            {
                state.History.VictoryPoints.Add(new StateVictoryPoints()
                {
                    Province = int.Parse(firstCollectionBuffer[i]),
                    Value = float.Parse(secondCollectionBuffer[i], CultureInfo.InvariantCulture.NumberFormat)
                });
            }
            text = Regex.Replace(text, @"^victory_points\s*=\s*{\s*(\d+)\s+(\d+[.]*\d*)\s*}", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);//Вырезание VictoryPoints

            buffer = Regex.Match(text, @"(^buildings\s*=\s*{.*?\n})", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline).Groups[1].Value;
            buffer = Regex.Replace(buffer, @"^[\S]{1}.*$", string.Empty, RegexOptions.Multiline);//Удаление первого уровня
            buffer = Regex.Replace(buffer, @"^[\s]{1}", string.Empty, RegexOptions.Multiline); // удаление табуляций

            firstCollectionBuffer = Regex.Matches(buffer, @"(^[\w|_]+)\s*=\s*(\d+)", RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();
            secondCollectionBuffer = Regex.Matches(buffer, @"(^[\w|_]+)\s*=\s*(\d+)", RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[2].Value).ToArray();
            for (int i = 0; i < firstCollectionBuffer.Length; i++)
            {
                state.History.Buildings.Buildings.Add(new StateBuildingsAmount()
                {
                    ContentName = firstCollectionBuffer[i],
                    Value = int.Parse(secondCollectionBuffer[i])
                });
            }

            firstCollectionBuffer = Regex.Matches(buffer, @"(\d+\s*=\s{.*?})", RegexOptions.IgnoreCase | RegexOptions.Singleline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();

            for (int i = 0; i < firstCollectionBuffer.Length; i++)
            {
                state.History.Buildings.ProvinceBuildings.Add(new StateProvinceBuildings()
                {
                    ContentName = Regex.Match(firstCollectionBuffer[i], @"(\d+)\s*=\s{.*?}", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value
                });
                secondCollectionBuffer = Regex.Matches(firstCollectionBuffer[i], @"(\w+)\s*=\s*(\d+)", RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[1].Value).ToArray();
                thirtCollectionBuffer = Regex.Matches(firstCollectionBuffer[i], @"(\w+)\s*=\s*(\d+)", RegexOptions.Multiline).
                OfType<Match>().Select(m => m.Groups[2].Value).ToArray();

                for (int j = 0; j < secondCollectionBuffer.Length; j++)
                {
                    state.History.Buildings.ProvinceBuildings[i].Values.Add(new StateBuildingsAmount()
                    {
                        ContentName = secondCollectionBuffer[j],
                        Value = int.Parse(thirtCollectionBuffer[j])
                    });
                }
            };

            text = Regex.Replace(text, @"^buildings\s*=\s*{.*?^}", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);
            text = Regex.Replace(text, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline); // удаление пустых строк
            state.History.Effects.Value = text;

            return state;
        }

        public string ParseFileName(string filename)
        {
            return Regex.Match(filename, "(.*)[.]txt").Groups[1].Value;
        }
    }
}