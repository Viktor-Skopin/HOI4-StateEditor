namespace HOI4_ModificationsConstructor
{
    public interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения

        string FolderPath { get; set; }   // путь к выбранному файлу

        bool OpenFileDialog();  // открытие файла

        bool SaveFileDialog();  // сохранение файла
    }
}