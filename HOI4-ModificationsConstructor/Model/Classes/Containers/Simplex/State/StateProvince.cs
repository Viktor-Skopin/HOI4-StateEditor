using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Номер провинции, принадлежащей региону.
    /// </summary>
    public class StateProvince : IIntContainer, IDataErrorInfo
    {
        public StateProvince()
        {
            ContentName = "province";

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
            }
        }

        public string ContentName { get; } //Используется только для идентификации

        public string GetString()
        {
            return Value.ToString();
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
                            error = "Номер провинции должен быть неотрицательным целым числом";
                        }
                        break;
                }
                return error;
            }
        }
    }
}