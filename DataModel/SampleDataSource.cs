//------------------------------------------------------------------------------
// <copyright file="SampleDataSource.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.ControlsBasics.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Samples.Kinect.ControlsBasics.Common;
    using System.Globalization;

    // The data model defined by this file serves as a representative example of a strongly-typed
    // model that supports notification when members are added, removed, or modified.  The property
    // names chosen coincide with data bindings in the standard item templates.
    // Applications may use this model as a starting point and build on it, or discard it entirely and
    // replace it with something appropriate to their needs.

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public sealed class SampleDataSource
    {
        private static SampleDataSource sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataCollection> allGroups = new ObservableCollection<SampleDataCollection>();

        private static Uri darkGrayImage = new Uri("Assets/DarkGray.png", UriKind.Relative);
        private static Uri mediumGrayImage = new Uri("Assets/mediumGray.png", UriKind.Relative);
        private static Uri lightGrayImage = new Uri("Assets/lightGray.png", UriKind.Relative);
        private static Uri a = new Uri("Images/clouds.jpg", UriKind.RelativeOrAbsolute);

        public SampleDataSource()
        {
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new SampleDataCollection(
                    "Group-1",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    SampleDataSource.mediumGrayImage,
                    SampleDataSource.mediumGrayImage,
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-1",
            //            "Buttons",
            //            "1231321321321321313132",
            //            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
            //            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
            //            "Several types of buttons with custom styles",
            //            "123333",
            //            group1,
            //            typeof(ButtonSample)));
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-2",
            //            "CheckBoxes and RadioButtons",
            //            string.Empty,
            //            SampleDataSource.a,
            //            SampleDataSource.mediumGrayImage,
            //            "CheckBox and RadioButton controls",
            //            itemContent,
            //            group1,
            //            typeof(CheckBoxRadioButtonSample)));
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-5",
            //            "Zoomable Photo",
            //            string.Empty,
            //            SampleDataSource.lightGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "ScrollViewer control hosting a photo, enabling scrolling and zooming.",
            //            itemContent,
            //            group1,
            //            typeof(ScrollViewerSample)));
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-7",
            //            "Engagement and Cursor Settings",
            //            "",
            //            SampleDataSource.darkGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Enables user to switch between engagement models and cursor visuals.",
            //            itemContent,
            //            group1,
            //            typeof(EngagementSettings)));
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-6",
            //            "Kinect Pointer Events",
            //            string.Empty,
            //            SampleDataSource.lightGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Example of how to get KinectPointerPoints.",
            //            itemContent,
            //            group1,
            //            typeof(KinectPointerPointSample)));
            //group1.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-7",
            //            "Engagement and Cursor Settings",
            //            "",
            //            SampleDataSource.darkGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Enables user to switch between engagement models and cursor visuals.",
            //            itemContent,
            //            group1,
            //            typeof(EngagementSettings)));
            group1.Items.Add(
                    new SampleDataItem(
                        "Group-1-Item-1",
                        "黃金甜蜜地瓜飲單瓶",
                        "NT$ 49",
                        new Uri("http://www.nargo.com.tw/image/data/0725/T9.jpg"),
                        new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
                        "黃金甜蜜推出營養地瓜飲，香濃的氣味來自台灣產地直銷的天然地瓜，也來自台灣農人的用心耕耘，少了化學的藥品、少了華而不實的香氣，以最簡單的原料—地瓜，製成一瓶瓶健康的飲品，用最簡單的心做最好的食品，用心將地瓜原本最美好的風味封存入一罐罐地瓜飲裡面，濃郁香甜，生津止渴。",
                        itemContent,
                        group1));
            group1.Items.Add(
                    new SampleDataItem(
                        "Group-1-Item-2",
                        "嘻豆非基因改造黃豆(高雄9號)",
                        "NT$ 150",
                        new Uri("http://www.nargo.com.tw/image/data/0725/T7.jpg"),
                        new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1575"),
                        "台灣每年進口約250萬公噸的大豆，有九成以上是基改大豆。本土黃豆新鮮、採收後直接冷藏，比進口黃豆安心許多，也很適合乾旱缺水的極端氣候，希望大家能多支持。高雄選9號黃豆，飽滿豆香濃，做豆漿或放入飯中一起烹煮都很合適！",
                        itemContent,
                        group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "嘻豆-黑白豆",
                    "NT$ 130",
                    new Uri("http://www.nargo.com.tw/image/data/0725/t5.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1571"),
                    "黑白豆-以高雄7號+高雄9號煎焙製造而成，香酥可口．豆香迷人！",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "新纖豆點",
                    "NT$250",
                    new Uri("http://www.nargo.com.tw/image/data/0725/t1.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1564"),
                    "嚴選煎焙台灣非基改黃豆、青仁黑豆、全果粒蔓越莓(糖、蔓越莓、葵花籽油)、南瓜子完美比例,讓人一口接一口停不下來",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【尤多拉】紅藜穀物棒",
                    "NT$ 200",
                    new Uri("http://www.nargo.com.tw/image/data/0725/123.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1556"),
                    "「德朱利斯」，音譯自DJULIS，為「台灣紅藜」的原住民發音。是大自然給予台東農民的恩賜。「紅藜」被稱為「穀類界的紅寶石」，然而，一顆顆紅寶石若無人聞問，也只能落地歸還給大自然。看見紅藜的光芒，我們捧起紅藜，牽起台東農民的手，創造出專屬於台東的特色伴手禮，將在地化產品推向國際。紅 藜 穀 物 棒紅藜香氣及酥餅的香酥脆，非常涮嘴，包您一口接著一口！",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【尤多拉】巧克彩虹卷系列-15入",
                    "NT$ 240",
                    new Uri("http://www.nargo.com.tw/image/data/0725/_MG_2187.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1559"),
                    "以日本老師傅技藝將厚實的手工日式蛋捲與香濃巧克力合而為一，傳統元素結合創新工法，為您的味覺開啟彩虹般的奇幻旅程繽粉華麗的手工日式蛋捲！有香蕉巧克力草莓巧克力黑巧克力三種口味除了視覺上的享受更讓您可一次品嚐三種不同蛋捲的獨特風味。",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【尤多拉】心有所屬芥末",
                    "NT$ 130",
                    new Uri("http://www.nargo.com.tw/image/data/0725/7813.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1542"),
                    "【尤多拉】心有所屬芥末",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "《南投拌手禮》冬筍餅禮盒",
                    "NT$ 150",
                    new Uri("http://www.nargo.com.tw/image/data/0725/n5.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1504"),
                    "到南投竹山遊玩，除了品嚐當地名產之外，當地人一定不忘推薦您嚐嚐當地有名的「日香食品」冬筍餅，它是陪伴許多五年級生度過繽紛的童年回憶的零嘴，深受許許多多忠實粉絲的喜愛與支持！",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【尤多拉】幸福煎餅禮盒 (六入)",
                    "NT$ 120",
                    new Uri("http://www.nargo.com.tw/image/data/0725/_MG_1928.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1409"),
                    "【尤多拉】幸福煎餅禮盒 (六入)",
                    itemContent,
                    group1));
            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【尤多拉】不淍花保濕補水幸福面膜",
                    "NT$ 320",
                    new Uri("http://www.nargo.com.tw/image/data/A010048.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1355"),
                    "日本高滲透親肌保水膜，全天侯補水、鎖水 深入肌膚深層提升保濕，產品完全不含Paraben(苯鉀酸脂類)防腐劑、人工色素、無酒精添加",
                    itemContent,
                    group1));

            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【躉泰】芋頭酥(12入)",
                    "NT$ 360",
                    new Uri("http://www.nargo.com.tw/image/data/A13002.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-869"),
                    "新鮮的芋頭加上手工麻糬，芋頭的香甜以及麻糬的Q嫩，宜人的口感，在簡單的外表下，蘊藏著絕對的美味，絕對是佐茶伴友，聊天說地的最佳茶點。",
                    itemContent,
                    group1));

            group1.Items.Add(
                new SampleDataItem(
                    "Group-1-Item-1",
                    "【綠誠】爽很大",
                    "NT$ 150",
                    new Uri("http://www.nargo.com.tw/image/cache/data/66-228x228.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-862"),
                    "輕鬆、自然、獨特的新包裝、新口味，一袋12包，芥末、麻辣、鹽酥雞、原味等四種口味，新鮮蔬菜精製而成的休閒點心，最適合郊遊、聊天、看電視、早餐、宵夜、茶點，隨手一包、百吃不厭，絕妙好搭配，片片好滋味。一推出，就造成熱賣。",
                    itemContent,
                    group1));

            this.AllGroups.Add(group1);
        }

        public ObservableCollection<SampleDataCollection> AllGroups
        {
            get { return this.allGroups; }
        }

        public static SampleDataCollection GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }
    }

    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataCollection"/> that
    /// defines properties common to both.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public abstract class SampleDataCommon : BindableBase
    {
        /// <summary>
        /// baseUri for image loading purposes
        /// </summary>
        private static Uri baseUri = new Uri("pack://application:,,,/");

        /// <summary>
        /// Field to store uniqueId
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Field to store title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Field to store subtitle
        /// </summary>
        private string subtitle = string.Empty;

        /// <summary>
        /// Field to store description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Field to store image
        /// </summary>
        private ImageSource image = null;

        /// <summary>
        /// Field to store image path
        /// </summary>
        private Uri imagePath = null;

        private ImageSource image2 = null;
        private Uri imagePath2 = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataCommon" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        protected SampleDataCommon(string uniqueId, string title, string subtitle, Uri imagePath, Uri imagePath2, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = imagePath;
            this.imagePath2 = imagePath2;
        }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty(ref this.uniqueId, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.SetProperty(ref this.subtitle, value); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(SampleDataCommon.baseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                this.SetProperty(ref this.image, value);
            }
        }

        public void SetImage(Uri path)
        {
            this.image = null;
            this.imagePath = path;
            this.OnPropertyChanged("Image");
        }


        public ImageSource Image2
        {
            get
            {
                if (this.image2 == null && this.imagePath2 != null)
                {
                    this.image2 = new BitmapImage(new Uri(SampleDataCommon.baseUri, this.imagePath2));
                }

                return this.image2;
            }

            set
            {
                this.imagePath2 = null;
                this.SetProperty(ref this.image2, value);
            }
        }

        public void SetImage2(Uri path2)
        {
            this.image2 = null;
            this.imagePath2 = path2;
            this.OnPropertyChanged("Image2");
        }




        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public class SampleDataItem : SampleDataCommon
    {
        private string content = string.Empty;
        private SampleDataCollection group;
        private Type navigationPage;

        public SampleDataItem(string uniqueId, string title, string subtitle, Uri imagePath, Uri imagePath2, string description, string content, SampleDataCollection group)
            : base(uniqueId, title, subtitle, imagePath, imagePath2, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataItem" /> class.
        /// </summary>
        /// <param name="uniqueId">The unique id of this item.</param>
        /// <param name="title">The title of this item.</param>
        /// <param name="subtitle">The subtitle of this item.</param>
        /// <param name="imagePath">A relative path of the image for this item.</param>
        /// <param name="description">A description of this item.</param>
        /// <param name="content">The content of this item.</param>
        /// <param name="group">The group of this item.</param>
        /// <param name="navigationPage">What page should launch when clicking this item.</param>
        public SampleDataItem(string uniqueId, string title, string subtitle, Uri imagePath, Uri imagePath2, string description, string content, SampleDataCollection group, Type navigationPage)
            : base(uniqueId, title, subtitle, imagePath, imagePath2, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = navigationPage;
        }

        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        public SampleDataCollection Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        public Type NavigationPage
        {
            get { return this.navigationPage; }
            set { this.SetProperty(ref this.navigationPage, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataCollection : SampleDataCommon, IEnumerable
    {
        private ObservableCollection<SampleDataItem> items = new ObservableCollection<SampleDataItem>();
        private ObservableCollection<SampleDataItem> topItem = new ObservableCollection<SampleDataItem>();

        public SampleDataCollection(string uniqueId, string title, string subtitle, Uri imagePath, Uri imagePath2, string description)
            : base(uniqueId, title, subtitle, imagePath, imagePath2, description)
        {
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
        }

        public ObservableCollection<SampleDataItem> Items
        {
            get { return this.items; }
        }

        public ObservableCollection<SampleDataItem> TopItems
        {
            get { return this.topItem; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        if (this.TopItems.Count > 12)
                        {
                            this.TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        this.TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        this.TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        this.TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        if (this.Items.Count >= 12)
                        {
                            this.TopItems.Add(this.Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems[e.OldStartingIndex] = this.Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TopItems.Clear();
                    while (this.TopItems.Count < this.Items.Count && this.TopItems.Count < 12)
                    {
                        this.TopItems.Add(this.Items[this.TopItems.Count]);
                    }

                    break;
            }
        }
    }
}
