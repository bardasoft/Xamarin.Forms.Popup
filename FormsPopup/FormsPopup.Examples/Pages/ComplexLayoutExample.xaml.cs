using System;

namespace MWX.XamForms.Popup.Examples
{
    public partial class ComplexLayoutExample
    {

        public TestPerson Person { get; set; } = new TestPerson();

        public ComplexLayoutExample()
        {
            InitializeComponent();
            BindingContext = this;
        }

        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            popup1.Show();
        }

        protected void Back_OnClicked(object sender, EventArgs e)
        {
            App.BackToExamplePickerPage();
        }

        private void Popup1_Tapped(object sender, PopupTappedEventArgs e)
        {
            popup1.Hide();
        }

        bool show = true;

        protected void Button_Delegate_OnClicked(object sender, EventArgs e)
        {
            show ^= true;
            ShowHidePopUp(show);
        }

        public Action<bool> ShowHidePopUp { get; set; }

    }

    public class TestPerson
    {
        public string FirstName { get; set; } = "Max";
        public string LastName { get; set; } = "Mueller";
    }
}
