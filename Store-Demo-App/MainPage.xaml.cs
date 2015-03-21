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
using Windows.UI.Popups;
using Windows.Storage;
using Windows.Storage.Search;
using System.Text.RegularExpressions;
using System.IO;

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
            List<channels> chList = await App.dsfdd.Db.QueryAsync<channels>("select * from channels where channelId > ?", 0);
            if (chList.Count > 0)
            {
                await showChannelsAsync();
            }
            else
            {
                // should prompt the user to do this 1st....
                await loadChannelDataIntoDbAsync();
                await showChannelsAsync();
            } 
        }

        private async Task showChannelsAsync()
        {
            List<channels> cList = await App.dsfdd.channelsData.Items.ToListAsync();
            itemGridView.ItemsSource = cList;//.DistinctBy(i => i.ChannelName);
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = (channels)e.ClickedItem;
            this.Frame.Navigate(typeof(PlayerPage), itemId);
        }

        private async Task loadChannelDataIntoDbAsync()
        {
            int clearDbRowsAffected = 0;

            PortableXMLTVInterfaceLibraryAsync P_XmltvIL = new PortableXMLTVInterfaceLibraryAsync();

            var ChannelAndProgrammeList = await P_XmltvIL.GetXmltvEntireFileAsync(new Uri("http://10.100.107.204/", UriKind.Absolute));

            #region // channels

            int numberOfChannelsAddedToDb = 0;

            var channelsFromXML = (from node in ChannelAndProgrammeList.Descendants("channel")
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



            List<channels> channelList = new List<channels>();

            foreach (var item in channelsFromXML)
            {
                
                channels c = new channels();

                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                string channelNameToLookupFix = r.Replace(item.channelName, "_");

                string imageFile = @"Assets\ChannelIcons\" + item.channelType + @"\" + channelNameToLookupFix + @".png";
                StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                var file = await InstallationFolder.TryGetItemAsync(imageFile);

                c.channelUniqueId = item.channelUniqueId;
                c.channelIcon = (file != null ? "ms-appx:///" + imageFile : item.channelIcon);
                c.channelName = item.channelName;
                c.channelType = item.channelType;
                c.channelCode = item.channelCode;
                c.channelPPV = item.channelPPV;
                c.channelSource = item.channelSource;
                c.channelUrl = item.channelUrl;

                channelList.Add(c); 
            }

            // clear the old db data
            clearDbRowsAffected = await App.dsfdd.Db.ExecuteAsync("delete from channels where channelId > ?", 0);

            numberOfChannelsAddedToDb = await App.dsfdd.Db.InsertAllAsync(channelList.AsEnumerable<channels>());

            if (numberOfChannelsAddedToDb.Equals(0))
            {
                showUserCancelableMessage("no channels added to the database");
            }
            else if (numberOfChannelsAddedToDb.Equals(1))
            {
                showUserCancelableMessage("1 channel added to the database");
            }
            else
            {
                showUserCancelableMessage(numberOfChannelsAddedToDb.ToString() + " channels added to the database");
            }

            #endregion

            #region // programmes

            int numberOfProgrammesAddedToDb = 0;

            var programmesFromXML = (from node in ChannelAndProgrammeList.Descendants("programme")
                                     select new
                                     {
                                         programmeStart = node.Attribute("start").Value.ToString(),
                                         programmeStop = node.Attribute("stop").Value.ToString(),
                                         programmeChannel = node.Attribute("channel").Value.ToString(),
                                         programmeTitle = node.Element("title").Value.ToString(),
                                         programmeDescription = node.Element("desc").Value.ToString(),
                                         programmeEpisodeNumberSystem = (node.Element("episode-num") != null ? node.Element("episode-num").Attribute("system").Value.ToString() : string.Empty),
                                         programmeEpisodeNumberProgId = (node.Element("episode-num") != null ? node.Element("episode-num").Value.ToString() : string.Empty)
                                     }).Distinct();

            List<programmes> programList = new List<programmes>();

            foreach (var item in programmesFromXML)
            {

                programmes p = new programmes();

                p.programmeStart = item.programmeStart;
                p.programmeStop = item.programmeStop;
                p.programmeChannel = item.programmeChannel;
                p.programmeTitle = item.programmeTitle;
                p.programmeDescription = item.programmeDescription;
                p.programmeEpisodeNumberSystem = item.programmeEpisodeNumberSystem;
                p.programmeEpisodeNumberProgId = item.programmeEpisodeNumberProgId;

                programList.Add(p);
            }

            // clear the old db data
            clearDbRowsAffected = await App.dsfdd.Db.ExecuteAsync("delete from programmes where programmeId > ?", 0);

            numberOfProgrammesAddedToDb = await App.dsfdd.Db.InsertAllAsync(programList.AsEnumerable<programmes>());

            if (numberOfProgrammesAddedToDb.Equals(0))
            {
                showUserCancelableMessage("no programmes added to the database");
            }
            else if (numberOfProgrammesAddedToDb.Equals(1))
            {
                showUserCancelableMessage("1 programme added to the database");
            }
            else
            {
                showUserCancelableMessage(numberOfProgrammesAddedToDb.ToString() + " programmes added to the database");
            }
            #endregion
        }

        private async void showUserCancelableMessage(string message)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 0;
            await messageDialog.ShowAsync();
        }


        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
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
