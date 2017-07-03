using System;

namespace MWX.XamForms.Popup
{
    /// <summary>
    /// Used to identify the logical sections of a <see cref="Popup"/>.
    /// </summary>
    [Flags]
    public enum PopupSectionType
    {
        NotSet,
        Border,
        Backdrop,
        Header,
        Body,
        Footer
    }
}