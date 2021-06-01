using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace HOI4_ModificationsConstructor
{
    public class DefaultDialogService : IDialogService
    {
        public string FolderPath { get; set; }

        public bool OpenFileDialog()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = @"C:\\Users",
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = @"C:\\Users",
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FolderPath = dialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}