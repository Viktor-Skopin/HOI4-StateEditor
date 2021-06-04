using System.ComponentModel;

namespace HOI4_ModificationsConstructor
{
    public interface IClausewitzElement : INotifyPropertyChanged
    {
        /// <summary>
        /// Название контента, отображающее его тип или значение.
        /// </summary>
        abstract public string ContentName { get; }

        /// <summary>
        /// Возвращает текстовое представление контейнера и всех содержащихся в нём контейнеров
        /// в виде кода.
        /// </summary>
        abstract public string GetString();
    }
}