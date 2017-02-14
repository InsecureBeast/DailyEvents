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
            if (string.IsNullOrEmpty(UserImage))
                UserImage = "https://lh6.googleusercontent.com/kfRPEQ7Wuqw7Kqrwh_bfjBgnueQBj16mMQ1X3Mzy1ZKY7hRJCh-BiHW9s84TmfBDDqcm8emwE66fWkI=w1920-h950";
            
            UserName = result.User.DisplayName;
        }

        public string Text { get; private set; }
        public string UserImage { get; private set; }
        public string UserName { get; private set; }
    }
}
