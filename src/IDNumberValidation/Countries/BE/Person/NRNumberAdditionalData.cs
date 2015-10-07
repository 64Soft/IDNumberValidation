using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation.Countries.BE.Person
{
    public class NRNumberAdditionalData : IAdditionalData, IHasBirthDate, IHasGender
    {
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
    }
}
