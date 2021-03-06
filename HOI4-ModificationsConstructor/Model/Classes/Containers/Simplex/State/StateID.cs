using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Класс-контейнер, содержащий информацию о идентификационном номере региона.
    /// </summary>
    public class StateID : IIntContainer, IDataErrorInfo
    {
        public StateID()
        {
            ContentName = "id";

            Value = 0;
        }

        //Основная логика
        private int _value;

        public int Value
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
                        if (Value < 0)
                        {
                            error = "ID должен быть неотрицательным целым числом";
                        }
                        break;
                }
                return error;
            }
        }
    }
}