using HRManagement.CustomExceptions;

namespace HRManagement.ValidateData
{
    public static class ValidateOnNull<T>
    {
        public static void ValidateDataOnNull(T input)
        {
            if (input == null)
            {
                throw new DoesNotExistsException();
            }
        }
    }
}
