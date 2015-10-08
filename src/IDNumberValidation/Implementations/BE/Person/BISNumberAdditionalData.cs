using System;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation.Implementations.BE.Person
{
    public class BISNumberAdditionalData : IAdditionalData, IHasBirthDate, IHasGender
    {
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public bool? GenderKnown { get; set; }
    } 
}
