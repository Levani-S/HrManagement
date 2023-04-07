using System.Runtime.Serialization;

namespace HRManagement.CustomExceptions
{
    [Serializable]
    public class CouldNotRegisterUserException : CustomException
    {
        public CouldNotRegisterUserException() : base()
        {

        }

        public CouldNotRegisterUserException(string message) : base(message)
        {

        }
        protected CouldNotRegisterUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
