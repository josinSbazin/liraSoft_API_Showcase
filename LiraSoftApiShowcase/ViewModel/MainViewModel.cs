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
        public RelayCommand CreateSimplePlateCommand { get; private set; }

        #region Private Methods

        #region Initialization

        private void InitCommands()
        {
            CreateEmptyModelCommand = new RelayCommand(CreateEmptyModel);
            CreateSimpleRodCommand = new RelayCommand(CreateSimpleRod);
            CreateSimplePlateCommand = new RelayCommand(CreateSimplePlate);
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

                // создаем пустую модель
                ModelCreator.CreateEmptyModel(filePath);

                // открываем в лире
                LiraUtils.OpenFileByLira(filePath);
            }
        }

        private void CreateSimpleRod()
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

                // создаем модель с простым стержнем
                ModelCreator.CreateSimpleRod(filePath);

                // открываем в лире
                LiraUtils.OpenFileByLira(filePath);
            }
        }

        private void CreateSimplePlate()
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

                // создаем модель с простой пластиной
                ModelCreator.CreateSimplePlate(filePath);

                // открываем в лире
                LiraUtils.OpenFileByLira(filePath);
            }
        }

        #endregion

        #endregion
    }
}