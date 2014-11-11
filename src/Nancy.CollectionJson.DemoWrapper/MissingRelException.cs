using System;

namespace Nancy.CollectionJson.DemoWrapper
{
    public class MissingRelException : Exception
    {
        public MissingRelException(string message) : base(message) { }
    }
}