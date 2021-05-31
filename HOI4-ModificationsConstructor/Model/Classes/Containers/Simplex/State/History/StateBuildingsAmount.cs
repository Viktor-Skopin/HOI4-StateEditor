using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HOI4_ModificationsConstructor
{
    public class StateBuildingsAmount : IIntContainer, IDataErrorInfo
    {
        public StateBuildingsAmount()
        {
            ContentName = "infrastructure";
            Value = 1;
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
        private string _contentName;
        public string ContentName
        {
            get { return _contentName; }
            set
            {
                _contentName = value;
                OnPropertyChanged("ContentName");
            }
        }
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
                        if (Value < 1)
                        {
                            error = "Крол-во построек должно быть целым числом больше нуля";
                        }
                        break;
                    case "ContentName":
                        if (!Regex.IsMatch(ContentName, @"^[\w]+$"))
                        {
                            error = "Название постройки должно быть неотрицательным целым числом";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
