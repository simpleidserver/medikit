using Medikit.Mobile.ViewModels;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Medikit.Mobile.Controls
{
    public class StepProgressBarControl : StackLayout
    {
        private int _nbComponents = 1;
        private StackLayout _navigationLayout;
        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);
        public static readonly BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);
        
        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }


        public StepProgressBarControl()
        {
            Orientation = StackOrientation.Vertical;
            VerticalOptions = LayoutOptions.FillAndExpand;
            _navigationLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 0),
                Spacing = 0
            };
            Children.Add(_navigationLayout);
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            var content = child as StepBarProgressContentControl;
            if (content != null)
            {
                var vm = content.BindingContext as BaseStepProgressBarViewModel;
                if (vm != null)
                {
                    vm.NextStepNavigated += HandleNextStep;
                    vm.PreviousStepNavigated += HandlePreviousStep;
                }

                var buttonContainer = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Start
                };
                var noEditImage = new Image
                {
                    Source = ImageSource.FromFile("emptyradio"),
                    IsVisible = false,
                    HorizontalOptions = LayoutOptions.Center
                };
                var editImage = new Image
                {
                    Source = ImageSource.FromFile("radio"),
                    IsVisible = false,
                    HorizontalOptions = LayoutOptions.Center
                };
                var completeImage = new Image
                {
                    Source = ImageSource.FromFile("checkmark"),
                    IsVisible = false,
                    HorizontalOptions = LayoutOptions.Center
                };
                var title = new Label
                {
                    Text = content.Title,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                buttonContainer.Children.Add(noEditImage);
                buttonContainer.Children.Add(editImage);
                buttonContainer.Children.Add(completeImage);
                buttonContainer.Children.Add(title);
                _navigationLayout.Children.Add(buttonContainer);
                if (_nbComponents == StepSelected)
                {
                    editImage.IsVisible = true;
                }
                else if (_nbComponents < StepSelected)
                {
                    completeImage.IsVisible = true;
                }
                else
                {
                    noEditImage.IsVisible = true;
                }

                if (_nbComponents < Steps)
                {
                    var separatorLine = new BoxView()
                    {
                        BackgroundColor = Color.Silver,
                        HeightRequest = 1,
                        WidthRequest = 5,
                        Margin = new Thickness(0, 20, 0, 0),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    _navigationLayout.Children.Add(separatorLine);
                }

                if (StepSelected != _nbComponents)
                {
                    content.IsVisible = false;
                }

                _nbComponents++;
            }
        }

        private void HandlePreviousStep(object sender, System.EventArgs e)
        {
            StepSelected--;
            UpdateNavigation();
        }

        private void HandleNextStep(object sender, System.EventArgs e)
        {
            StepSelected++;
            UpdateNavigation();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        private void UpdateNavigation()
        {
            var buttonContainerLst = _navigationLayout.Children.Select(_ => _ as StackLayout).Where(_ => _ != null);
            var children = Children.Select(_ => _ as StepBarProgressContentControl).Where(_ => _ != null);
            int i = 1;
            foreach(var buttonContainer in buttonContainerLst)
            {
                var noEditImage = buttonContainer.Children.ElementAt(0);
                var editImage = buttonContainer.Children.ElementAt(1);
                var completeImage = buttonContainer.Children.ElementAt(2);
                noEditImage.IsVisible = false;
                editImage.IsVisible = false;
                completeImage.IsVisible = false;
                if (i == StepSelected)
                {
                    editImage.IsVisible = true;
                }
                else if (i < StepSelected)
                {
                    completeImage.IsVisible = true;
                }
                else
                {
                    noEditImage.IsVisible = true;
                }

                i++;
            }

            i = 1;
            foreach(var child in children)
            {
                if (i == StepSelected)
                {
                    child.IsVisible = true;
                }
                else
                {
                    child.IsVisible = false;
                }

                i++;
            }
        }
    }
}
