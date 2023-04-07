using System.Runtime.Serialization;

namespace HRManagement.CustomExceptions
{
    [Serializable]
    public class CouldNotUpdateUserException : CustomException
    {
        public CouldNotUpdateUserException() : base()
        {

        }

        public CouldNotUpdateUserException(string message) : base(message)
        {

        }
        protected CouldNotUpdateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
