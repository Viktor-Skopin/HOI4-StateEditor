namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Отображает окна открытия и сохранения файла, а также содержит информацию о пути к файлу.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Отображает сообщение с заданным текстом.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowMessage(string message);

        /// <summary>
        /// Путь к папке или файлу.
        /// </summary>
        string FolderPath { get; set; }

        /// <summary>
        /// Отображает окно открытия файла.
        /// </summary>
        /// <returns>Возвращяет "true" если выбор прошёл успешно и "false" в случае ошибки.</returns>
        bool OpenFileDialog();

        /// <summary>
        /// Отображает окно сохранения файла.
        /// </summary>
        /// <returns>Возвращяет "true" если выбор прошёл успешно и "false" в случае ошибки.</returns>
        bool SaveFileDialog();
    }
}