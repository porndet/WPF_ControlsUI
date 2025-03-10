using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2__.NET_Framework_.ViewModel
{
    public class GetDataFolderItemNo
    {
        public ObservableCollection<FolderItemsDayOfWeeks> ItemsDayOfWeeks { get; set; } =
                new ObservableCollection<FolderItemsDayOfWeeks>();

        public event EventHandler ChangeItemsDayOfWeek;
        public GetDataFolderItemNo()
        {
            foreach (var item in Directory.GetDirectories(@"48821-3431"))
            {
                ItemsDayOfWeeks.Add(new FolderItemsDayOfWeeks()
                {
                    CheckedFile = false,
                    Name = Path.GetFileName(item),
                });

                ItemsDayOfWeeks.Last().PropertyChanged += GetDataFolderItemNo_PropertyChanged;
            }
        }

        private void GetDataFolderItemNo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ChangeItemsDayOfWeek?.Invoke(this, EventArgs.Empty);
        }
    }

    public class FolderItemsDayOfWeeks : INotifyPropertyChanged
    {
        private bool checkedFile { get; set; }
        public bool CheckedFile
        {
            get { return checkedFile; }
            set
            {
                checkedFile = value;
                OnPropertyChanged("CheckedFile");
            }
        }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
