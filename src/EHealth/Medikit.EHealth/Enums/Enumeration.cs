// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Medikit.EHealth.Enums
{
    public class Enumeration : IComparable
    {
        public Enumeration(int value, string code, string description)
        {
            Value = value;
            Code = code;
            Description = description;
        }

        public int Value { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Value.Equals(otherValue.Value);
            return typeMatches && valueMatches;
        }

        public static T Get<T>(string code) where T : Enumeration
        {
            var enumerations = GetAll<T>();
            return enumerations.FirstOrDefault(_ => _.Code == code);
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static bool TryParse<T>(int id, out T result) where T : Enumeration
        {
            var r = Parse<T>(id);
            if (r == null)
            {
                result = null;
                return false;
            }

            result = r;
            return true;
        }

        public static T Parse<T>(int val) where T : Enumeration
        {
            return Enumeration.GetAll<T>().FirstOrDefault(_ => _.Value == val);
        }

        public int CompareTo(object other) => Value.CompareTo(((Enumeration)other).Value);

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Code, Description);
        }
    }
}
