using MWX.XamForms.Popup.Examples.Pages;
using Xamarin.Forms;

namespace MWX.XamForms.Popup.Examples
{
    public enum ExamplePage
    {
        CodedPopupExample,
        CodedSimpleExample,
        NavigationPage,
        ComplexLayoutExample,
        TemplatedPicker,
    }

    public class App : Application
    {
        static ExamplePicker pickerPage = new ExamplePicker();
        public App()
        {
            /*
             *  Try these:
             *  MainPage = new CodedPopupExample();
             *  MainPage = new CodedSimpleExample();
             *  MainPage = new NavigationPage(new NavigationExample());
             *  MainPage = new ComplexLayoutExample();
             */

            MainPage = pickerPage;
        }

        public static void BackToExamplePickerPage()
        {
            Device.BeginInvokeOnMainThread(() => App.Current.MainPage = pickerPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
