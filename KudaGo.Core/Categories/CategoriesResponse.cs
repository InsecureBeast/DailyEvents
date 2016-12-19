using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Categories
{
    public interface ICategoriesResponse
    {
        string Slug { get; }
        string Name { get; }
    }

    class CategoriesResponse : ICategoriesResponse
    {
        public CategoriesResponse(JCategoriesResponse jResponse)
        {
            Slug = string.Empty;
            Name = string.Empty;

            if (jResponse == null)
                return;

            Slug = jResponse.Slug;
            Name = jResponse.Name;
        }

        public string Slug { get; private set; }
        public string Name { get; private set; }
    }
}
