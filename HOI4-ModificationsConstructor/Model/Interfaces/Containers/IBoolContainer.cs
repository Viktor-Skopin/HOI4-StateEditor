namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит и возвращает в текстовом отформатированном виде данные формата "да/нет".
    /// </summary>
    public interface IBoolContainer : IClausewitzElement
    {
        /// <summary>
        /// Значение данных.
        /// </summary>
        bool Value { get; set; }
    }
}