using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KudaGo.Client.ViewModels
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyOfPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.NotifyOfPropertyChanged(propertyExpression, PropertyChanged);
        }
    }

    public static class PropertyExtensions
    {
        public static void NotifyOfPropertyChanged<T>(this INotifyPropertyChanged sender, Expression<Func<T>> propertyExpression, PropertyChangedEventHandler eventHandler)
        {
            if (eventHandler == null)
                return;

            var body = (MemberExpression)propertyExpression.Body;
            eventHandler(sender, new PropertyChangedEventArgs(body.Member.Name));
        }
    }
}
