using System;

namespace ZPastel.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(Type model, string keyValue, string keyName)
            : base($"{model.Name} with {keyName} [{keyValue}] not found")
        {
        }
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string keyValue, string keyName)
            : base(typeof(T), keyValue, keyName)
        {
        }
    }
}
