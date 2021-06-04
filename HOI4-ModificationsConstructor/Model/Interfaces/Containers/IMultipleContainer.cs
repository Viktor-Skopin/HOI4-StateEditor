using System.Collections.Generic;

namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит и возвращает в текстовом отформатированном виде список классов-контейнеров,
    /// реализующих интерфейс "IClausewitzElement".
    /// </summary>
    public interface IMultipleContainer : IClausewitzElement
    {
        /// <summary>
        /// Список классов-контейнеров.
        /// </summary>
        List<IClausewitzElement> Elements { get; }
    }
}