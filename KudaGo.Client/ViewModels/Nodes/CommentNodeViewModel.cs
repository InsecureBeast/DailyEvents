using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Comments;

namespace KudaGo.Client.ViewModels.Nodes
{
    class CommentNodeViewModel
    {
        public CommentNodeViewModel(ICommentResult result)
        {
            if (result == null)
                return;

            Text = result.Text;
            UserImage = result.User.Avatar;
            UserName = result.User.DisplayName;
        }

        public string Text { get; private set; }
        public string UserImage { get; private set; }
        public string UserName { get; private set; }
    }
}
