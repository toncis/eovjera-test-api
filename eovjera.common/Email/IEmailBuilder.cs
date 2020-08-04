namespace eOvjera.Common.Email
{
    /// <summary>
    /// Alter mail message before sending through EmailContainer
    /// </summary>
    public interface IEmailBuilder
    {
        object Build(EmailMessage mail);
    }
}
