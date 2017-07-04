
# Xamarin.Forms Popup View

This repository houses an example of using the Xamarin.Forms API to create a popup view.

It's inspired and based on the FormsPopUp implementation of Michael Davis - https://github.com/michaeled/FormsPopup

But i fixed some issues and extended it with an TemplatedPicker, which is correctly bindable and doesnt use any platformspecific components.

**NuGet**

The Control Library is available as NuGet Package MWX.XamForms.Popup here https://www.nuget.org/packages/MWX.XamForms.Popup/

It's currently in prerelease state but will get released soon!

**Projects**

* MWX.XamForms.Popup                   - (The `Popup` implementation)
* MWX.XamForms.Popup.Examples          - the Xamarin Forms Sample Implementation
* MWX.XamForms.Popup.Examples.Droid    - the Andorid Host Projekt for the Example
* MWX.XamForms.Popup.Examples.iOS      - the iOS Host Projekt for the Example

## A short note ##

This project has a few documented issues. They are mostly related to iOS9.

## Initializing

The current implementation requires either one of two conditions be met before you can use a popup view within a `Page`:

1. The visible page must extend from the `PopupPage` class.
2. Any visible page that does not extend from `PopupPage` must instantiate an object of type `PopupPageInitializer` after the page's content has been set. This is easier than it seems:

**Examples**

There is a sample project in the solution where you can test it.

## Xaml Usage

you need to embed it into your Xaml like this
```xaml
<ContentPage ...
             xmlns:pop="clr-namespace:MWX.XamForms.Popup;assembly=MWX.XamForms.Popup"
             ...
             >
```

## TemplatedPicker

you can fully bind it your ViewModel:
```xaml
            <pop:TemplatedPicker ItemsSource="{Binding Items}" SelectedItem="{Binding SelectedItem}" ListViewRowHeight="60">
                <pop:TemplatedPicker.CellTemplate>
                    <DataTemplate>
                        <ViewCell>
							... add your Template here ...
                        </ViewCell>
                    </DataTemplate>   
                </pop:TemplatedPicker.CellTemplate>
				<!-- optionally you can also configure an empty template that is shown when no item is selected. -->
                <pop:TemplatedPicker.EmptyTemplate>
                    <DataTemplate>
                        <ViewCell>
							... add your Empty-Template here ...
                        </ViewCell>
                    </DataTemplate>
                </pop:TemplatedPicker.EmptyTemplate>
            </pop:TemplatedPicker>
```

## Dynamic Content

If you want to use the Picker or a PopUp in a ListView or other dynamic Controls, you need to embed it into a PopupPage or use a PopUpInitializer directly on the Page to initialize the PopUp.

```xaml
            <pop:PopUpInitializer />

            <ListView ItemsSource="{Binding SelectableItemsArray}" >
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <pop:TemplatedPicker ItemsSource="{Binding Items, Source={x:Reference viewModel}}" SelectedItem="{Binding Item}" ListViewRowHeight="60">
                                <pop:TemplatedPicker.CellTemplate>
                                    <DataTemplate>
                                        <ViewCell>
                                            <Grid>
                                                <Grid.RowDefinitions>
                                                    <RowDefinition />
                                                    <RowDefinition />
                                                </Grid.RowDefinitions>
                                                <Grid.ColumnDefinitions>
                                                    <ColumnDefinition Width="20"/>
                                                    <ColumnDefinition/>
                                                </Grid.ColumnDefinitions>
                                                <Label Grid.Row="0" Grid.Column="1" Text="{Binding Title}" FontSize="Medium" BackgroundColor="{Binding BackColor}" />
                                                <Label Grid.Row="1" Grid.Column="1" Text="{Binding Description}" FontSize="Small" />
                                            </Grid>
                                        </ViewCell>
                                    </DataTemplate>
                                </pop:TemplatedPicker.CellTemplate>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
```

## Normal PopUp's

