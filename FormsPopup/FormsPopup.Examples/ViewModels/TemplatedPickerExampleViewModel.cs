using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MWX.XamForms.Popup.Examples.ViewModels
{
    public class TemplatedPickerExampleViewModel : BaseViewModel
    {
        public Command BackCommand { get; set; }

        public List<PickerItem> Items { get; set; }


        public TemplatedPickerExampleViewModel()
        {
            BackCommand = new Command(() => App.BackToExamplePickerPage());
            Items = new List<PickerItem>();

            for (int i = 0; i < 10; i++)
            {
                Items.Add(new PickerItem { Title = $"Title {i}", Description = $"Description {i}" });
            }
        }

        public class PickerItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}
