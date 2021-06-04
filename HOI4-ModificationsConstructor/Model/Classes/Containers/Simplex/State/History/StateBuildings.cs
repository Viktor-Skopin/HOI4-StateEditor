using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Класс-контейнер, содержащий список региональных построек и список провинциальных построек.
    /// </summary>
    public class StateBuildings : IMultipleContainer
    {
        public StateBuildings()
        {
            ContentName = "buildings";

            Elements = new List<IClausewitzElement>();

            Buildings = new List<StateBuildingsAmount>();

            ProvinceBuildings = new List<StateProvinceBuildings>();
        }

        //Основная логика
        private List<StateBuildingsAmount> _buildings;

        /// <summary>
        /// Региональные постройки.
        /// </summary>
        public List<StateBuildingsAmount> Buildings
        {
            get { return _buildings; }
            set
            {
                _buildings = value;
                OnPropertyChanged("Buildings");
            }
        }

        private List<StateProvinceBuildings> _provinceBuildings;

        /// <summary>
        /// Провинциальные постройки.
        /// </summary>
        public List<StateProvinceBuildings> ProvinceBuildings
        {
            get { return _provinceBuildings; }
            set
            {
                _provinceBuildings = value;
                OnPropertyChanged("ProvinceBuildings");
            }
        }

        public List<IClausewitzElement> Elements { get; }
        public string ContentName { get; }

        private void MakeList()
        {
            Elements.Clear();
            Elements.AddRange(Buildings);
            Elements.AddRange(ProvinceBuildings);
        }

        public string GetString()
        {
            MakeList();

            string result = $"{ContentName} = {{";
            foreach (IClausewitzElement element in Elements)
            {
                result += $"\n{element.GetString()}";
            }
            result += $"\n}}";

            return result;
        }

        //Событие изменения
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}