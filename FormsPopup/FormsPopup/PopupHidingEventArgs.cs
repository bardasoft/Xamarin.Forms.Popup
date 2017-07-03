using System;

namespace MWX.XamForms.Popup
{
	/// <summary>
	/// Represents the available event data, when the <see cref="Popup.Hiding"/> event is invoked. 
	/// </summary>
	public class PopupHidingEventArgs : EventArgs
	{
		/// <summary>
		/// The event can be stopped, by assigning <value>true</value> to this property.
		/// </summary>
		public bool Cancel { get; set; }
	}
}
