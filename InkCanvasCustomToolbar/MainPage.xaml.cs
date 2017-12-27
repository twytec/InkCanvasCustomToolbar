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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace InkCanvasCustomToolbar
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            CreatePencil();
            CreateStencil();
        }

        #region Create Pencil

        Windows.UI.Input.Inking.InkDrawingAttributes _ballPencilAttributes;
        Windows.UI.Input.Inking.InkDrawingAttributes _pencilAttributes;
        Windows.UI.Input.Inking.InkDrawingAttributes _highlighterAttributes;

        public Color BallPencilStrokeColor
        {
            get { return _ballPencilAttributes.Color; }
            set { _ballPencilAttributes.Color = value; UpdateInkAttributes(_ballPencilAttributes); }
        }

        void CreatePencil()
        {
            myInkCanvas.InkPresenter.InputDeviceTypes =
                Windows.UI.Core.CoreInputDeviceTypes.Mouse | Windows.UI.Core.CoreInputDeviceTypes.Pen | Windows.UI.Core.CoreInputDeviceTypes.Touch;

            _ballPencilAttributes = new Windows.UI.Input.Inking.InkDrawingAttributes() {
                Color = Colors.Black,
                DrawAsHighlighter = false,
                FitToCurve = true,
                IgnorePressure = false,
                IgnoreTilt = true,
                PenTip = Windows.UI.Input.Inking.PenTipShape.Circle,
                PenTipTransform = new System.Numerics.Matrix3x2(1, 0, 0, 1, 0, 0),
                Size = new Size(4, 4)
            };

            _pencilAttributes = Windows.UI.Input.Inking.InkDrawingAttributes.CreateForPencil();

            _highlighterAttributes = new Windows.UI.Input.Inking.InkDrawingAttributes() {
                Color = Colors.Yellow,
                DrawAsHighlighter = true,
                FitToCurve = true,
                IgnorePressure = false,
                IgnoreTilt = true,
                PenTip = Windows.UI.Input.Inking.PenTipShape.Rectangle,
                PenTipTransform = new System.Numerics.Matrix3x2(1, 0, 0, 1, 0, 0),
                Size = new Size(12, 36)
            };

            //Activate BallPencil
            myInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(_ballPencilAttributes);
        }

        #endregion
        
        #region InkToggleBtn_Click

        private void InkToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                var unselectedBtn = this.Resources["BtnNeutral"] as Style;
                var selectedBtn = this.Resources["BtnDefault"] as Style;

                btnBallPencil.Style = unselectedBtn;
                btnEraser.Style = unselectedBtn;
                btnHighlighter.Style = unselectedBtn;
                btnPencil.Style = unselectedBtn;
                btn.Style = selectedBtn;

                if (btn == btnBallPencil)
                {
                    UpdateInkAttributes(_ballPencilAttributes);
                }
                else if (btn == btnPencil)
                {
                    UpdateInkAttributes(_pencilAttributes);
                }
                else if (btn == btnHighlighter)
                {
                    UpdateInkAttributes(_highlighterAttributes);
                }
                else if (btn == btnEraser)
                {
                    myInkCanvas.InkPresenter.InputProcessingConfiguration.Mode = Windows.UI.Input.Inking.InkInputProcessingMode.Erasing;
                }
            }
        }

        void UpdateInkAttributes(Windows.UI.Input.Inking.InkDrawingAttributes attr)
        {
            myInkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(attr);
            myInkCanvas.InkPresenter.InputProcessingConfiguration.Mode = Windows.UI.Input.Inking.InkInputProcessingMode.Inking;
        }


        #endregion

        #region Create Stencil

        Windows.UI.Input.Inking.InkPresenterRuler inkPresenterRuler;
        Windows.UI.Input.Inking.InkPresenterProtractor inkPresenterProtractor;

        void CreateStencil()
        {
            inkPresenterProtractor = new Windows.UI.Input.Inking.InkPresenterProtractor(myInkCanvas.InkPresenter);
            inkPresenterRuler = new Windows.UI.Input.Inking.InkPresenterRuler(myInkCanvas.InkPresenter);
        }

        #endregion

        #region BtnToggleStencil_Click

        private void BtnToggleStencil_Click(object sender, RoutedEventArgs e)
        {
            if (inkPresenterProtractor != null && inkPresenterRuler != null)
            {
                var unselectedBtn = this.Resources["BtnNeutral"] as Style;
                var selectedBtn = this.Resources["BtnDefault"] as Style;

                btnStencil.Style = selectedBtn;

                if (inkPresenterRuler.IsVisible == false && inkPresenterProtractor.IsVisible == false)
                {
                    inkPresenterRuler.IsVisible = true;
                }
                else if (inkPresenterRuler.IsVisible == true && inkPresenterProtractor.IsVisible == false)
                {
                    inkPresenterRuler.IsVisible = false;
                    inkPresenterProtractor.IsVisible = true;
                }
                else if (inkPresenterRuler.IsVisible == false && inkPresenterProtractor.IsVisible == true)
                {
                    inkPresenterRuler.IsVisible = false;
                    inkPresenterProtractor.IsVisible = false;
                    btnStencil.Style = unselectedBtn;
                }
            }
        }

        #endregion
    }
}
