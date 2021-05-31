using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Ключ локализации региона.
    /// </summary>
    public class StateName : IStringContainer , IDataErrorInfo
    {
        public StateName()
        {
            ContentName = "name";

            Value = "STATE_0";
        }

        //Основная логика
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
        public string ContentName { get; }

        public string GetString()
        {
            return $"{ContentName} = \"{Value}\"";
        }

        //Событие изменения
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //Валидация
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Value":
                        if (!Regex.IsMatch(Value, @"^[\w]+$"))
                        {
                            error = "Имя не должно содержать спецсимволов и пробелов";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
