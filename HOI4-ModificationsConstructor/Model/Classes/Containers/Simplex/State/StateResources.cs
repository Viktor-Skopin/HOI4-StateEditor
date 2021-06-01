using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Список ресурсов региона.
    /// </summary>
    public class StateResources : IMultipleContainer
    {
        public StateResources()
        {
            ContentName = "resources";

            Elements = new List<IClausewitzElement>();

            Values = new ObservableCollection<StateResource>();
        }

        public List<IClausewitzElement> Elements { get; }
        private ObservableCollection<StateResource> _values;

        public ObservableCollection<StateResource> Values
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

            if (Elements.Count > 0)
            {
                string result = $"{ContentName} = {{";
                foreach (IClausewitzElement element in Elements)
                {
                    result += $"\n{element.GetString()}";
                }
                result += $"\n}}";

                return result;
            }
            else return string.Empty;
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