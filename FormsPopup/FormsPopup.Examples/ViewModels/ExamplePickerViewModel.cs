using MWX.XamForms.Popup.Examples.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MWX.XamForms.Popup.Examples.ViewModels
{
    public class ExamplePickerViewModel : BaseViewModel
    {
        public PickerItem[] Items { get; set; }

        private PickerItem selectedItem;

        public PickerItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; SwitchPage(); }
        }

        public ExamplePickerViewModel()
        {
            Items = new PickerItem[]
            {
                new PickerItem
                {
                    Index = 2,
                    Name = "CodedSimpleExample",
                    Description = "very simple Example",
                    Page2Show = new CodedSimpleExample()
                },
                new PickerItem
                {
                    Index = 1,
                    Name = "CodedPopupExample",
                    Description = "Sample with coded Content",
                    Page2Show = new CodedPopupExample()
                },
                new PickerItem
                {
                    Index = 3,
                    Name = "ComplexLayoutExample",
                    Description = "more Complex Content Example",
                    Page2Show = new ComplexLayoutExample()
                },
                new PickerItem
                {
                    Index = 3,
                    Name = "NavigationExample",
                    Description = "Example in an NavigationPage",
                    Page2Show = new NavigationPage(new NavigationExample())
                },
                new PickerItem
                {
                    Index = 4,
                    Name = "TemplatedPicker",
                    Description = "Example for the TemplatedPicker",
                    Page2Show = new TemplatedPickerExample()

                },
                new PickerItem
                {
                    Index = 4,
                    Name = "Picker in ListView",
                    Description = "Example for the TemplatedPicker in a ListView",
                    Page2Show = new TemplatedPickerList()

                }
            };
        }
        private void SwitchPage()
        {
            if (selectedItem == null) return;

            // that can cause an endless loop ! be careful ;)
            // OnPropertyChanged(nameof(SelectedItem));

            Task.Run(async () =>
            {
                await Task.Delay(200);
                if (selectedItem.Page2Show != null)
                {
                    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = selectedItem.Page2Show);
                }
            });
        }
    }

    public class PickerItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Page Page2Show { get; set; }
    }

}
