using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Класс-контейнер, хранящий информацию о регионе.
    /// </summary>
    public class State : IMultipleContainer
    {
        public State()
        {
            ContentName = "state";

            Elements = new List<IClausewitzElement>();

            ID = new StateID();
            Name = new StateName();
            Manpower = new StateManpower();
            Category = new StateCategory();
            BuildingsFactor = new StateBuildingsFactor();
            IsImpassable = new StateImpassable();
            Provinces = new StateProvinces();
            Resources = new StateResources();
            History = new StateHistory();

            MakeList();
        }

        private StateID _id;
        private StateName _name;
        private StateManpower _manpower;
        private StateCategory _category;
        private StateBuildingsFactor _buildingsFactor;
        private StateImpassable _isImpassable;
        private StateProvinces _provinces;
        private StateResources _resources;
        private StateHistory _history;

        private string _fileName;
        private Jsbeautifier.Beautifier _beautifier = new Jsbeautifier.Beautifier();

        private Jsbeautifier.BeautifierOptions _beautifierOptions = new Jsbeautifier.BeautifierOptions()
        {
            IndentWithTabs = true
        };

        private string _stringView;

        public List<IClausewitzElement> Elements { get; }

        public string ContentName { get; }

        /// <summary>
        /// Идентификационный номер.
        /// </summary>
        public StateID ID
        {
            get
            {
                UpdateString();
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Имя текстового файла, в который будут сохранены данные региона.
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        /// <summary>
        /// Ключ локализации региона. При отсутствии локализации, выполняет функции названия региона.
        /// </summary>
        public StateName Name
        {
            get
            {
                UpdateString();
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Население.
        /// </summary>
        public StateManpower Manpower
        {
            get
            {
                UpdateString();
                return _manpower;
            }
            set
            {
                _manpower = value;
                OnPropertyChanged("Manpower");
            }
        }

        /// <summary>
        /// Категория.
        /// </summary>
        public StateCategory Category
        {
            get
            {
                UpdateString();
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        /// <summary>
        /// Фактор максимального строительства региона. Влияет на максимальное количество доступных
        /// ячеек строительства.
        /// </summary>
        public StateBuildingsFactor BuildingsFactor
        {
            get
            {
                UpdateString();
                return _buildingsFactor;
            }
            set
            {
                _buildingsFactor = value;
                OnPropertyChanged("BuildingsFactor");
            }
        }

        /// <summary>
        /// Непроходимость. Определяет возможность передвигаться через регион.
        /// </summary>
        public StateImpassable IsImpassable
        {
            get
            {
                UpdateString();
                return _isImpassable;
            }
            set
            {
                _isImpassable = value;
                OnPropertyChanged("IsImpassable");
            }
        }

        /// <summary>
        /// Провинции, входящие в состав региона.
        /// </summary>
        public StateProvinces Provinces
        {
            get
            {
                UpdateString();
                return _provinces;
            }
            set
            {
                _provinces = value;
                OnPropertyChanged("Provinces");
            }
        }

        /// <summary>
        /// Ресурсы, расположенные внутри региона.
        /// </summary>
        public StateResources Resources
        {
            get
            {
                UpdateString();
                return _resources;
            }
            set
            {
                _resources = value;
                OnPropertyChanged("Resources");
            }
        }

        /// <summary>
        /// Блок данных содержащий сведения о постройках, владельце и очках победы региона.
        /// </summary>
        public StateHistory History
        {
            get
            {
                UpdateString();
                return _history;
            }
            set
            {
                _history = value;
                OnPropertyChanged("History");
            }
        }

        /// <summary>
        /// Отформатированный код, сформированный из данных региона.
        /// </summary>
        public string StringView
        {
            get { return _stringView; }
            set
            {
                _stringView = value;
                OnPropertyChanged("StringView");
            }
        }

        /// <summary>
        /// Очищает список элементов и заново формирует его.
        /// </summary>
        private void MakeList()
        {
            Elements.Clear();
            Elements.Add(ID);
            Elements.Add(Name);
            Elements.Add(Manpower);
            Elements.Add(Category);
            Elements.Add(BuildingsFactor);
            Elements.Add(IsImpassable);
            Elements.Add(Provinces);
            Elements.Add(Resources);
            Elements.Add(History);
        }

        /// <summary>
        /// Возвращает отформатированный код, сформированный из данных региона.
        /// </summary>
        public string GetString()
        {
            MakeList();

            string result = $"{ContentName} = {{";
            foreach (IClausewitzElement element in Elements)
            {
                result += $"\n{element.GetString()}";
            }
            result += $"\n}}";

            //Расстановка табуляций с помощью сторонней библиотеки "Jsbeautifier"
            return _beautifier.Beautify(result, _beautifierOptions);
        }

        /// <summary>
        /// Обновление содержимого строки с кодом.
        /// </summary>
        private void UpdateString()
        {
            //Проверка на количество элементов нужна так как выполнение метода при частично не инициализированных данных приводит к ошибке.
            if (Elements.Count < 9) return;
            else StringView = GetString();
        }

        /// <summary>
        /// Событие, возникающее при изменении свойств класса.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, вызываемый при изменении свойств класса.
        /// </summary>
        /// <param name="prop">Название сфойства.</param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}