using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MWX.XamForms.Popup
{
    public static class PostMobilExtensionMethods
    {
        /// <summary>
        /// returns the Position in the Window of the View
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static Point GetWindowCoordinates(this VisualElement view)
        {
            // A view's default X- and Y-coordinates are LOCAL with respect to the boundaries of its parent,
            // and NOT with respect to the screen. This method calculates the SCREEN coordinates of a view.
            // The coordinates returned refer to the top left corner of the view.
            
            // Initialize with the view's "local" coordinates with respect to its parent
            double screenCoordinateX = view.X;
            double screenCoordinateY = view.Y;

            // Get the view's parent (if it has one...)
            if (view.Parent.GetType() != typeof(Application))
            {
                VisualElement parent = (VisualElement)view.Parent;


                // Loop through all parents
                while (parent != null)
                {
                    // Add in the coordinates of the parent with respect to ITS parent
                    screenCoordinateX += parent.X + parent.TranslationX;
                    screenCoordinateY += parent.Y + parent.TranslationY;

                    // If the parent of this parent isn't the app itself, get the parent's parent.
                    if (parent.Parent is Application)
                        parent = null;
                    else
                        parent = (VisualElement)parent.Parent;
                }
            }

            // Return the final coordinates...which are the global SCREEN coordinates of the view
            return new Point(screenCoordinateX, screenCoordinateY);
        }

        /// <summary>
        /// Search the element hierarchy for an ancestor of type <typeparam name="T"></typeparam> and return it.
        /// </summary>
        /// <param name="visual">The starting location of the search</param>
        public static T FindParent<T>(this Element visual) where T : Element
        {
            var obj = visual;
            while ((obj as T) == null)
            {
                if (obj.Parent == null || obj.Parent is Application)
                {
                    return null;
                }

                obj = obj.Parent as Element;
            }

            return (T)obj;
        }
        
        /// <summary>
        /// Search the element hierarchy for a parent that meets the supplied condition.
        /// </summary>
        /// <param name="visual">The starting location of the search</param>
        /// <param name="condition">The condition that is recursively called for each parent view in the hierarchy</param>
        /// <returns></returns>
        public static Element FindParent(this Element visual, Predicate<Element> condition)
        {
            if (visual.Parent == null)
            {
                return null;
            }

            var current = visual.Parent;

            while (current != null)
            {
                var result = condition(current);

                if (result)
                {
                    return current;
                }

                current = current.Parent;
            }

            return null;
        }
    }
}
