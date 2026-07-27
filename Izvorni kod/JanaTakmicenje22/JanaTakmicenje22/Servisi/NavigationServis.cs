using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanaTakmicenje22.Servisi
{
    public class NavigationServis : ObservableObject
    {
        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            private set => SetProperty(ref _currentView, value);
        }

        private readonly Dictionary<string, Func<object>> _viewFactory = new();

        public void Register(string key, Func<object> factory)
        {
            _viewFactory[key] = factory;
        }

        public void NavigateTo(string key)
        {
            if (_viewFactory.TryGetValue(key, out var factory))
                CurrentView = factory();
        }

    }
}
