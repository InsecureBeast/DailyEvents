using DailyEvents.Core.Data.JData;

namespace DailyEvents.Core.Data
{
    public interface IUser
    {
        string DisplayName { get; }
        string Avatar { get; }
    }

    internal class User : IUser
    {
        public User(JUser jUser)
        {
            if (jUser == null)
                return;

            DisplayName = jUser.Name;
            Avatar = jUser.Avatar;
        }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
    }
}