You can also use the normal PopUps.

The normal PopUps need CodeBehind in the Page so fully MVVM supported.

Here is a Sample:

```csharp
public class CodedSimpleExample : ContentPage
{
	public CodedSimpleExample()
	{
		var popup = new Popup
		{
			XPositionRequest = 0.5,
			YPositionRequest = 0.2,
			ContentHeightRequest = 0.1,
			ContentWidthRequest = 0.4,
			Padding = 10,
	
			Body = new ContentView
			{
				BackgroundColor = Color.White,
				Content = new Label
				{
					XAlign = TextAlignment.Center,
					YAlign = TextAlignment.Center,
					TextColor = Color.Black,
					Text = "Hello, World!"
				}
			}
		};

		var button = new Button {Text = "Show Popup"};
		button.Clicked += (s, e) => popup.Show();
		
		Content = new StackLayout
		{
			Children = 
			{
				button
			}
		};
		
		// Required for the popup to work. It must come after the Content has been set.
		new PopupPageInitializer(this) {popup};
	}
}
```

Two examples have been added to the `MWX.XamForms.Popup.Examples` project to demonstrate this point. Reference either `CodedPopupExample.cs` or `XamlPopupExample.xaml/XamlPopupExample.cs`.

## Sizing and Placement

The current implementation relies heavily on proportional sizes. For any `Popup` properties that require a location or size, you should pass in a value between 0 and 1.

**Example**

```csharp
var popup = new Popup
{
	XPositionRequest = 0.5,
	YPositionRequest = 0.5,
	ContentWidthRequest = 0.8,
	ContentHeightRequest = 0.8
};
```

## Events

The following events are invoked during various moments in a popup's life cycle. They're availble to all `Popup` views.

* Initializing (happens once, during the hosting page's `Appearing` event)
* Tapped
* Showing
* Shown
* Hiding
* Hidden

The `Tapped`, `Showing`, and `Hiding` events can be cancelled by using the event argument property:

**Example**

```csharp
private void Popup1_Showing(object sender, PopupShowingEventArgs e)
{
	if (_preventShowing.On)
	{
		e.Cancel = true;
	}
}
```

## Animations

The `Popup.ShowAsync()` and `Popup.HideAsync()` methods can be used to add animations. If you do not wish to include animations, you can use either `Popup.Show()` or `Popup.Hide()`.

**Example**

```csharp
double original;

await popup.ShowAsync(async p =>
{
	original = p.Scale;

	await Task.WhenAll
	(
		/** 
		 *  Since p is the Popup object, scaling it would also affect the overlay
		 *  behind the popup's body. Although it wouldn't be noticeable in this simple example,
		 *  it would be if the overlay's color was set.
		**/
		
		p.SectionContainer.RelScaleTo(0.05, 100, Easing.CubicOut),
		p.SectionContainer.RelScaleTo(-0.05, 105, Easing.CubicOut)
	).ContinueWith(c =>
	{	// reset popup to original size
		p.SectionContainer.Scale = original;
	});
});
```

## Styling

At this moment, there are no default styles for the popup. Your views will inherit whatever styles you have attached to your resource dictionaries.

## Miscellaneous Features

* The left, top, right, and bottom border colors can be individually set
* During the `Tapped` event, you can determine if the user tapped within the header, body, or footer sections.

## Screenshots
![alt text](https://github.com/michaeled/FormsPopup/blob/master/pictures/androidPopup.gif "Android")
![alt text](https://github.com/michaeled/FormsPopup/blob/master/pictures/iOSPopup.gif "iOS")


More screenshots are available in the repository.

## FAQ

<dl>
  <dt>Q. Can I use XAML to create popup views?</dt>
  <dd><strong>A.</strong> Of course!</dd>

  <dt>Q. Can I add control XX in the popup?</dt>
  <dd><strong>A.</strong> I don't see why not. Let me know if you have any problems.</dd>
</dl>
