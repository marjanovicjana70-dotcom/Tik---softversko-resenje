using JanaTakmicenje22.Core.Models;
using JanaTakmicenje22.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JanaTakmicenje22.Views.Pages
{
    public partial class NotesPage : UserControl
    {
        private readonly NotesViewModel _vm;

        public NotesPage(NotesViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        private void NotesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotesList.SelectedItem is Note note)
            {
                _vm.SelectNote(note);
                EmptyState.Visibility = Visibility.Collapsed;
                EditorPanel.Visibility = Visibility.Visible;
            }
        }

        private async void NewNote_Click(object sender, RoutedEventArgs e)
        {
            await _vm.NewNoteCommand.ExecuteAsync(null);
            EmptyState.Visibility = Visibility.Collapsed;
            EditorPanel.Visibility = Visibility.Visible;
        }

        private async void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            await _vm.SaveNoteCommand.ExecuteAsync(null);
        }

        private async void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Želiš da obrišeš ovu belešku trajno?", "Potvrda",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await _vm.DeleteNoteCommand.ExecuteAsync(null);
                if (!_vm.HasSelectedNote)
                {
                    EmptyState.Visibility = Visibility.Visible;
                    EditorPanel.Visibility = Visibility.Collapsed;
                }
            }
        }
        public async Task RefreshAsync()
        {
            if (_vm != null)
            {
                await _vm.LoadAsync();
                var binding = NotesList.GetBindingExpression(ItemsControl.ItemsSourceProperty);
                this.DataContext = null;
                this.DataContext = _vm;
                if (_vm.SelectedNote != null)
                {
                    EmptyState.Visibility = Visibility.Collapsed;
                    EditorPanel.Visibility = Visibility.Visible;
                }
            }
        }
    }

}
