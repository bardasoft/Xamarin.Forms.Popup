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


        private PickerItemEntryInList[] _SelectableItemsArray;

        public PickerItemEntryInList[] SelectableItemsArray
        {
            get { return _SelectableItemsArray; }
            set { _SelectableItemsArray = value; }
        }



        public TemplatedPickerExampleViewModel()
        {
            BackCommand = new Command(() => App.BackToExamplePickerPage());
            Items = new List<PickerItem>();

            SelectableItemsArray = new PickerItemEntryInList[5];

            for (int i = 0; i < SelectableItemsArray.Length; i++)
            {
                SelectableItemsArray[i] = new PickerItemEntryInList();
            }

            var colors = new Color[]
            {
                Color.Linen,
                Color.Maroon,
                Color.MediumAquamarine,
                Color.MediumBlue,
                Color.MediumOrchid,
                Color.MediumSeaGreen,
                Color.MediumSlateBlue,
                Color.Magenta,
                Color.LightSeaGreen,
                Color.PapayaWhip,
                Color.Peru,
                Color.SpringGreen,
                Color.SteelBlue,
                Color.Tan,
                Color.Teal,
                Color.Thistle,
                Color.Tomato,
                Color.Snow,
                Color.Transparent,
                Color.Violet,
                Color.Wheat,
                Color.White,
                Color.WhiteSmoke,
                Color.Yellow,
                Color.YellowGreen,
                Color.Turquoise,
                Color.PeachPuff,
                Color.SlateGray,
                Color.SkyBlue,
                Color.Pink,
                Color.Plum,
            };


            for (int i = 0; i < 10; i++)
            {
                Items.Add(new PickerItem { Title = $"Title {i}", Description = $"Description {i}", BackColor = colors[i] });
            }
        }

        public class PickerItem
        {
            public string Title { get; set; }
            public string Description { get; set; }

            public Color BackColor { get; set; }
        }

        public class PickerItemEntryInList
        {
            public PickerItem Item { get; set; }
        }
    }
}
