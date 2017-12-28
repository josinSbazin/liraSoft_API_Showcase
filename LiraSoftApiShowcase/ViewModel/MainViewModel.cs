using System;
using LiraSoftApiShowcase.Model;
using MicroMvvm;
using Microsoft.Win32;

namespace LiraSoftApiShowcase.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            InitCommands();
        }

        // Создаем пустую модель в Lira Soft
        public RelayCommand CreateEmptyModelCommand { get; private set; }

        // Строим простейший архитектурный стержень
        public RelayCommand CreateSimpleRodCommand { get; private set; }

        // Строим простейшую архитектурную пластину
        public RelayCommand CreateSimplePolygonCommand { get; private set; }

        #region Private Methods

        #region Initialization

        private void InitCommands()
        {
            CreateEmptyModelCommand = new RelayCommand(CreateEmptyModel);
            CreateSimpleRodCommand = new RelayCommand(CreateSimpleRod);
            CreateSimplePolygonCommand = new RelayCommand(CreateSimplePolygon);
        }

        #endregion

        #region RelayCommand Actions

        private void CreateEmptyModel()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Файлы проекта расчета (*.fep)|*.fep",
                FileName = Guid.NewGuid().ToString(),
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;

                // create empty model
                ModelCreator.CreateEmptyModel(filePath);
                // start lira
                LiraUtils.OpenFileByLira(filePath);
            }
        }

        private void CreateSimpleRod() { }

        private void CreateSimplePolygon() { }

        #endregion

        #endregion
    }
}