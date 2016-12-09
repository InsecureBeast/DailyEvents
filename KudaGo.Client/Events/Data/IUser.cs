using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.Events.Data
{
    public interface IUser
    {
        string Display_Name { get; }
        string Avatar { get; }
    }

    internal class User : IUser
    {
        public string Display_Name { get; set; }
        public string Avatar { get; set; }
    }
}
