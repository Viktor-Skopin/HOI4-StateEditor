using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Класс, осуществляющий сохранение и загрузку данных о регионах.
    /// </summary>
    public class StateFileService : IFileService
    {
        public List<State> Open(string foldername)
        {
            StateParcer parcer = new StateParcer();

            List<State> states = new List<State>();

            //Получение содержимого файлов
            string[] files = Directory.EnumerateFiles(foldername + @"\history\states", "*.txt").Select(File.ReadAllText).ToArray();

            //Получение информации о файлах
            FileInfo[] fileInfos = new DirectoryInfo(foldername + @"\history\states").GetFiles("*.txt");

            string[] fileNames = new string[fileInfos.Length];

            for (int i = 0; i < fileInfos.Length; i++)
            {
                State state = parcer.ParseState(files[i]);
                state.FileName = parcer.ParseFileName(fileInfos[i].Name);
                states.Add(state);
            }

            return states;
        }

        public void Save(string foldername, List<State> states)
        {
            string fullPath = $"{foldername}\\history\\states";

            foreach (State state in states)
            {
                if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

                using (FileStream fs = new FileStream($"{fullPath}\\{state.FileName}.txt", FileMode.Create))
                {
                    byte[] text = System.Text.Encoding.Default.GetBytes(state.GetString());

                    fs.Write(text, 0, text.Length);
                }
            }
        }
    }
}