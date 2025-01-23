using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2__.NET_Framework_.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Instance.checkboxA.IsChecked = true;

            //Console.WriteLine(MainWindow.Instance.buttonA.Name);

            //MainWindow.Instance.buttonA.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            //typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod
            //    ("OnClick", BindingFlags.Instance | BindingFlags.NonPublic)
            //    .Invoke(MainWindow.Instance.buttonA, new object[0]);

            //var mainWin = Application.Current.Windows[0] as MainWindow;

            //if (mainWin != null) {
            //    Console.WriteLine(mainWin.buttonA.Name);
            //}
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            popupShow.IsOpen = true;

            Console.WriteLine("MouseEnter Rectangle");
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!popupShow.IsMouseOver)
            {
                Console.WriteLine("MouseLeave Rectangle");
                popupShow.IsOpen = false;
            }
        }

        private void popupShow_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RectangleBox.IsMouseOver)
            {
                Console.WriteLine("MouseLeave Popup");
                popupShow.IsOpen = false;
            }
        }

        private void RectangleBox_MouseMove(object sender, MouseEventArgs e)
        {
            var mousemove = e.GetPosition(RectangleBox);

            popupShow.VerticalOffset = mousemove.Y + 15;
            popupShow.HorizontalOffset = mousemove.X + 10;

            Console.WriteLine(mousemove.X + " " + mousemove.Y);
        }
    }
}
