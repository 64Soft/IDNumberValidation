namespace IDNumberValidation.AdditionalDataInterfaces
{
    /// <summary>
    /// If an ID number contains gender information, it should implement this interface
    /// </summary>
    public interface IHasGender
    {
        /// <summary>
        /// The gender of a person. If empty, it is unknown or could not be parsed
        /// </summary>
        GenderEnum? Gender { get; }
    }
}
