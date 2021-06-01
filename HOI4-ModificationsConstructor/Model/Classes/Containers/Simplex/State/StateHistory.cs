using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HOI4_ModificationsConstructor
{
    public class StateHistory : IMultipleContainer
    {
        public StateHistory()
        {
            ContentName = "history";

            Elements = new List<IClausewitzElement>();

            Owner = new StateOwner();
            Effects = new StateEffects();
            VictoryPoints = new ObservableCollection<StateVictoryPoints>();
            Buildings = new StateBuildings();
        }

        //Основная логика
        private StateOwner _owner;

        private StateEffects _effects;
        private ObservableCollection<StateVictoryPoints> _victoryPoints;
        private StateBuildings _buildings;

        public StateOwner Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                OnPropertyChanged("Owner");
            }
        }

        public StateEffects Effects
        {
            get { return _effects; }
            set
            {
                _effects = value;
                OnPropertyChanged("Effects");
            }
        }

        public ObservableCollection<StateVictoryPoints> VictoryPoints
        {
            get { return _victoryPoints; }
            set
            {
                _victoryPoints = value;
                OnPropertyChanged("VictoryPoints");
            }
        }

        public StateBuildings Buildings
        {
            get { return _buildings; }
            set
            {
                _buildings = value;
                OnPropertyChanged("Buildings");
            }
        }

        public List<IClausewitzElement> Elements { get; }
        public string ContentName { get; }

        private void MakeList()
        {
            Elements.Clear();
            Elements.Add(Owner);
            Elements.Add(Effects);
            Elements.AddRange(VictoryPoints);
            Elements.Add(Buildings);
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

            return result;
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