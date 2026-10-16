using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using AplikacjaNotatki;

namespace Notatki16102026
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Note> notes;
        private readonly string defaultFilePath = "notes.json";

        public MainWindow()
        {
            InitializeComponent();
            LoadNotesOnStartup();
            lstNotes.ItemsSource = notes;
        }

        private void LoadNotesOnStartup()
        {
            if (File.Exists(defaultFilePath))
            {
                try
                {
                    string json = File.ReadAllText(defaultFilePath);
                    var loadedNotes = JsonSerializer.Deserialize<ObservableCollection<Note>>(json);
                    if (loadedNotes != null)
                    {
                        notes = loadedNotes;
                        return;
                    }
                }
                catch { }
            }
            notes = new ObservableCollection<Note>();
        }

        private void SaveDefaultNotes()
        {
            try
            {
                string json = JsonSerializer.Serialize(notes);
                File.WriteAllText(defaultFilePath, json);
            }
            catch { }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNoteWindow addWindow = new AddNoteWindow();
            addWindow.Owner = this;
            if (addWindow.ShowDialog() == true)
            {
                notes.Add(new Note { Content = addWindow.NoteContent });
                SaveDefaultNotes();
            }
        }

        private void BtnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Plik JSON (*.json)|*.json|Plik tekstowy (*.txt)|*.txt",
                DefaultExt = "json",
                FileName = "mojenotatki"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = JsonSerializer.Serialize(notes, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(saveFileDialog.FileName, json);
                    MessageBox.Show("Notatki zostały pomyślnie zapisane do pliku.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas zapisu pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}