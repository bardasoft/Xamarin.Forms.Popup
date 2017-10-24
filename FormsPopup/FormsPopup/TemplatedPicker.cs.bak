using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace MWX.XamForms.Popup
{
    public class TemplatedPicker : Frame
    {
        StackLayout listContent;
        ListView lv;
        Popup popup;

        StackLayout spContent;
        Frame frame;
        Button closeButton, unselectButton;
        Label headerLabel, descriptionLabel;

        #region BindableProperties

        public static readonly BindableProperty CellTemplateProperty
            = BindableProperty.Create(nameof(CellTemplate), typeof(DataTemplate), typeof(TemplatedPicker), defaultValue: null, propertyChanged: RefreshLayoutByProperty);
        /// <summary>
        /// CellTemplate for a Item
        /// </summary>
        public DataTemplate CellTemplate { get { return (DataTemplate)GetValue(CellTemplateProperty); } set { SetValue(CellTemplateProperty, value); } }

        public static readonly BindableProperty EmptyTemplateProperty
            = BindableProperty.Create(nameof(EmptyTemplate), typeof(DataTemplate), typeof(TemplatedPicker), defaultValue: null, propertyChanged: RefreshLayoutByProperty);
        /// <summary>
        /// EmptyTemplate for a Item
        /// </summary>
        public DataTemplate EmptyTemplate { get { return (DataTemplate)GetValue(EmptyTemplateProperty); } set { SetValue(EmptyTemplateProperty, value); } }


        public static readonly BindableProperty SelectedItemProperty
            = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(TemplatedPicker)
                , defaultBindingMode: BindingMode.TwoWay, defaultValue: null, propertyChanged: RefreshLayoutByProperty);
        /// <summary>
        /// the Currently Selected Item
        /// </summary>
        public object SelectedItem { get { return GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); } }


        public static readonly BindableProperty ItemsSourceProperty
            = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(TemplatedPicker), defaultValue: null, propertyChanged: RefreshLayoutByProperty);
        /// <summary>
        /// The Source of the 
        /// </summary>
        public IEnumerable ItemsSource { get { return (IEnumerable)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }


        public static BindableProperty HeaderBackgroundColorProperty
            = BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.White, propertyChanged: PropertyHeaderBackgroundColorChanged);
        /// <summary>
        /// Background Color of the Header
        /// </summary>
        public Color HeaderBackgroundColor { get { return (Color)GetValue(HeaderBackgroundColorProperty); } set { SetValue(HeaderBackgroundColorProperty, value); } }

        private static void PropertyHeaderBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.popup.Header.BackgroundColor = (Color)newValue;
            }
        }


        public static BindableProperty CloseButtonTextProperty
            = BindableProperty.Create(nameof(CloseButtonText), typeof(string), typeof(TemplatedPicker), defaultValue: "Close", propertyChanged: PropertyCloseButtonTextChanged);
        public string CloseButtonText { get { return (string)GetValue(CloseButtonTextProperty); } set { SetValue(CloseButtonTextProperty, value); } }

        private static void PropertyCloseButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.closeButton.Text = (string)newValue;
            }
        }


        public static BindableProperty CloseButtonTextColorProperty
            = BindableProperty.Create(nameof(CloseButtonTextColor), typeof(Color), typeof(TemplatedPicker), defaultValue: default(Color), propertyChanged: PropertyCloseButtonTextColorChanged);
        /// <summary>
        /// Color of the Text of the CloseButton
        /// </summary>
        public Color CloseButtonTextColor { get { return (Color)GetValue(CloseButtonTextColorProperty); } set { SetValue(CloseButtonTextColorProperty, value); } }

        private static void PropertyCloseButtonTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.closeButton.TextColor = (Color)newValue;
            }
        }

        public static BindableProperty HeaderTextProperty = BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(TemplatedPicker), defaultValue: "Selection", propertyChanged: PropertyHeaderTextChanged);
        /// <summary>
        /// Text in the Header of the PopUp
        /// </summary>
        public string HeaderText { get { return (string)GetValue(HeaderTextProperty); } set { SetValue(HeaderTextProperty, value); } }

        private static void PropertyHeaderTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.headerLabel.Text = (string)newValue;
            }
        }


        public static BindableProperty HeaderTextColorProperty = BindableProperty.Create(nameof(HeaderTextColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.Black, propertyChanged: PropertyHeaderTextColorChanged);
        /// <summary>
        /// Color of the HeaderText
        /// </summary>
        public Color HeaderTextColor { get { return (Color)GetValue(HeaderTextColorProperty); } set { SetValue(HeaderTextColorProperty, value); } }

        private static void PropertyHeaderTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.headerLabel.TextColor = (Color)newValue;
            }
        }


        public static BindableProperty HeaderPaddingProperty = BindableProperty.Create(nameof(HeaderPadding), typeof(Thickness), typeof(TemplatedPicker), defaultValue: new Thickness(10), propertyChanged: PropertyHeaderPaddingChanged);
        /// <summary>
        /// Padding of the HeaderContent
        /// </summary>
        public Thickness HeaderPadding { get { return (Thickness)GetValue(HeaderPaddingProperty); } set { SetValue(HeaderPaddingProperty, value); } }

        private static void PropertyHeaderPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                ((ContentView)instance.popup.Header).Padding = (Thickness)newValue;
            }
        }


        public static BindableProperty BodyPaddingProperty = BindableProperty.Create(nameof(BodyPadding), typeof(Thickness), typeof(TemplatedPicker), defaultValue: new Thickness(10), propertyChanged: PropertyBodyPaddingChanged);
        /// <summary>
        /// Padding of the ListView int the Body
        /// </summary>
        public Thickness BodyPadding { get { return (Thickness)GetValue(BodyPaddingProperty); } set { SetValue(BodyPaddingProperty, value); } }

        private static void PropertyBodyPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                ((ContentView)instance.popup.Body).Padding = (Thickness)newValue;
            }
        }


        public static BindableProperty BodyBackgroundColorProperty = BindableProperty.Create(nameof(BodyBackgroundColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.White, propertyChanged: PropertyBodyBackgroundColorChanged);
        /// <summary>
        /// BackgroundColor Of the Body
        /// </summary>
        public Color BodyBackgroundColor { get { return (Color)GetValue(BodyBackgroundColorProperty); } set { SetValue(BodyBackgroundColorProperty, value); } }

        private static void PropertyBodyBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.popup.Body.BackgroundColor = (Color)newValue;
            }
        }


        public static BindableProperty FooterBackgroundColorProperty = BindableProperty.Create(nameof(FooterBackgroundColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.White, propertyChanged: PropertyFooterBackgroundColorChanged);
        /// <summary>
        /// Background Color of the Footer
        /// </summary>
        public Color FooterBackgroundColor { get { return (Color)GetValue(FooterBackgroundColorProperty); } set { SetValue(FooterBackgroundColorProperty, value); } }

        private static void PropertyFooterBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.popup.Footer.BackgroundColor = (Color)newValue;
            }
        }


        public static BindableProperty SelectedOutlineColorProperty = BindableProperty.Create(nameof(SelectedOutlineColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.Gray, propertyChanged: PropertySelectedOutlineColorChanged);
        /// <summary>
        /// Color of the Outline of the SelectedItem-View
        /// </summary>
        public Color SelectedOutlineColor { get { return (Color)GetValue(SelectedOutlineColorProperty); } set { SetValue(SelectedOutlineColorProperty, value); } }

        private static void PropertySelectedOutlineColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.frame.OutlineColor = (Color)newValue;
            }
        }


        public static BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.Transparent, propertyChanged: PropertySelectedBackgroundColorChanged);
        /// <summary>
        /// Color of the SelectedItem-View
        /// </summary>
        public Color SelectedBackgroundColor { get { return (Color)GetValue(SelectedBackgroundColorProperty); } set { SetValue(SelectedBackgroundColorProperty, value); } }

        private static void PropertySelectedBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.frame.BackgroundColor = (Color)newValue;
            }
        }


        public static BindableProperty PopUpWidthRequestProperty = BindableProperty.Create(nameof(PopUpWidthRequest), typeof(double), typeof(TemplatedPicker), defaultValue: 1d, propertyChanged: PropertyPopUpWidthRequestChanged);
        /// <summary>
        /// Width of the popup dialog in relation to the page width (values between 0 <-> 1) - default 1
        /// </summary>
        public double PopUpWidthRequest { get { return (double)GetValue(PopUpWidthRequestProperty); } set { SetValue(PopUpWidthRequestProperty, value); } }

        private static void PropertyPopUpWidthRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.popup.ContentWidthRequest = (double)newValue;
            }
        }


        public static BindableProperty PopUpHeightRequestProperty = BindableProperty.Create(nameof(PopUpHeightRequest), typeof(double), typeof(TemplatedPicker), defaultValue: 1d, propertyChanged: PropertyPopUpHeightRequestChanged);
        /// <summary>
        /// Height of the popup dialog in relation to the page height (values between 0 <-> 1) - default 1
        /// </summary>
        public double PopUpHeightRequest { get { return (double)GetValue(PopUpHeightRequestProperty); } set { SetValue(PopUpHeightRequestProperty, value); } }

        private static void PropertyPopUpHeightRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.popup.ContentHeightRequest = (double)newValue;
            }
        }


        public static BindableProperty HeaderFontSizeProperty = BindableProperty.Create(nameof(HeaderFontSize), typeof(NamedSize), typeof(TemplatedPicker), defaultValue: NamedSize.Large, propertyChanged: PropertyHeaderFontSizeChanged);
        /// <summary>
        /// Size of the HeaderText in the Popup
        /// </summary>
        public NamedSize HeaderFontSize { get { return (NamedSize)GetValue(HeaderFontSizeProperty); } set { SetValue(HeaderFontSizeProperty, value); } }

        private static void PropertyHeaderFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.headerLabel.FontSize = Device.GetNamedSize((NamedSize)newValue, typeof(Label));
            }
        }


        public static BindableProperty DescriptionTextProperty = BindableProperty.Create(nameof(DescriptionText), typeof(string), typeof(TemplatedPicker), defaultValue: "Select a item:", propertyChanged: PropertyDescriptionTextChanged);
        /// <summary>
        /// DescriptionText on the top of the body.
        /// </summary>
        public string DescriptionText { get { return (string)GetValue(DescriptionTextProperty); } set { SetValue(DescriptionTextProperty, value); } }

        private static void PropertyDescriptionTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.descriptionLabel.Text = (string)newValue;
            }
        }


        public static BindableProperty DescriptionFontSizeProperty = BindableProperty.Create(nameof(DescriptionFontSize), typeof(NamedSize), typeof(TemplatedPicker), defaultValue: NamedSize.Medium, propertyChanged: PropertyDescriptionFontSizeChanged);
        /// <summary>
        /// Size of the description-Text on the top of the body
        /// </summary>
        public NamedSize DescriptionFontSize { get { return (NamedSize)GetValue(DescriptionFontSizeProperty); } set { SetValue(DescriptionFontSizeProperty, value); } }

        private static void PropertyDescriptionFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.descriptionLabel.FontSize = Device.GetNamedSize((NamedSize)newValue, typeof(Label));
            }
        }


        public static BindableProperty UnSelectButtonVisibleProperty = BindableProperty.Create(nameof(UnSelectButtonVisible), typeof(bool), typeof(TemplatedPicker), defaultValue: true, propertyChanged: PropertyUnSelectButtonVisibleChanged);
        /// <summary>
        /// Indicates if the "Unselect" Button should be visible or not.
        /// </summary>
        public bool UnSelectButtonVisible { get { return (bool)GetValue(UnSelectButtonVisibleProperty); } set { SetValue(UnSelectButtonVisibleProperty, value); } }

        private static void PropertyUnSelectButtonVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.unselectButton.IsVisible = (bool)newValue;
            }
        }


        public static BindableProperty UnSelectButtonTextProperty = BindableProperty.Create(nameof(UnSelectButtonText), typeof(string), typeof(TemplatedPicker), defaultValue: "none", propertyChanged: PropertyUnSelectButtonTextChanged);
        /// <summary>
        /// Text of the UnselectButton
        /// </summary>
        public string UnSelectButtonText { get { return (string)GetValue(UnSelectButtonTextProperty); } set { SetValue(UnSelectButtonTextProperty, value); } }

        private static void PropertyUnSelectButtonTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.unselectButton.Text = (string)newValue;
            }
        }


        public static BindableProperty UnSelectButtonTextColorProperty = BindableProperty.Create(nameof(UnSelectButtonTextColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.Blue, propertyChanged: PropertyUnSelectButtonTextColorChanged);
        /// <summary>
        /// TextColor of the UnSelectButton
        /// </summary>
        public Color UnSelectButtonTextColor { get { return (Color)GetValue(UnSelectButtonTextColorProperty); } set { SetValue(UnSelectButtonTextColorProperty, value); } }

        private static void PropertyUnSelectButtonTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.unselectButton.TextColor = (Color)newValue;
            }
        }


        public static BindableProperty UnSelectButtonBackgroundColorProperty = BindableProperty.Create(nameof(UnSelectButtonBackgroundColor), typeof(Color), typeof(TemplatedPicker), defaultValue: Color.Transparent, propertyChanged: PropertyUnSelectBackgroundColorChanged);
        /// <summary>
        /// BackgroundColor of the UnselectButton
        /// </summary>
        public Color UnSelectButtonBackgroundColor { get { return (Color)GetValue(UnSelectButtonBackgroundColorProperty); } set { SetValue(UnSelectButtonBackgroundColorProperty, value); } }

        private static void PropertyUnSelectBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.unselectButton.BackgroundColor = (Color)newValue;
            }
        }


        public static BindableProperty ListViewRowHeightProperty = BindableProperty.Create(nameof(ListViewRowHeight), typeof(int), typeof(TemplatedPicker), defaultValue: 60, propertyChanged: PropertyListViewRowHeightChanged);
        /// <summary>
        /// Height of the Items in the PopUp-ListView
        /// </summary>
        public int ListViewRowHeight { get { return (int)GetValue(ListViewRowHeightProperty); } set { SetValue(ListViewRowHeightProperty, value); } }

        private static void PropertyListViewRowHeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                instance.frame.HeightRequest =
                instance.lv.RowHeight = (int)newValue;
            }
        }


        public static BindableProperty EmptyTextProperty = BindableProperty.Create(nameof(EmptyText), typeof(string), typeof(TemplatedPicker), defaultValue: " - nothing selected -", propertyChanged: PropertyEmptyTextChanged);
        /// <summary>
        /// The Text that is displayed when no Item is selected and no EmptyTemplate is set.
        /// </summary>
        public string EmptyText { get { return (string)GetValue(EmptyTextProperty); } set { SetValue(EmptyTextProperty, value); } }

        private static void PropertyEmptyTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as TemplatedPicker;
            if (instance != null)
            {
                // TODO: update when EmptyText changed
            }
        }


        #endregion

        public TemplatedPicker()
        {
            this.Padding = 0;
            this.Margin = 5;
            spContent = new StackLayout();
            listContent = new StackLayout();
            frame = new Frame()
            {
                Padding = 0,
                Margin = 0
            };
            lv = new ListView
            {
                RowHeight = ListViewRowHeight,
                Margin = 0
            };
            headerLabel = new Label
            {
                FontSize = Device.GetNamedSize(HeaderFontSize, typeof(Label)),
                TextColor = HeaderTextColor,
                Text = HeaderText
            };

            closeButton = new Button
            {
                Text = CloseButtonText,
                TextColor = CloseButtonTextColor,
                BackgroundColor = CloseButtonTextColor,
            };
            closeButton.Clicked += CloseButton_Clicked;

            unselectButton = new Button
            {
                Text = UnSelectButtonText,
                TextColor = UnSelectButtonTextColor,
                BackgroundColor = UnSelectButtonBackgroundColor,
                IsVisible = UnSelectButtonVisible
            };
            unselectButton.Clicked += UnselectButton_Clicked;

            popup = new Popup
            {
                XPositionRequest = 0.5,
                YPositionRequest = 0.5,
                ContentWidthRequest = 1,
                ContentHeightRequest = 1,

                Header = new ContentView
                {
                    Padding = HeaderPadding,
                    BackgroundColor = HeaderBackgroundColor,
                    Content = headerLabel
                },

                Body = new ContentView
                {
                    Padding = BodyPadding,
                    BackgroundColor = BodyBackgroundColor,
                    Content = listContent
                },

                Footer = new ContentView
                {
                    BackgroundColor = FooterBackgroundColor,

                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children = { unselectButton, closeButton }
                    }
                }
            };

            frame.BackgroundColor = SelectedBackgroundColor;
            frame.OutlineColor = SelectedOutlineColor;

            spContent.Children.Add(frame);
            //spContent.Children.Add(popup);

            frame.GestureRecognizers.Add(new TapGestureRecognizer(selectionTapped));

            this.Content = spContent;

            descriptionLabel = new Label
            {
                FontSize = Device.GetNamedSize(DescriptionFontSize, typeof(Label)),
                Text = DescriptionText
            };

            listContent.Children.Add(descriptionLabel);
            listContent.Children.Add(lv);

            RefreshContent();

            lv.ItemSelected += Lv_ItemSelected;
        }

        private async void UnselectButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (popup.IsVisible) await popup.HideAsync(null);
                this.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (Debugger.IsAttached) Debugger.Break();
            }
        }

        bool initialized = false;
        static PopupPageInitializer init;

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

        private void CheckInit(ContentPage page = null, bool lateInit = false)
        {
            if (initialized) return;
            if (page == null) page = this.FindParent<ContentPage>();
            if (page != null)
            {
                var init = PopupPageInitializer.GetPopUpInitializer(page, popup, lateInit);

                if (lateInit) init.LateInit();

                initialized = true;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await HidePopUp();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (Debugger.IsAttached) Debugger.Break();
            }
        }

        private async Task HidePopUp()
        {
            await popup.HideAsync(null);
        }

        private async void selectionTapped(View arg1, object arg2)
        {
            try
            {
                CheckInit(Application.Current.MainPage as ContentPage, !initialized);

                lv.ItemTemplate = this.CellTemplate;
                lv.ItemsSource = ItemsSource;

                await popup.ShowAsync(null);
            }
            catch (PopUpNotInitializedException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Oops!", ex.Message, "ok");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler: {ex}");
            }
        }

        private async void Lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await HidePopUp();

            this.SelectedItem = e.SelectedItem;
        }



        private static void RefreshLayoutByProperty(BindableObject bindable, object oldValue, object newValue)
        {
            var contentView = bindable as TemplatedPicker;
            if (contentView != null)
            {
                contentView.emptyContent = null;
                contentView.RefreshContent();
            }
        }

        bool isRefreshing = false;

        public void RefreshContent()
        {
            if (isRefreshing) return; // nicht rekusiv ;)
            try
            {
                isRefreshing = true;

                Element cont = GetEmptyView();
                if (SelectedItem != null)
                {
                    if (filledContent == null) filledContent = (CellTemplate?.CreateContent() as ViewCell).View;
                    cont = filledContent;
                }

                if (cont == null) cont = GetEmptyView();

                //cont.InputTransparent = true;

                lv.ItemTemplate = this.CellTemplate;

                // to avoid nullreference excetion in Xamarin.Forms.ListView
                //lv.SelectedItem = null;
                //lv.ItemsSource = null;
                lv.ItemsSource = ItemsSource;
                lv.SelectedItem = SelectedItem;

                var vc = cont as View;
                if (vc != null)
                {
                    frame.Content = vc;

                    frame.Content.Margin = 0;
                    frame.HeightRequest = ListViewRowHeight;

                    frame.Content.BindingContext = SelectedItem;
                }
            }
            finally
            {
                isRefreshing = false;
            }


        }

        View emptyContent = null;
        View filledContent = null;

        private View GetEmptyView()
        {
            if (emptyContent == null)
            {
                if (EmptyTemplate != null) emptyContent = (EmptyTemplate?.CreateContent() as ViewCell)?.View;
                else emptyContent = new Label { Text = EmptyText, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, HeightRequest = ListViewRowHeight };
            }

            return emptyContent;
        }
    }

    public class AbsoluteTopLayout : AbsoluteLayout
    {
        public Action BringToFrontHandler { get; set; }

        public void BringToFront()
        {
            BringToFrontHandler?.Invoke();
        }
    }
}

