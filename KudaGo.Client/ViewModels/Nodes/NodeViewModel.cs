using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels.Nodes
{
    abstract class NodeViewModel : PropertyChangedBase
    {
        public abstract long Id { get; protected set; }
        public virtual string Title { get; protected set; }
    }
}
