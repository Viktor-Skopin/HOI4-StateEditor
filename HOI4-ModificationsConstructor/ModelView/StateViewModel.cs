using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;


namespace HOI4_ModificationsConstructor
{
    public class StateViewModel
    {
        public StateViewModel(IDialogService dialogService, IFileService fileService)
        {
            _dialogService = dialogService;
            _fileService = fileService;

            States = new ObservableCollection<State>();
        }

        /// <summary>
        /// Список регионов.
        /// </summary>
        public ObservableCollection<State> States { get; set; }

        private State _selectedState;

        /// <summary>
        /// Выбранный регион.
        /// </summary>
        public State SelectedState
        {
            get { return _selectedState; }
            set
            {
                _selectedState = value;
                OnPropertyChanged("SelectedState");
            }
        }

        private IFileService _fileService;
        private IDialogService _dialogService;
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (_dialogService.SaveFileDialog() == true)
                            {
                                _fileService.Save(_dialogService.FolderPath, States.ToList());
                                _dialogService.ShowMessage("Файл сохранён");
                            }
                        }
                        catch (Exception ex)
                        {
                            _dialogService.ShowMessage(ex.Message);
                        }
                    }));
            }
        }

        private RelayCommand openCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.OpenFileDialog() == true)
                          {
                              States.Clear();
                              List<State> states = _fileService.Open(_dialogService.FolderPath);
                              foreach (State state in states)
                              {
                                  States.Add(state);
                              };                                                          
                              _dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand addCommand;
        /// <summary>
        /// Команда добавления нового региона.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      State state;
                      if (SelectedState != null)
                      {
                          state = new State()
                          {
                              ID = new StateID()
                              {
                                  Value = States[States.Count - 1].ID.Value + 1
                              },
                              Name = new StateName()
                              {
                                  Value = $"STATE_{States[States.Count - 1].ID.Value + 1}"
                              }
                          };
                      }
                      else
                      {
                          state = new State()
                          {
                              ID = new StateID()
                              {
                                  Value = 0
                              },
                              Name = new StateName()
                              {
                                  Value = "STATE_0"
                              }
                          };
                      }
                      States.Insert(States.Count, state);
                      SelectedState = state;
                  }));
            }
        }

        private RelayCommand removeCommand;
        /// <summary>
        /// Команда удаления выбранного региона.
        /// </summary>
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        State state = obj as State;
                        if (state != null)
                        {
                            States.Remove(state);
                        }
                    },
                    (obj) => States.Count > 0 && SelectedState != null));
            }
        }

        private RelayCommand _addProvinceCommand;

        public RelayCommand AddProvinceCommand
        {
            get
            {
                return _addProvinceCommand ??
                    (_addProvinceCommand = new RelayCommand(obj =>
                    {
                        StateProvince province = new StateProvince();

                        SelectedState.Provinces.Values.Insert(SelectedState.Provinces.Values.Count, province);
                    },
                    (obj) => SelectedState != null));
            }
        }

        private RelayCommand _removeProvinceCommand;

        public RelayCommand RemoveProvinceCommand
        {
            get
            {
                return _removeProvinceCommand ??
                    (_removeProvinceCommand = new RelayCommand(obj =>
                    {
                        StateProvince province = obj as StateProvince;
                        if (province != null)
                        {
                            SelectedState.Provinces.Values.Remove(province);
                        }
                    }));
            }
        }

        private RelayCommand _addResourseCommand;
        public RelayCommand AddResourseCommand
        {
            get
            {
                 return _addResourseCommand ??
                    (_addResourseCommand = new RelayCommand(obj =>
                    {
                        StateResource resource = new StateResource();

                        SelectedState.Resources.Values.Insert(SelectedState.Resources.Values.Count, resource);
                    },
                    (obj) => SelectedState != null));
            }
        }

        private RelayCommand _removeResourseCommand;
        public RelayCommand RemoveResourceCommand
        {
            get
            {
                return _removeResourseCommand ??
                    (_removeResourseCommand = new RelayCommand(obj =>
                    {
                        StateResource resource = new StateResource();
                        if (resource != null)
                        {
                            SelectedState.Resources.Values.Remove(resource);
                        }
                    }));
            }
        }

        private RelayCommand _addVictoryPointCommand;
        public RelayCommand AddVictoryPointCommand
        {
            get
            {
                return _addVictoryPointCommand ??
                    (_addVictoryPointCommand = new RelayCommand(obj =>
                    {
                        StateVictoryPoints victoryPoints = new StateVictoryPoints();

                        SelectedState.History.VictoryPoints.Insert(SelectedState.History.VictoryPoints.Count, victoryPoints);
                    },
                    (obj) => SelectedState != null));
            }
        }

        private RelayCommand _removeVictoryPointCommand;
        public RelayCommand RemoveVictoryPointCommand
        {
            get
            {
                return _removeVictoryPointCommand ??
                    (_removeVictoryPointCommand = new RelayCommand(obj =>
                    {
                        StateVictoryPoints victoryPoints = new StateVictoryPoints();
                        if (victoryPoints != null)
                        {
                            SelectedState.History.VictoryPoints.Remove(victoryPoints);
                        }
                    }));
            }
        }

        private RelayCommand _addBuildingCommand;
        public RelayCommand AddBuildingCommand
        {
            get
            {
                return _addBuildingCommand ??
                    (_addBuildingCommand = new RelayCommand(obj =>
                    {
                        StateBuildingsAmount stateBuildings = new StateBuildingsAmount();

                        SelectedState.History.Buildings.Buildings.Insert(SelectedState.History.Buildings.Buildings.Count, stateBuildings);
                    },
                    (obj) => SelectedState != null));
            }
        }

        private RelayCommand _removeBuildingCommand;
        public RelayCommand RemoveBuildingCommand
        {
            get
            {
                return _removeBuildingCommand ??
                    (_removeBuildingCommand = new RelayCommand(obj =>
                    {
                        StateBuildingsAmount stateBuildings = new StateBuildingsAmount();
                        if (stateBuildings != null)
                        {
                            SelectedState.History.Buildings.Buildings.Remove(stateBuildings);
                        }
                    }));
            }
        }

        private RelayCommand _addProvinceBuildingCommand;
        public RelayCommand AddProvinceBuildingCommand
        {
            get
            {
                return _addProvinceBuildingCommand ??
                    (_addProvinceBuildingCommand = new RelayCommand(obj =>
                    {
                        StateProvinceBuildings buildings = new StateProvinceBuildings();

                        SelectedState.History.Buildings.ProvinceBuildings.Insert(SelectedState.History.Buildings.ProvinceBuildings.Count, buildings);
                    },
                    (obj) => SelectedState != null));
            }
        }

        private RelayCommand _removeProvinceBuildingCommand;
        public RelayCommand RemoveProvinceBuildingCommand
        {
            get
            {
                return _removeProvinceBuildingCommand ??
                    (_removeProvinceBuildingCommand = new RelayCommand(obj =>
                    {
                        StateProvinceBuildings buildings = new StateProvinceBuildings();
                        if (buildings != null)
                        {
                            SelectedState.History.Buildings.ProvinceBuildings.Remove(buildings);
                        }
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}