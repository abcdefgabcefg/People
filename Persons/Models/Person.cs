using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persons.Models
{
    public class Person : IEquatable<Person>, IComparable<Person>
    {
        public int PersonId { get; set; }

        private string firstName;
        private string lastName;

        [Required(ErrorMessage = errorMessageRequired)]
        [Display(Name ="First Name")]
        [MaxLength(40, ErrorMessage =errorMessageMaxLenght)]
        [RegularExpression(regExp, ErrorMessage =errorMessageRegExp)]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value.Trim();
                firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();
            }
        }

        [Required(ErrorMessage = errorMessageRequired)]
        [Display(Name = "Last Name")]
        [MaxLength(40, ErrorMessage = errorMessageMaxLenght)]
        [RegularExpression(regExp, ErrorMessage = errorMessageRegExp)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value.Trim();
                lastName = lastName.Substring(0, 1).ToUpper() + lastName.Substring(1).ToLower();
            }
        }

        private const string errorMessageRequired = "It's required field";
        private const string errorMessageMaxLenght = "Max lenght is 40 letters";
        private const string regExp = @"^[ ]*[a-zA-z]+[ ]*$";
        private const string errorMessageRegExp = "Only letters allowed";

        public bool Equals(Person other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.PersonId == PersonId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(other as Person);
        }

        public override int GetHashCode()
        {
            return PersonId.GetHashCode();
        }

        public int CompareTo(Person other)
        {
            if (other == null)
            {
                return 1;
            }
            return PersonId.CompareTo(other.PersonId);
        }

        public static bool operator ==(Person fPerson, Person sPerson)
        {
            if (ReferenceEquals(null, fPerson))
            {
                if (ReferenceEquals(null, sPerson))
                {
                    return true;
                }
                return false;
            }
            return fPerson.Equals(sPerson);
        }

        public static bool operator !=(Person fPerson, Person sPerson)
        {
            return !(fPerson == sPerson);
        }

        public static bool operator >(Person fPerson, Person sPerson)
        {
            return fPerson.CompareTo(sPerson) == 1;
        }

        public static bool operator <(Person fPerson, Person sPerson)
        {
            return fPerson.CompareTo(sPerson) == -1;
        }

        public static bool operator >=(Person fPerson, Person sPerson)
        {
            return fPerson.CompareTo(sPerson) >= 0;
        }

        public static bool operator <=(Person fPerson, Person sPerson)
        {
            return fPerson.CompareTo(sPerson) <= 0;
        }


    }
}