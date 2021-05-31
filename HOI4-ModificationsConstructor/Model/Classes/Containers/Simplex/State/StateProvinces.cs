using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    public class StateProvinces : IMultipleContainer
    {
        public StateProvinces()
        {
            ContentName = "provinces";

            Elements = new List<IClausewitzElement>();

            Values = new ObservableCollection<StateProvince>();
        }

        //Основная логика
        public List<IClausewitzElement> Elements { get; }
        private ObservableCollection<StateProvince> _values;
        public ObservableCollection<StateProvince> Values
        {
            get { return _values; }
            set
            {
                _values = value;
                OnPropertyChanged("Values");
            }
        }
        public string ContentName { get; }
        private void MakeList()
        {
            Elements.Clear();
            Elements.AddRange(Values);
        }
        public string GetString()
        {
            MakeList();

            string result = $"{ContentName} = {{";
            result += "\n";
            foreach (IClausewitzElement element in Elements)
            {
                result += $"{element.GetString()} ";
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
