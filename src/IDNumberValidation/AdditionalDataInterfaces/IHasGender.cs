namespace IDNumberValidation.AdditionalDataInterfaces
{
    /// <summary>
    /// Indicates that an ID number contains gender information of a person
    /// </summary>
    public interface IHasGender
    {
        /// <summary>
        /// The gender of a person. If empty, it is unknown or could not be parsed
        /// </summary>
        GenderEnum? Gender { get; set; }
    }
}
