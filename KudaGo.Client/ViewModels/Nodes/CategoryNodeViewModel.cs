using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Categories;

namespace KudaGo.Client.ViewModels.Nodes
{
    class CategoryNodeViewModel
    {
        public CategoryNodeViewModel(ICategoriesResponse item)
        {
            if (item == null)
                return;

            Name = item.Name;
            Slug = item.Slug;
        }

        public string Name { get; private set; }
        public string Slug { get; private set; }

        public override string ToString()
        {
            return Name + " : " + Slug;
        }
    }
}
