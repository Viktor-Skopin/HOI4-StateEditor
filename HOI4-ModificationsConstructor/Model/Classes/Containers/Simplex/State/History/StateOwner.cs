using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HOI4_ModificationsConstructor
{
    public class StateOwner : IStringContainer, IDataErrorInfo
    {
        /// <summary>
        /// Класс-контейнер, содержащий информацию о стране-владельце региона.
        /// </summary>
        public StateOwner()
        {
            ContentName = "owner";

            Value = "ENG";
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
            return $"{ContentName} = {Value}";
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
                        if (!Regex.IsMatch(Value, @"^[\w]{3}$"))
                        {
                            error = "Тег страны должен быть длинной 3 символа и не должен содержать спецсимволов и пробелов";
                        }
                        break;
                }
                return error;
            }
        }
    }
}