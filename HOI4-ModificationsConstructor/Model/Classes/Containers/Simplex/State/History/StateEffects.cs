using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    public class StateEffects : IStringContainer
    {
        public StateEffects()
        {
            ContentName = "effects";

            Value = "";
        }

        //Основная логика
        public string ContentName { get; }

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public string GetString()
        {
            //Заглушка
            return Value;
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