using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    public class ZoomImageBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        private RotateTransform GetRotateTransform(UIElement element)
        {
            return (RotateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is RotateTransform);
        }

        private MatrixTransform GetMatrixTransform(UIElement element)
        {
            return (MatrixTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is MatrixTransform);
        }

        public int paramangle
        {
            get { return (int)GetValue(paramangleProperty); }
            set { SetValue(paramangleProperty, value); }
        }

        public static readonly DependencyProperty paramangleProperty =
            DependencyProperty.Register("paramangle", typeof(int), typeof(ZoomImageBorder), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(child_changeValue_2)));

        private Point? mousePos;

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            this.IsManipulationEnabled = true;
            this.ClipToBounds = true;

            if (this.child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);

                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);

                RotateTransform rt = new RotateTransform();
                group.Children.Add(rt);

                MatrixTransform mt = new MatrixTransform();
                group.Children.Add(mt);

                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.5, 0.5);

                this.MouseWheel += child_MouseWheel;
                this.MouseLeftButtonDown += child_MouseLeftButtonDown;
                this.MouseLeftButtonUp += child_MouseLeftButtonUp;
                this.MouseMove += child_MouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(child_PreviewMouseRightButtonDown);
            }
        }

        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        private void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    if (mousePos.HasValue)
                    {
                        var transform = GetMatrixTransform(child);
                        var matrix = transform.Matrix;
                        var pos = e.GetPosition(this);
                        matrix.Translate(pos.X - mousePos.Value.X, pos.Y - mousePos.Value.Y);
                        transform.Matrix = matrix;
                        mousePos = pos;
                    }

                    //var tt = GetTranslateTransform(child);

                    //Vector v = start - e.GetPosition(this);

                    //if (paramangle == 90)
                    //{
                    //    tt.Y = origin.X + v.X;
                    //    tt.X = origin.Y - v.Y;
                    //}
                    //else if(paramangle == 180)
                    //{
                    //    tt.X = v.X + origin.X;
                    //    tt.Y = v.Y + origin.Y;
                    //}
                    //else if(paramangle == 270)
                    //{
                    //    tt.Y = origin.X - v.X;
                    //    tt.X = origin.Y + v.Y;
                    //}
                    //else
                    //{   
                    //    tt.X = origin.X - v.X;
                    //    tt.Y = origin.Y - v.Y;
                    //}
                }
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                mousePos = null;

                //this.Cursor = Cursors.Arrow;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.CaptureMouse();
                mousePos = e.GetPosition(this);

                //var tt = GetTranslateTransform(child);
                //start = e.GetPosition(this);
                //origin = new Point(tt.X, tt.Y);
                //this.Cursor = Cursors.Hand;
                //child.CaptureMouse();
            }
        }

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var transform = GetMatrixTransform(child);
                var matrix = transform.Matrix;
                var pos = e.GetPosition(this);

                if (e.Delta > 0)
                    matrix.ScaleAt(1.1, 1.1, 0, 0);
                else
                    matrix.ScaleAt(1 / 1.1, 1 / 1.1, 0, 0);

                transform.Matrix = matrix;

                //var st = GetScaleTransform(child);
                //var tt = GetTranslateTransform(child);

                //double zoom = e.Delta > 0 ? .2 : -.2;
                //if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                //    return;

                //Point relative = e.GetPosition(child);
                //double absoluteX;
                //double absoluteY;

                //absoluteX = relative.X * st.ScaleX + tt.X;
                //absoluteY = relative.Y * st.ScaleY + tt.Y;

                //st.ScaleX += zoom;
                //st.ScaleY += zoom;

                //tt.X = absoluteX - relative.X * st.ScaleX;
                //tt.Y = absoluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_changeValue(object sender, EventArgs e)
        {
            Console.WriteLine("123");
        }

        private void child_changeValue_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("456");
        }

        private static void child_changeValue_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ZoomImageBorder zoom)
            {
                RotateTransform rotate = zoom.GetRotateTransform(zoom.child);
                rotate.Angle = zoom.paramangle;
            }
        }
    }


}
