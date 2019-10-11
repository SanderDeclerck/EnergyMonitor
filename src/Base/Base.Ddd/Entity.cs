﻿using System;

namespace Invoicing.Base.Ddd
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        public Guid Id { get; protected set; }

        public bool IsTransient()
        {
            return Id == default(Guid);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity item))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            if (this.IsTransient() || item.IsTransient())
            {
                return false;
            }

            return this.Id == item.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }

                return _requestedHashCode.Value;
            }

            return base.GetHashCode();
        }
    }
}
