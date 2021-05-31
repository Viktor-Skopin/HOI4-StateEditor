using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System;

namespace HOI4_ModificationsConstructor
{
    public class DefaultFileService : IFileService
    {
        public List<State> Open(string foldername)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            StateParcer parcer = new StateParcer();

            string[] files;

            List<State> states = new List<State>();

            files = Directory.EnumerateFiles(foldername + @"\history\states", "*.txt").Select(File.ReadAllText).ToArray();

            DirectoryInfo info = new DirectoryInfo(foldername + @"\history\states");
            FileInfo[] fileInfos = info.GetFiles("*.txt");
            string[] fileNames = new string[fileInfos.Length];

            for(int i = 0; i < fileInfos.Length; i++)
            {           
                State state = parcer.ParseState(files[i]);
                state.FileName = parcer.ParseFileName(fileInfos[i].Name);
                states.Add(state);
            }

            stopwatch.Stop();
            Debug.WriteLine($"Время импорта - {stopwatch.ElapsedMilliseconds}");

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
                    byte[] arrey = System.Text.Encoding.Default.GetBytes(state.GetString());

                    fs.Write(arrey, 0, arrey.Length);
                }
            }
        }
    }
}
