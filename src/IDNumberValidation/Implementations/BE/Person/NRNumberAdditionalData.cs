using System;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation.Implementations.BE.Person
{
    public class NRNumberAdditionalData : IAdditionalData, IHasBirthDate, IHasGender
    {
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
    }
}
