using System.Runtime.Serialization;

namespace Итоговая_WinForms_Луков_Павел.Interfaces
{
    public class UpdateEventArgs : IValidating
    {
        public bool IsValidated { get; }

        public UpdateEventArgs(bool isValidated)
        {
            IsValidated = isValidated;
        }
    }

    [DataContract]
    public abstract  class Updating
    {
        public delegate void UpdatingHandler(object sender, UpdateEventArgs e);
    }
}
