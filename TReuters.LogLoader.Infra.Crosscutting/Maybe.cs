﻿using NullGuard;
using System;
using System.Collections.Generic;
using System.Text;

namespace TReuters.LogLoader.Infra.Crosscutting
{
    public struct Maybe<T>
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                Contracts.Require(HasValue);

                return _value;
            }
        }

        public bool HasValue
        {
            get { return _value != null; }
        }

        public bool HasNoValue
        {
            get { return !HasValue; }
        }

        private Maybe([AllowNull] T value)
        {
            _value = value;
        }

        public static implicit operator Maybe<T>([AllowNull] T value)
        {
            return new Maybe<T>(value);
        }
    }
}
