using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace KG.Weather.Data.Models
{
    [DebuggerDisplay("{DisplayName} - {Value}")]
    [DataContract(Namespace = "http://github.com/HeadspringLabs/Enumeration/5/13")]
    public abstract class Enumeration<TEnumeration, TValue> :
        IComparable<TEnumeration>, IEquatable<TEnumeration>
    where TEnumeration : Enumeration<TEnumeration, TValue>
    where TValue : IComparable
    {
        private static readonly Lazy<TEnumeration[]> Enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        [DataMember(Order = 0)]
        private TValue value;

        [DataMember(Order = 1)]
        private string displayName;

        protected Enumeration()
        {
        }

        protected Enumeration(TValue value, string displayName)
        {
            this.value = value;
            this.displayName = displayName;
        }

        public TValue Value
        {
            get
            {
                return this.value;
            }
            // Entity Framework will only retrieve and set the value.
            // Use this setter to find the corresponding display name as defined in the static fields.
            protected set
            {
                this.value = value;

                this.displayName = FromValue(value).DisplayName;
            }
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public int CompareTo(TEnumeration other)
        {
            return Value.CompareTo(other == default(TEnumeration) ? default(TValue) : other.Value);
        }

        public override sealed string ToString()
        {
            return DisplayName;
        }

        public static TEnumeration[] GetAll()
        {
            return Enumerations.Value;
        }

        private static TEnumeration[] GetEnumerations()
        {
            Type enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TEnumeration);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && ValueEquals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return !Equals(left, right);
        }

        public static implicit operator TValue(Enumeration<TEnumeration, TValue> enumObj)
        {
            return enumObj.Value;
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(
                value,
                "value",
                item => item.Value?.Equals(value) ?? (value == null));
        }

        public static TEnumeration Parse(string displayName)
        {
            return Parse(displayName, "display name", item => item.DisplayName == displayName);
        }

        static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {

            if (!TryParse(predicate, out TEnumeration result))
            {
                string message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(TEnumeration));
                throw new ArgumentException(message, "value");
            }

            return result;
        }

        public static bool TryParse(TValue value, out TEnumeration result)
        {
            return TryParse(e => e.ValueEquals(value), out result);
        }

        public static bool TryParse(string displayName, out TEnumeration result)
        {
            return TryParse(e => e.DisplayName == displayName, out result);
        }

        protected virtual bool ValueEquals(TValue value)
        {
            return Value.Equals(value);
        }        
    }

    public abstract class Enumeration<TEnumeration> : Enumeration<TEnumeration, string>
        where TEnumeration : Enumeration<TEnumeration>
    {
        public Enumeration() { }

        public Enumeration(string value)
            : base(value, value) { }

        public Enumeration(string value, string displayName)
            : base(value, displayName) { }
    }
}
