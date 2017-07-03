using System;

namespace MWX.XamForms.Popup
{
    public class PopUpNotInitializedException : Exception
    {
        const string ErrorMessage = @"The TemplatedPicker could'nt get initialized!

If the Picker is part of a dynamically loaded ressource - like in a ListView-Template - you need an empty invisble Picker on your the ContentPage!
This invisible Picker is needed directly on the Page to intercept the appearing events correctly to be able to create an overlay on the Page.

Simply create a <TemplatedPicker IsVisible=""False"" /> on directly your ContentPage to make an initialisation possible!
";
        public PopUpNotInitializedException() : this(ErrorMessage)
        { }
        public PopUpNotInitializedException(string message) : base(message) { }
        public PopUpNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }
}