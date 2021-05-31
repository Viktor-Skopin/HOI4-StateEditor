using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Очки победы региона.
    /// </summary>
    public class StateVictoryPoints : IFloatContainer, IDataErrorInfo
    {
        public StateVictoryPoints()
        {
            ContentName = "victory_points";
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
        private int _province;
        public int Province
        {
            get { return _province; }
            set
            {
                _province = value;
                OnPropertyChanged("Province");
            }
        }
        public string ContentName { get; }
        public string GetString()
        {
            string result = $"{ContentName} = {{";
            result += "\n";
            result += $"{Province} {Value}";
            result += "\n}";

            return result;
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
                            error = "Кол-во очков победы должно быть целым числом и не должно быть меньше единицы";
                        }
                        break;
                    case "Province":
                        if(Province < 0)
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
