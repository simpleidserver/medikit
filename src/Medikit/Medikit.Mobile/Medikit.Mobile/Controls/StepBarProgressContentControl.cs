using Xamarin.Forms;

namespace Medikit.Mobile.Controls
{
    public class StepBarProgressContentControl : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(TitleProperty), typeof(string), typeof(StepBarProgressContentControl), "default", defaultBindingMode: BindingMode.TwoWay);
        
        public StepBarProgressContentControl()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
