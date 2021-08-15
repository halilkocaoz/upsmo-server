using System;

namespace UpMo.Entities
{
    public abstract class BaseEntity<T> where T : struct
    {
        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        // Uppercase because of Golang lint :)
        public T ID { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; set; }
    }
}