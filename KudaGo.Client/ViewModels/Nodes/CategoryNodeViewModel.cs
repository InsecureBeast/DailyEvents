using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Categories;

namespace KudaGo.Client.ViewModels.Nodes
{
    class CategoryNodeViewModel : PropertyChangedBase
    {
        private bool _isFree;

        public CategoryNodeViewModel(ICategoriesResponse item)
        {
            if (item == null)
                return;

            Name = GetCategoryName(item.Name);
            OriginalName = item.Name;
            Slug = item.Slug;

            if (Slug == "theater")
                Name = "Театр"; //TODO Localization
            if (Slug == "kids")
                Name = "Детям"; //TODO Localization
            if (Slug == "cinema")
                Name = GetName(item.Name);
            if (Slug == "open")
                Name = "Знания"; //TODO Localization
            if (Slug == "global" || Slug == "yoga" || Slug == "other")
                Name = "Развлечения"; //TODO Localization
            if (Slug == "permanent-exhibitions")
                Name = "Выставки"; //TODO Localization
            if (Slug == "night")
                Name = "Вечеринки"; //TODO Localization
            if (Slug == "discount")
                Name = "Акции"; //TODO Localization
            if (Slug == "presentation")
                Name = "События для бизнеса"; //TODO Localization
        }

        public string Name { get; protected set; }
        public string OriginalName { get; protected set; }
        public string Slug { get; set; }

        public bool IsFree
        {
            get { return _isFree; }
            set
            {
                _isFree = value;
                NotifyOfPropertyChanged(() => IsFree);
            }
        }

        public override string ToString()
        {
            return Name + ":" + Slug;
        }

        private string GetCategoryName(string originalName)
        {
            var start = originalName.IndexOf("(");
            if (start == -1)
                return originalName;

            var name = originalName.Substring(start + 1, originalName.Length -  start - 2);
            return name.Replace("Раздел ", ""); //TODO Localization
        }

        private string GetName(string originalName)
        {
            var start = originalName.IndexOf("(");
            if (start == -1)
                return originalName;

            return originalName.Substring(0, start - 1);
        }
    }

    class AllCategoryNodeViewModel : CategoryNodeViewModel
    {
        public AllCategoryNodeViewModel() : base(null)
        {
            Name = "Все"; //TODO Localization
            Slug = string.Empty;
            OriginalName = "All";
        }
    }
}
