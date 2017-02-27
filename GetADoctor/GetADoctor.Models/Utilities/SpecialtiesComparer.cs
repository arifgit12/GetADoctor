using System;
using System.Collections.Generic;

namespace GetADoctor.Models.Utilities
{
    public class SpecialtiesComparer : IEqualityComparer<Speciality>
    {
        public bool Equals(Speciality x, Speciality y)
        {
            // Check whether the compared objects reference the same data.
            if (object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            // Check whether the properties are equal.
            return x.Name == y.Name;
        }

        public int GetHashCode(Speciality record)
        {
            // Check whether the object is null.
            if (object.ReferenceEquals(record, null)) return 0;

            // Get the hash code for the tag field if it is not null.
            var hashTag = record.Name?.GetHashCode() ?? 0;

            var hashTagid = 0;
            // Get the hash code for the tagid field.
            if (record.Name != null)
            {
                hashTagid = record.Name.GetHashCode();
            }

            // Calculate the hash code for ARecord.
            return hashTag ^ hashTagid;
        }
    }
}
