using System.Runtime.Serialization;

namespace HRManagement.CustomExceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException() : base()
        {

        }

        public CustomException(string message) : base(message)
        {

        }
        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}