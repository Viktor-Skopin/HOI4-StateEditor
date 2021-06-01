using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Кол-во дополнительных ячеек зданий
    /// </summary>
    public class StateBuildingsFactor : IFloatContainer, IDataErrorInfo
    {
        public StateBuildingsFactor()
        {
            ContentName = "buildings_max_level_factor";

            Value = 0F;
        }

        //Основная логика
        private float _value;

        public float Value
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
                        if (Value < 0f)
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