namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит и возвращает в текстовом отформатированном виде целое число.
    /// </summary>
    public interface IIntContainer : IClausewitzElement
    {
        /// <summary>
        /// Значение данных.
        /// </summary>
        int Value { get; set; }
    }
}