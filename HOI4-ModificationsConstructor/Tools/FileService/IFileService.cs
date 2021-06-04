using System.Collections.Generic;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Экспортирует и импортирует серию данных о регионах.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Возвращает список регионов, полученный из текстовых файлов.
        /// </summary>
        /// <param name="foldername">Путь к папке модификации либо игры.</param>
        /// <returns></returns>
        List<State> Open(string foldername);

        /// <summary>
        /// Экспортирует данные о регионах в текстовые файлы и сохраняет их в указанную папку.
        /// </summary>
        /// <param name="foldername">Путь к папке модификации либо игры.</param>
        /// <param name="states">Список сохраняемых регионов.</param>
        void Save(string foldername, List<State> states);
    }
}