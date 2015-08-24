using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniRitter.UniRitter2015.Specs.Models
{
    public class Person : IModel
    {
        public Guid? id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string url { get; set; }

        public bool Equals(Person other)
        {
            if (other == null) return false;

            return
                id == other.id
                && firstName == other.firstName
                && lastName == other.lastName
                && email == other.email
                && url == other.url;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                return Equals(obj as Person);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        Guid? IModel.GetId()
        {
            return id;
        }

        bool IModel.AttributeEquals(IModel other)
        {
            if (other == null ||
               other.GetType() != typeof(Person)) return false;

            var person = (Person)other;
            return
                firstName == person.firstName
                && lastName == person.lastName
                && email == person.email
                && url == person.url;
        }

        bool IModel.IsEquals(IModel other)
        {
            if (other == null ||
               other.GetType() != typeof(Person)) return false;

            var person = (Person)other;
            return
                id == person.id
                && firstName == person.firstName
                && lastName == person.lastName
                && email == person.email
                && url == person.url;
        }
    }
}