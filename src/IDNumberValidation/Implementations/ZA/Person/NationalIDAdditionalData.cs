using System;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation.Implementations.ZA.Person
{
    public class NationalIDAdditionalData : IAdditionalData, IHasBirthDate, IHasGender
    {
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
    }
}
