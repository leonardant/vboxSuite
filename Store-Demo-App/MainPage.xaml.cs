using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using mpeg2_player.Common;
using mpeg2_player.Data;
using System.Threading.Tasks;
using Vbox_Home_XMLTVInterfaceLibrary;
using System.Linq;

namespace mpeg2_player
{
    public sealed partial class MainPage : LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void LoadState(Object param, Dictionary<String, Object> state)
        {
            DefaultViewModel["Groups"] = await loadChannelDataAsync();
                // VideoDataSource.GetGroups();
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            /*
            VideoDataItem item = e.ClickedItem as VideoDataItem;
            if (item.ID >= 0)
            {
                Frame.Navigate(typeof(PlayerPage), item.ID);
            }
            */
            var itemId = (ChannelListDataItem)e.ClickedItem;
            this.Frame.Navigate(typeof(PlayerPage), itemId);
        }

        private async Task<List<ChannelListDataItem>> loadChannelDataAsync()
        {

            PortableXMLTVInterfaceLibraryAsync P_XmltvIL = new PortableXMLTVInterfaceLibraryAsync();

            var ChannelList = await P_XmltvIL.GetXmltvEntireFileAsync(new Uri("http://10.100.107.204/", UriKind.Absolute));

            var channels = (from node in ChannelList.Descendants("channel")
                            select new
                            {
                                UniqueId = node.Attribute("id").Value.ToString(),
                                icon = node.Element("icon").Attribute("src").Value.ToString(),
                                channelName = node.Element("display-name").Value.ToString(),
                                url = node.Element("url").Attribute("src").Value.ToString()
                            }).Distinct();

            List<ChannelListDataItem> cList = new List<ChannelListDataItem>();

            foreach (var item in channels)
            {
                ChannelListDataItem cldi = new ChannelListDataItem(item.UniqueId, item.icon, item.channelName, item.url);
                cList.Add(cldi);
            }

            itemGridView.ItemsSource = cList;

            return cList;

        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {

            /*
            int id = await App.Current.PickFile();
            if(id >= 0) {
                Frame.Navigate(typeof(PlayerPage), id); 
            }
            */
            Frame.Navigate(typeof(PlayerPage), ((Button)sender).CommandParameter.ToString());
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement elem = sender as FrameworkElement;
            VideoDataGroup group = elem.DataContext as VideoDataGroup;
            Frame.Navigate(typeof(DirectoryPage), group.ID);
        }
    }
}
