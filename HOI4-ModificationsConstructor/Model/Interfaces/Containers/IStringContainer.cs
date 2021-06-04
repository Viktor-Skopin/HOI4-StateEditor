namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит и возвращает в текстовом отформатированном виде строковую переменную.
    /// </summary>
    public interface IStringContainer : IClausewitzElement
    {
        /// <summary>
        /// Значение данных.
        /// </summary>
        string Value { get; set; }
    }
}