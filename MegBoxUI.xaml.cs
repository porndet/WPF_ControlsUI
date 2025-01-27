using System.Windows;
using System.Windows.Media;

namespace WpfApp2__.NET_Framework_
{
    public partial class MegBoxUI : Window
    {
        public MegBoxUI()
        {
            InitializeComponent();

            int type = 2;
            bool OKCancel = false;

            if(type == 1)
            {
                BdIcon.Background = this.FindResource("SuccessColor_Gradient") as GradientBrush;
                BdText.Background = this.FindResource("SuccessColor_Gradient") as GradientBrush;
                TitleText.Foreground = this.FindResource("SuccessColor_Text") as Brush;
                TitleText.Text = "Success";
                iconImg.Data = this.FindResource("circle-check") as Geometry;
            }
            else if(type == 2)
            {
                BdIcon.Background = this.FindResource("ErrorColor_Gradient") as GradientBrush;
                BdText.Background = this.FindResource("ErrorColor_Gradient") as GradientBrush;
                TitleText.Foreground = this.FindResource("ErrorColor_Text") as Brush;
                TitleText.Text = "Error";
                iconImg.Data = this.FindResource("circle-xmark") as Geometry;
            }
            else
            {
                BdIcon.Background = this.FindResource("QuestionColor_Gradient") as GradientBrush;
                BdText.Background = this.FindResource("QuestionColor_Gradient") as GradientBrush;
                TitleText.Foreground = this.FindResource("QuestionColor_Text") as Brush;
                TitleText.Text = "Question";
                iconImg.Data = this.FindResource("circle-question") as Geometry;
            }

            if(!OKCancel)
            {
                Btnok.Margin = new Thickness(0, 0, 0, 0);
                Btncancel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
