﻿using Xamarin.Forms;

namespace MWX.XamForms.Popup.Examples
{
    public class CodedSimpleExample : ContentPage
    {
        public CodedSimpleExample()
        {
            var popup = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.5,
                ContentHeightRequest = 0.8,
                ContentWidthRequest = 0.8,
                Padding = 10,
                Body = new ContentView
                {
                    BackgroundColor = Color.White,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            new Entry(),
                            new Label
                            {
                                VerticalTextAlignment = TextAlignment.Center,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.Black,
                                Text = "Hello, World!"
                            }
                        }
                    }
                }
            };

            popup.Tapped += (sender, args) =>
            {
                args.Popup.Hide();
            };


			var button = new Button { Text = "Show Popup" };
            button.Clicked += (s, e) => popup.Show();

            var backButton = new Button { Text = "Back To Examples" };
            backButton.Clicked += (s, e) => App.BackToExamplePickerPage();

            var stack = new StackLayout
            {
                Children =
                {
                    new Entry { Text = "Some input..."},
                    button,
                    backButton
                }
            };

			Device.OnPlatform(() => stack.Padding = new Thickness(0, 25, 0, 0));
			Content = stack;
			new PopupPageInitializer(this) { popup };
		}
    }
}