using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MWX.XamForms.Popup
{
    public class PopUpInitializer : View
    {
        public PopUpInitializer() 
        {
            this.IsVisible = false;
            this.IsEnabled = false;
        }

        public bool Initialized { get; private set; }

        private void CheckInit(ContentPage page = null, bool lateInit = false)
        {
            if (Initialized) return;
            if (page == null) page = this.FindParent<ContentPage>();
            if (page != null)
            {
                var init = PopupPageInitializer.GetPopUpInitializer(page, null, lateInit);

                if (lateInit) init.LateInit();

                Initialized = true;
            }
        }


        protected override void OnParentSet()
        {
            base.OnParentSet();
            CheckInit();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            CheckInit();
        }
    }
    
}
