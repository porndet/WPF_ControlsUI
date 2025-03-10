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
using System.Windows.Shapes;
using WpfApp2__.NET_Framework_.ViewModel;

namespace WpfApp2__.NET_Framework_
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            var GetData = new GetDataFolderItemNo();
            GetData.ChangeItemsDayOfWeek += GetData_ChangeItemsDayOfWeek;
            this.DataContext = GetData;

            InitializeComponent();
        }

        private void GetData_ChangeItemsDayOfWeek(object sender, EventArgs e)
        {
            if (sender is GetDataFolderItemNo DataFolder)
            {
                foreach(var item in DataFolder.ItemsDayOfWeeks.Where(x => x.CheckedFile))
                {
                    Console.WriteLine(item.Name + " " + item.CheckedFile);
                }
            }
        }
    }
}
