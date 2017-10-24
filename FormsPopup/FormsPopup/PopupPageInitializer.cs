using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MWX.XamForms.Popup
{
    /// <summary>
    /// Initialize the <see cref="Popup"/> views that are shown from a <seealso cref="ContentPage"/>
    /// </summary>
    public sealed class PopupPageInitializer : IEnumerable<Popup>
    {
        private readonly List<Popup> _popups = new List<Popup>();
        private readonly AbsoluteLayout _absContent = new AbsoluteLayout();
        private bool _initialized;

        public ContentPage ParentPage { get; set; }
        public IEnumerable<Popup> Popups => _popups;


        /// <summary>
        /// Instantiate <see cref="PopupPageInitializer"/>
        /// </summary>
        /// <param name="parentPage">The page that contains the <see cref="Popup"/> views</param>
        public PopupPageInitializer(ContentPage parentPage)
        {
            if (parentPage == null) throw new ArgumentNullException(nameof(parentPage));

            ParentPage = parentPage;
            parentPage.ChildAdded += ParentPage_ChildAdded;
            parentPage.Appearing += ParentPage_Appearing;
        }


        /// <summary>
        /// This method must be called before the <seealso cref="ContentPage.Content"/> property is set.
        /// </summary>
        /// <param name="popup">The popup to be initialized</param>
        public void Add(Popup popup)
        {
            if (popup == null) throw new ArgumentNullException(nameof(popup));
            _popups.Add(popup);
        }


        public IEnumerator<Popup> GetEnumerator()
        {
            return _popups.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            var oldContent = ParentPage.Content;

            Device.OnPlatform(() => ParentPage.Content = null);

            _absContent.Children.Add(oldContent);

            AbsoluteLayout.SetLayoutFlags(oldContent, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(oldContent, new Rectangle(0, 0, 1, 1));

            ParentPage.Content = _absContent;
        }


        private void ParentPage_ChildAdded(object sender, ElementEventArgs e)
        {
            if (ParentPage is PopupPage) return;

            if (ParentPage.Content == e.Element && e.Element != _absContent)
            {
                Initialize();
            }
        }

        bool initialized = false;
        private void ParentPage_Appearing(object sender, EventArgs e)
        {
            Initialize();

            foreach (var popup in Popups)
            {
                _absContent.Children.Add(popup, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                popup.OnInitializing();
            }
            initialized = true;

        }

        internal void LateInit()
        {
            if (!initialized) throw new NotSupportedException(@"The TemplatedPicker cannot get initialized!

If the Picker is part of a dynamically loaded ressource - like in a ListView-Template - you need an empty invisble Picker on your the ContentPage!
This invisible Picker is needed directly on the Page to intercept the appearing events correctly to be able to create an overlay on the Page.

Simply create a <TemplatedPicker IsVisible=""False"" /> on directly your ContentPage to make an initialisation possible!
");
            foreach (var popup in Popups)
            {
                if (!_absContent.Children.Contains(popup))
                {
                    _absContent.Children.Add(popup, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
                    popup.OnInitializing();
                }
            }
        }

        static Dictionary<ContentPage, PopupPageInitializer> cache = new Dictionary<ContentPage, PopupPageInitializer>();
        public static PopupPageInitializer GetPopUpInitializer(ContentPage page)
        {
            if (cache.ContainsKey(page))
            {
               return cache[page];
            }
            return null;
        }

        public static PopupPageInitializer GetPopUpInitializer(ContentPage page, Popup popup, bool lateInit)
        {
            if (cache.ContainsKey(page))
            {
                var init = cache[page];
                init.Add(popup);
                return init;
            }
            else
            {
                if (lateInit) throw new PopUpNotInitializedException();

                var init = new PopupPageInitializer(page);
                if (popup != null) init.Add(popup);
                cache.Add(page, init);
                return init;
            }
        }
    }
}