using System;
using System.Collections.Generic;
using System.Text;

using System.Windows;

namespace AplikacjaNotatki
{
    public partial class AddNoteWindow : Window
    {
        public string NoteContent { get; private set; }

        public AddNoteWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                MessageBox.Show("Treść notatki nie może być pusta!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NoteContent = txtContent.Text;
            DialogResult = true;
            Close();
        }
    }
}
