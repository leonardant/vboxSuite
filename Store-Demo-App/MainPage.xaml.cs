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
using PortableVbox_GHome_RecordingsInterface;
using Store_Demo_App.Data.DataModels;
using Store_Demo_App.Common._23pd;

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
            List<channel> chList = await App.dsfdd.Db.QueryAsync<channel>("select * from channel where channelId > ?", 0);
            if (chList.Count > 0)
            {
                await showChannelsAsync();
            }
            else
            {
                // should prompt the user to do this 1st....
                await loadChannelDataIntoDbAsync();
                await loadRecordingsDataIntoDbAsync();

                await showChannelsAsync();
            } 
        }

        private async Task showChannelsAsync()
        {
            List<channel> cList = await App.dsfdd.channelsData.Items.ToListAsync();
            itemGridViewChannels.ItemsSource = cList;//.DistinctBy(i => i.ChannelName);
            itemGridViewRecordings.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            itemGridViewChannels.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async Task showRecordingsAsync()
        {
            List<recording> rList = await App.dsfdd.recordingsData.Items.ToListAsync();
            itemGridViewRecordings.ItemsSource = rList;//.DistinctBy(i => i.ChannelName);
            itemGridViewChannels.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            itemGridViewRecordings.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void ItemView_ItemChannelsClick(object sender, ItemClickEventArgs e)
        {
            var itemId = (channel)e.ClickedItem;
            this.Frame.Navigate(typeof(PlayerPage), itemId);
        }

        private void ItemView_ItemRecordingsClick(object sender, ItemClickEventArgs e)
        {
            var itemId = (recording)e.ClickedItem;
            this.Frame.Navigate(typeof(PlayerPage), itemId);
        }

        private async Task loadRecordingsDataIntoDbAsync()
        {
            int clearDbRowsAffected = 0;

            RecordingsInterfaceLibraryAsync RIL = new RecordingsInterfaceLibraryAsync();

            var RecordingsList = await RIL.GetRecordsListAsync(new Uri("http://10.100.107.204/", UriKind.Absolute), true);

            #region // scheduled Recordings
            int numberOfRecordingsAddedToDb = 0;

            var recordingsFromXML = (from node in RecordingsList.Descendants("record")
                                     select new
                                     {
                                         recordingChannel = node.Attribute("channel").Value.ToString(),
                                         recordingStart = node.Attribute("start").Value.ToString(),
                                         recordingStop = node.Attribute("stop").Value.ToString(),
                                         recordingChannelName = node.Element("channel-name").Value.ToString(),
                                         recordingTitle = (node.Element("programme-title") != null ? node.Element("programme-title").Value.ToString() : string.Empty),
                                         recordingDescription = (node.Element("programme-desc") != null ? node.Element("programme-desc").Value.ToString() : string.Empty),
                                         recordingState = node.Element("state").Value.ToString(),
                                         recordingRecordId = (node.Element("record-id") != null ? node.Element("record-id").Value.ToString() : string.Empty),
                                         recordingUrl = (node.Element("url") != null ? node.Element("url").Value.ToString() : string.Empty),
                                         recordingLocalTarget = (node.Element("LocalTarget") != null ? node.Element("LocalTarget").Value.ToString() : string.Empty)
                                     }).Distinct();

            List<recording> recordingsList = new List<recording>();

            foreach (var item in recordingsFromXML)
            {

                recording r = new recording();

                r.recordingChannel = item.recordingChannel;
                r.recordingStart = item.recordingStart;
                r.recordingStop = item.recordingStop;
                r.recordingChannelName = item.recordingChannelName;
                r.recordingTitle = item.recordingTitle;
                r.recordingDescription = item.recordingDescription;
                r.recordingState = item.recordingState;
                r.recordingRecordId = item.recordingRecordId;
                r.recordingUrl = item.recordingUrl;
                r.recordingLocalTarget = item.recordingLocalTarget;

                recordingsList.Add(r);
            }

            // clear the old db data
            clearDbRowsAffected = await App.dsfdd.Db.ExecuteAsync("delete from recording where recordingId > ?", 0);

            numberOfRecordingsAddedToDb = await App.dsfdd.Db.InsertAllAsync(recordingsList.AsEnumerable<recording>());

            #endregion
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



            List<channel> channelList = new List<channel>();

            foreach (var item in channelsFromXML)
            {
                
                channel c = new channel();

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
            clearDbRowsAffected = await App.dsfdd.Db.ExecuteAsync("delete from channel where channelId > ?", 0);

            numberOfChannelsAddedToDb = await App.dsfdd.Db.InsertAllAsync(channelList.AsEnumerable<channel>());

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

            List<programme> programList = new List<programme>();

            foreach (var item in programmesFromXML)
            {

                programme p = new programme();

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
            clearDbRowsAffected = await App.dsfdd.Db.ExecuteAsync("delete from programme where programmeId > ?", 0);

            numberOfProgrammesAddedToDb = await App.dsfdd.Db.InsertAllAsync(programList.AsEnumerable<programme>());

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
            //MessageDialogShower m;

            var messageDialog = new MessageDialog(message);
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 0;
            await messageDialog.ShowDialogSafely();
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement elem = sender as FrameworkElement;
            VideoDataGroup group = elem.DataContext as VideoDataGroup;
            Frame.Navigate(typeof(DirectoryPage), group.ID);
        }

        private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(PlayerPage), ((Button)sender).CommandParameter.ToString());   

            switch (((Button)sender).CommandParameter.ToString())
            {
                case "Channels":
                    await showChannelsAsync();
                    break;
                //case "Recordings":

                default:
                    await showRecordingsAsync();
                    break;
            }
        }
    }
}
