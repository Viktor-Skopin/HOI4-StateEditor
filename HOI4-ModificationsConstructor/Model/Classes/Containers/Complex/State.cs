using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
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
        
        //Основная логика
        private StateID _id;
        private StateName _name;
        private StateManpower _manpower;
        private StateCategory _category;
        private StateBuildingsFactor _buildingsFactor;
        private StateImpassable _isImpassable;
        private StateProvinces _provinces;
        private StateResources _resources;
        private StateHistory _history;

        private Jsbeautifier.Beautifier _beautifier = new Jsbeautifier.Beautifier();
        private Jsbeautifier.BeautifierOptions _beautifierOptions = new Jsbeautifier.BeautifierOptions()
        {
            IndentWithTabs = true
        };

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

        private string _fileName;
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
        /// Название.
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
        /// Фактор строительства.
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
        /// Непроходимость
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
        /// Привинции.
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
        /// Ресурсы.
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
        /// Блок данных "History".
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

        private string _stringView;
        /// <summary>
        /// Сформированная строка.
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

        public List<IClausewitzElement> Elements { get; }
        public string ContentName { get; }

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

        public string GetString()
        {
            MakeList();

            string result = $"{ContentName} = {{";
            foreach (IClausewitzElement element in Elements)
            {
                result += $"\n{element.GetString()}";
            }
            result += $"\n}}";
                     
            return _beautifier.Beautify(result, _beautifierOptions);

            
        }

        private void UpdateString()
        {
            if (Elements.Count < 9) return;
            else StringView = GetString();
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