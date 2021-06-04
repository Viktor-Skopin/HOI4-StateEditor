using System.ComponentModel;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит данные определённого типа и возвращает их в текстовом представлении.
    /// </summary>
    public interface IClausewitzElement : INotifyPropertyChanged
    {
        /// <summary>
        /// Название данных, отображающее их тип или значение.
        /// </summary>
        string ContentName { get; }

        /// <summary>
        /// Возвращает текстовое представление контейнера и всех содержащихся в нём контейнеров.
        /// в виде кода.
        /// </summary>
        string GetString();
    }
}