using System;
using System.Collections.Generic;

namespace GetADoctor.Models.Utilities
{
    // This is not a table
    public class DoctorComparer : IEqualityComparer<Doctor>
    {
        public bool Equals(Doctor x, Doctor y)
        {
            // Check whether the compared objects reference the same data.
            if (object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            // Check whether the properties are equal.
            var result = x.FirstName == y.FirstName && x.Age == y.Age && x.Gender == y.Gender && x.MobileNumber == y.MobileNumber &&
                          x.UIN == y.UIN && x.Rating == y.Rating;
            return result;
        }

        public int GetHashCode(Doctor record)
        {
            if (object.ReferenceEquals(record, null)) return 0;

            // Get the hash code for the tag field if it is not null.
            var hashTag = record.FirstName?.GetHashCode() ?? 0;

            var hashTagid = 0;
            // Get the hash code for the tagid field.
            if (record.FirstName != null)
            {
                hashTagid += record.FirstName.GetHashCode();
            }

            hashTagid += record.Age.GetHashCode();

            if (record.Rating != null)
            {
                hashTagid += record.Rating.GetHashCode();
            }

            hashTagid += record.Gender.GetHashCode();
            if (record.MobileNumber != null)
            {
                hashTagid += record.MobileNumber.GetHashCode();
            }

            if (record.Speciality != null)
            {
                hashTagid += record.Speciality.GetHashCode();
            }

            // Calculate the hash code for ARecord.
            return hashTag ^ hashTagid;
        }
    }
}
