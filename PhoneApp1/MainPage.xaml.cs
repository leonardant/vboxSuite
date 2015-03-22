using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;

using Microsoft.PlayerFramework;
using mpeg2_player.Common;
using mpeg2_player.Data;


namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static readonly Guid MPEG2_GUID = new Guid("e06d8026-db46-11cf-b4d1-00805f6cbbea");

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            MediaPlayer.MediaExtensionManager.RegisterVideoDecoder("libmpeg2.Decoder", MPEG2_GUID, new Guid());
            //MediaPlayer.MediaStarted += MediaPlayer_MediaStarted;
            //MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            //MediaPlayer.IsInteractiveChanged += MediaPlayer_IsInteractiveChanged;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}