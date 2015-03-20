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
using MoreLinq;
using mpeg2_player.Data.DataModels;

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
            DefaultViewModel["Groups"] = await loadChannelDataIntoDbAsync();
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

        private async Task<List<ChannelListDataItem>> loadChannelDataIntoDbAsync()
        {

            PortableXMLTVInterfaceLibraryAsync P_XmltvIL = new PortableXMLTVInterfaceLibraryAsync();

            var ChannelList = await P_XmltvIL.GetXmltvEntireFileAsync(new Uri("http://10.100.107.204/", UriKind.Absolute));

            var channelsFromXML = (from node in ChannelList.Descendants("channel")
                            select new
                            {
                                channelUniqueId = node.Attribute("id").Value.ToString(),
                                channelIcon = node.Element("icon").Attribute("src").Value.ToString(),
                                channelName = node.Elements("display-name").ElementAt(0).Value.ToString(),
                                channelType = node.Elements("display-name").ElementAt(1).Value.ToString(),
                                channelCode = node.Elements("display-name").ElementAt(2).Value.ToString(),
                                channelPPV = node.Elements("display-name").ElementAt(3).Value.ToString(),
                                channelSource = node.Elements("display-name").ElementAt(4).Value.ToString(),
                                channelUrl = node.Element("url").Attribute("src").Value.ToString()
                            }).Distinct();

            List<ChannelListDataItem> cList = new List<ChannelListDataItem>();

            List<channels> channelList = new List<channels>();

            foreach (var item in channelsFromXML)
            {
                ChannelListDataItem cldi = new ChannelListDataItem(item.channelUniqueId, item.channelIcon, item.channelName, item.channelType, item.channelCode, item.channelPPV, item.channelSource, item.channelUrl);
                channels c = new channels();

                c.channelUniqueId = item.channelUniqueId;
                c.channelIcon = item.channelIcon;
                c.channelName = item.channelName;
                c.channelType = item.channelType;
                c.channelCode = item.channelCode;
                c.channelPPV = item.channelPPV;
                c.channelSource = item.channelSource;
                c.channelUrl = item.channelUrl;

                channelList.Add(c);

                cList.Add(cldi);
            }

            int x = await App.dsfdd.Db.InsertAllAsync(channelList.AsEnumerable<channels>());

            itemGridView.ItemsSource = cList;//.DistinctBy(i => i.ChannelName);

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
