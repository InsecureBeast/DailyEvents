using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Core
{
    public enum TextFormatEnum
    {
        Html,// - текст с тэгами
        Plain, // - текст без тегов, ссылки <a href=a_href>a_title</a> преобразуются в вид: a_title a_href
        Text// - текст без тегов, от ссылок остается только название
    }
}
