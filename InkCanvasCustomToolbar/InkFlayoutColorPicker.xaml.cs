using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace InkCanvasCustomToolbar
{
    public sealed partial class InkFlayoutColorPicker : UserControl
    {
        public InkFlayoutColorPicker()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register(nameof(StrokeColor), typeof(Color), typeof(InkFlayoutColorPicker), new PropertyMetadata(Colors.Black));

        public Color StrokeColor
        {
            get
            {
                return (Color)GetValue(StrokeColorProperty);
            }
            set
            {
                SetValue(StrokeColorProperty, value);
            }
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            StrokeColor = args.NewColor;
        }
    }
}
