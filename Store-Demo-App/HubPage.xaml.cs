using mpeg2_player;
using mpeg2_player.Common;
using mpeg2_player.Data.DataModels;
using Store_Demo_App.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Humanizer;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=321224

namespace Store_Demo_App
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : LayoutAwarePage
    {
        private int _numberOfTvChannels;
        private int _numberOfRadioChannels;

        public HubPage()
        {
            this.InitializeComponent();
        }


        protected async override void LoadState(Object param, Dictionary<String, Object> state)
        {
            await processHubsectionData();
        }

        private List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                {
                    _List.Add(_Child as Control);
                }
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }


        private T FindControl<T>(DependencyObject parentContainer, string controlName)
        {
            var childControls = AllChildren(parentContainer);
            var control = childControls.OfType<Control>().Where(x => x.Name.Equals(controlName)).Cast<T>().First();
            return control;
        }

        private async Task processHubsectionData()
        {
            List<channel> chList = await App.dsfdd.Db.QueryAsync<channel>("select * from channel where channelId > ?", 0);
            if (chList.Count > 0)
            {
                var numberOfTvChannels = (from TVC in chList
                                where TVC.channelType.Equals("TV")
                                select TVC).Count();
                if (numberOfTvChannels > 0)
                {
                    _numberOfTvChannels = numberOfTvChannels;
                    TvHubsection.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    TvHubsection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                var numberOfRadioChannels = (from RC in chList
                                          where RC.channelType.Equals("Radio")
                                             select RC).Count();
                if (numberOfRadioChannels > 0)
                {
                    _numberOfRadioChannels = numberOfRadioChannels;
                    RadioHubsection.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    TvHubsection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    RadioHubsection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

            }
            else
            {
                TvHubsection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                RadioHubsection.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            } 
        }

        private void RadioHubSection_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), "Radio");
        }

        private void TvHubSection_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), "TV");
        }

        private void TvText_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                TextBlock tb = (TextBlock)sender;
                if (_numberOfTvChannels == 1)
                {
                    tb.Text = "There is " + " channel".ToQuantity(1) + " available.";
                }
                else
                {
                    tb.Text = "There are " + " channel".ToQuantity(_numberOfTvChannels) + " available.";
                }    
            }
        }

        private void RadioText_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock)
            {
                TextBlock tb = (TextBlock)sender;
                if (_numberOfRadioChannels == 1)
                {
                    tb.Text = "There is " + " channel".ToQuantity(_numberOfRadioChannels) + " available.";
                }
                else
                {
                    tb.Text = "There are " + " channel".ToQuantity(_numberOfRadioChannels) + " available.";
                }
            }
        }

    }
}
