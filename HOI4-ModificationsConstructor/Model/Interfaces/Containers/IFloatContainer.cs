namespace HOI4_ModificationsConstructor
{
    /// <summary>
    /// Содержит и возвращает в текстовом отформатированном виде дробное число.
    /// </summary>
    public interface IFloatContainer : IClausewitzElement
    {
        /// <summary>
        /// Значение данных.
        /// </summary>
        float Value { get; set; }
    }
}