using System.Collections.Generic;

namespace HOI4_ModificationsConstructor
{
    public interface IMultipleContainer : IClausewitzElement
    {
        /// <summary>
        /// Список элементов контейнера.
        /// </summary>
        List<IClausewitzElement> Elements { get; }
    }
}
