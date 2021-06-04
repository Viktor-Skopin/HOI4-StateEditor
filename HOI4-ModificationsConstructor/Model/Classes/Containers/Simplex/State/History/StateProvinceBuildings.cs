using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Класс-контейнер, содержащий информацию о номере провинции и её постройках.
    /// </summary>
    public class StateProvinceBuildings : IMultipleContainer, IDataErrorInfo
    {
        public StateProvinceBuildings()
        {
            ContentName = "0";

            Elements = new List<IClausewitzElement>();

            Values = new List<StateBuildingsAmount>();
        }

        //Основная логика
        public List<IClausewitzElement> Elements { get; }

        private List<StateBuildingsAmount> _values;

        /// <summary>
        /// Провинциальные постройки.
        /// </summary>
        public List<StateBuildingsAmount> Values
        {
            get { return _values; }
            set
            {
                _values = value;
                OnPropertyChanged("Values");
            }
        }

        private string _contentName;

        /// <summary>
        /// Номер провинции.
        /// </summary>
        public string ContentName
        {
            get { return _contentName; }
            set
            {
                _contentName = value;
                OnPropertyChanged("ContentName");
            }
        }

        private void MakeList()
        {
            Elements.Clear();
            Elements.AddRange(Values);
        }

        public string GetString()
        {
            MakeList();

            string result = $"{ContentName} = {{";
            foreach (IClausewitzElement element in Elements)
            {
                result += $"\n{element.GetString()} ";
            }
            result += $"\n}}";

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
                    case "ContentName":
                        if (!Regex.IsMatch(ContentName, @"^[\d]+$"))
                        {
                            error = "Номер провинции должен быть челым неотрицательным числом";
                        }
                        break;
                }
                return error;
            }
        }
    }
}