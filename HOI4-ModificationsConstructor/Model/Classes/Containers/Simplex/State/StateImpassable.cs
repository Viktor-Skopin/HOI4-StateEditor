using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Статус непроходимости региона.
    /// </summary>
    public class StateImpassable : IBoolContainer
    {
        public StateImpassable()
        {
            ContentName = "impassable";

            Value = false;
        }

        //Основная логика
        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public string ContentName { get; }
        public string GetString()
        {
            string result;

            if (Value == true) result = "yes";
            else result = "no";

            return $"{ContentName} = {result}";
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
