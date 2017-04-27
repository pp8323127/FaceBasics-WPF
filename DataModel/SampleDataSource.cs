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


//========================
            var group0 = new SampleDataCollection(
                    "Group-0",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    SampleDataSource.mediumGrayImage,
                    SampleDataSource.mediumGrayImage,
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-1",
            //            "Buttons",
            //            "1231321321321321313132",
            //            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
            //            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
            //            "Several types of buttons with custom styles",
            //            "123333",
            //            group0,
            //            typeof(ButtonSample)));
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-2",
            //            "CheckBoxes and RadioButtons",
            //            string.Empty,
            //            SampleDataSource.a,
            //            SampleDataSource.mediumGrayImage,
            //            "CheckBox and RadioButton controls",
            //            itemContent,
            //            group0,
            //            typeof(CheckBoxRadioButtonSample)));
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-5",
            //            "Zoomable Photo",
            //            string.Empty,
            //            SampleDataSource.lightGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "ScrollViewer control hosting a photo, enabling scrolling and zooming.",
            //            itemContent,
            //            group0,
            //            typeof(ScrollViewerSample)));
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-7",
            //            "Engagement and Cursor Settings",
            //            "",
            //            SampleDataSource.darkGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Enables user to switch between engagement models and cursor visuals.",
            //            itemContent,
            //            group0,
            //            typeof(EngagementSettings)));
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-6",
            //            "Kinect Pointer Events",
            //            string.Empty,
            //            SampleDataSource.lightGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Example of how to get KinectPointerPoints.",
            //            itemContent,
            //            group0,
            //            typeof(KinectPointerPointSample)));
            //group0.Items.Add(
            //        new SampleDataItem(
            //            "Group-1-Item-7",
            //            "Engagement and Cursor Settings",
            //            "",
            //            SampleDataSource.darkGrayImage,
            //            SampleDataSource.mediumGrayImage,
            //            "Enables user to switch between engagement models and cursor visuals.",
            //            itemContent,
            //            group0,
            //            typeof(EngagementSettings)));
            group0.Items.Add(
                    new SampleDataItem(
                        "Group-0-Item-1",
                        "黃金甜蜜地瓜飲單瓶",
                        "NT$ 49",
                        new Uri("http://www.nargo.com.tw/image/data/0725/T9.jpg"),
                        new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1576"),
                        "黃金甜蜜推出營養地瓜飲，香濃的氣味來自台灣產地直銷的天然地瓜，也來自台灣農人的用心耕耘，少了化學的藥品、少了華而不實的香氣，以最簡單的原料—地瓜，製成一瓶瓶健康的飲品，用最簡單的心做最好的食品，用心將地瓜原本最美好的風味封存入一罐罐地瓜飲裡面，濃郁香甜，生津止渴。",
                        itemContent,
                        group0));

            group0.Items.Add(
                    new SampleDataItem(
                        "Group-0-Item-2",
                        "嘻豆非基因改造黃豆(高雄9號)",
                        "NT$ 150",
                        new Uri("http://www.nargo.com.tw/image/data/0725/T7.jpg"),
                        new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1575"),
                        "台灣每年進口約250萬公噸的大豆，有九成以上是基改大豆。本土黃豆新鮮、採收後直接冷藏，比進口黃豆安心許多，也很適合乾旱缺水的極端氣候，希望大家能多支持。高雄選9號黃豆，飽滿豆香濃，做豆漿或放入飯中一起烹煮都很合適！",
                        itemContent,
                        group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "嘻豆-黑白豆",
                    "NT$ 130",
                    new Uri("http://www.nargo.com.tw/image/data/0725/t5.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1571"),
                    "黑白豆-以高雄7號+高雄9號煎焙製造而成，香酥可口．豆香迷人！",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "新纖豆點",
                    "NT$250",
                    new Uri("http://www.nargo.com.tw/image/data/0725/t1.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1564"),
                    "嚴選煎焙台灣非基改黃豆、青仁黑豆、全果粒蔓越莓(糖、蔓越莓、葵花籽油)、南瓜子完美比例,讓人一口接一口停不下來",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【尤多拉】紅藜穀物棒",
                    "NT$ 200",
                    new Uri("http://www.nargo.com.tw/image/data/0725/123.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1556"),
                    "「德朱利斯」，音譯自DJULIS，為「台灣紅藜」的原住民發音。是大自然給予台東農民的恩賜。「紅藜」被稱為「穀類界的紅寶石」，然而，一顆顆紅寶石若無人聞問，也只能落地歸還給大自然。看見紅藜的光芒，我們捧起紅藜，牽起台東農民的手，創造出專屬於台東的特色伴手禮，將在地化產品推向國際。紅 藜 穀 物 棒紅藜香氣及酥餅的香酥脆，非常涮嘴，包您一口接著一口！",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【尤多拉】巧克彩虹卷系列-15入",
                    "NT$ 240",
                    new Uri("http://www.nargo.com.tw/image/data/0725/_MG_2187.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1559"),
                    "以日本老師傅技藝將厚實的手工日式蛋捲與香濃巧克力合而為一，傳統元素結合創新工法，為您的味覺開啟彩虹般的奇幻旅程繽粉華麗的手工日式蛋捲！有香蕉巧克力草莓巧克力黑巧克力三種口味除了視覺上的享受更讓您可一次品嚐三種不同蛋捲的獨特風味。",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【尤多拉】心有所屬芥末",
                    "NT$ 130",
                    new Uri("http://www.nargo.com.tw/image/data/0725/7813.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1542"),
                    "【尤多拉】心有所屬芥末",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "《南投拌手禮》冬筍餅禮盒",
                    "NT$ 150",
                    new Uri("http://www.nargo.com.tw/image/data/0725/n5.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1504"),
                    "到南投竹山遊玩，除了品嚐當地名產之外，當地人一定不忘推薦您嚐嚐當地有名的「日香食品」冬筍餅，它是陪伴許多五年級生度過繽紛的童年回憶的零嘴，深受許許多多忠實粉絲的喜愛與支持！",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【尤多拉】幸福煎餅禮盒 (六入)",
                    "NT$ 120",
                    new Uri("http://www.nargo.com.tw/image/data/0725/_MG_1928.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1409"),
                    "【尤多拉】幸福煎餅禮盒 (六入)",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【尤多拉】不淍花保濕補水幸福面膜",
                    "NT$ 320",
                    new Uri("http://www.nargo.com.tw/image/data/A010048.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-1355"),
                    "日本高滲透親肌保水膜，全天侯補水、鎖水 深入肌膚深層提升保濕，產品完全不含Paraben(苯鉀酸脂類)防腐劑、人工色素、無酒精添加",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【躉泰】芋頭酥(12入)",
                    "NT$ 360",
                    new Uri("http://www.nargo.com.tw/image/data/A13002.JPG"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-869"),
                    "新鮮的芋頭加上手工麻糬，芋頭的香甜以及麻糬的Q嫩，宜人的口感，在簡單的外表下，蘊藏著絕對的美味，絕對是佐茶伴友，聊天說地的最佳茶點。",
                    itemContent,
                    group0));

            group0.Items.Add(
                new SampleDataItem(
                    "Group-0-Item-1",
                    "【綠誠】爽很大",
                    "NT$ 150",
                    new Uri("http://www.nargo.com.tw/image/cache/data/66-228x228.jpg"),
                    new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=nargo-862"),
                    "輕鬆、自然、獨特的新包裝、新口味，一袋12包，芥末、麻辣、鹽酥雞、原味等四種口味，新鮮蔬菜精製而成的休閒點心，最適合郊遊、聊天、看電視、早餐、宵夜、茶點，隨手一包、百吃不厭，絕妙好搭配，片片好滋味。一推出，就造成熱賣。",
                    itemContent,
                    group0));

            this.AllGroups.Add(group0);


//=============================
            var group1 = new SampleDataCollection(
                "Group-1",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-1",
            "【綠誠】爽很大",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/66-500x500.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-862"),
            "輕鬆、自然、獨特的新包裝、新口味，一袋12包，芥末、麻辣、鹽酥雞、原味等四種口味，新鮮蔬菜精製而成的休閒點心，最適合郊遊、聊天、看電視、早餐、宵夜、茶點，隨手一包、百吃不厭，絕妙好搭配，片片好滋味。一推出，就造成熱賣。",
            itemContent,
            group1));

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-2",
            "《南投拌手禮》美人腿湯麵(肉燥)",
            "NT$30",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/n4-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1502"),
            "《南投拌手禮》美人腿湯麵(肉燥)",
            itemContent,
            group1));

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-3",
            "《南投拌手禮》美人腿湯麵(牛肉)",
            "NT$30",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/n3-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1500"),
            "《南投拌手禮》美人腿湯麵(牛肉)",
            itemContent,
            group1));

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-4",
            "【尤多拉】好運桶─起司口味(1桶)",
            "NT$130",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/_MG_1948-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-128"),
            "香濃的起司粉包覆著每一顆玉米爆，鹹鹹甜甜的滋味讓你一口接一口。",
            itemContent,
            group1));

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-5",
            "【綠誠】遇發則發-鱈魚片(胡椒口味)",
            "NT$100",
            new Uri("http://www.nargo.com.tw/image/cache/data/A03006-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-617"),
            "此商品不參加一口咬出好文寫推文送美食活動",
            itemContent,
            group1));

            group1.Items.Add(
            new SampleDataItem(
            "group1-Item-6",
            "【大尖山】雲林古坑高山二合一咖啡(18入)",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/A02002-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-95"),
            "因更換包裝，故實體與照片不同，但內容物不變，請安心選購。",
            itemContent,
            group1));

            this.AllGroups.Add(group1);


//=============================
            var group2 = new SampleDataCollection(
                "Group-2",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-1",
            "【小林煎餅】瓦煎燒禮盒(1盒)",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/A01004-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-92"),
            "瓦片自古以來即為富貴吉祥之象徵。小瓦片口感較大瓦片稍扎實些，帶有濃郁牛奶香，餅紋依時令節慶變化，單獨小包裝最適合獨享。",
            itemContent,
            group2));

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-2",
            "《南投拌手禮》麻辣香菇醬",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/pic_37099__20111230185240b-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-982"),
            "◇用途：拌飯、拌麵、素粽…等佐料，清粥小菜、中西式麵食配料(饅頭、土司、飯糰)炒菜調味、火鍋湯底皆可。",
            itemContent,
            group2));

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-3",
            "【萬味軒】杏仁肉乾(1包)",
            "NT$200",
            new Uri("http://www.nargo.com.tw/image/cache/data/A24008-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-253"),
            "杏仁的香氣，加上牛肉的咬勁，一口接著一口，讓人愛不釋手。",
            itemContent,
            group2));

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-4",
            "【躉泰】紫晶酥(12入)",
            "NT$360",
            new Uri("http://www.nargo.com.tw/image/cache/data/7896-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-869"),
            "新鮮的芋頭加上手工麻糬，芋頭的香甜以及麻糬的Q嫩，宜人的口感，在簡單的外表下，蘊藏著絕對的美味，絕對是佐茶伴友，聊天說地的最佳茶點。",
            itemContent,
            group2));

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-5",
            "《屏東物產館》新丁有機轉型期烘焙咖啡豆",
            "NT$500",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/p1-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1490"),
            "生長於屏東北大武山889公尺的新丁咖啡，為阿拉比卡品種，秉持自然工法栽培，過程中絕無使用化學農藥。採後的生果在吳錦柱的堅持下，顆顆精選，並必須整整於南部豔陽下曝曬72天(有別一般咖啡豆僅經日曬7天)，因此相當珍貴優質。獨特的製作工序，讓「新丁咖啡」散發出一種屏東獨有客家香。阿拉比卡品種生長環境非常重要，低緯度高海拔嘉義以南，北緯23度種植在大武山條件更佳，由此緯度自然會產生日夜溫差較大會有霧氣，山坡地自然半日照，地屬於硬地，這樣的選擇種植地產出的咖啡非常香醇口感一流，咖啡因很低，晚上喝不至於有睡眠困擾。低",
            itemContent,
            group2));

            group2.Items.Add(
            new SampleDataItem(
            "group2-Item-6",
            "《稻香》甜酒豆腐乳",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/DSC06477-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1067"),
            "《稻香》甜酒豆腐乳",
            itemContent,
            group2));

            this.AllGroups.Add(group2);


//=============================
            var group3 = new SampleDataCollection(
                "Group-3",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-1",
            "【關西農會】仙草茶茶包(90入)",
            "NT$220",
            new Uri("http://www.nargo.com.tw/image/cache/data/C04002-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-799"),
            "此商品不參加一口咬出好文好康活動",
            itemContent,
            group3));

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-2",
            "嘻豆-豆妞",
            "NT$130",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/t5-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1568"),
            "以高雄10號煎焙製造而成，香氣綿延．口齒留香！",
            itemContent,
            group3));

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-3",
            "《養生十舖》健康養生桑椹原汁400ml",
            "NT$350",
            new Uri("http://www.nargo.com.tw/image/cache/data/ssy-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1186"),
            "桑椹所含的酸味是蘋果酸。桑椹含有十八種氨基酸，同時還含有多種維他命B1、B2、C、A、D和胡蘿蔔素，葡萄糖，果糖，蘋果酸以及鈣質、鐵質等，營養成分十分豐富。",
            itemContent,
            group3));

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-4",
            "【福聯興】阿里山烏龍茶茶包清香",
            "NT$190",
            new Uri("http://www.nargo.com.tw/image/cache/data/B02005-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-752"),
            "此商品不參加一口咬出好文章好康活動",
            itemContent,
            group3));

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-5",
            "《南投拌手禮》日月潭紅茶",
            "NT$350",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/n12-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1515"),
            "南投縣魚池鄉擁有台灣最大的高山湖泊；潭之東面形似日輪，西面形如月鉤，故名為日月潭。為國際知名遊賞景點，亦是台灣紅茶的故鄉；日治時期引進栽種成功後，不僅是當時日本天皇的指定貢品，在倫敦市場上更與世界頂級紅茶齊名。",
            itemContent,
            group3));

            group3.Items.Add(
            new SampleDataItem(
            "group3-Item-6",
            "《屏東物產館》有機黑豆",
            "NT$210",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/b11-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1450"),
            ":台灣在來種黑豆包括恆春黑豆、屏東黑豆、青仁黑豆，黑豆產量與生育期日照量多寡有密切關係，日照量不足植株生長柔弱，結實不良，恆春黑豆屬於感光型黑豆品種，不宜在晚春或夏作栽培，以秋作為主。屏東縣滿州鄉盛產原生種小黑豆，入秋後，時而濕潤的氣候，加上適量日照，又有落山風吹拂，讓滿州黑豆的顆粒雖小卻很結實，加上採無農藥種植，相當受到養生主義者喜愛。",
            itemContent,
            group3));

            this.AllGroups.Add(group3);



//=============================
            var group4 = new SampleDataCollection(
                "Group-4",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-1",
            "【尤多拉】小黑糖-美研四物(1罐)",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010010-500x500.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1299"),
            "為體貼忙碌的現代人，將隨身系列小黑糖以單顆包裝，便利好攜帶，隨時隨地品味",
            itemContent,
            group4));

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-2",
            "【尤多拉】日月潭阿薩姆紅茶(1盒)",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010014-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1303"),
            "西元 1925年間由印度引進大葉種的阿薩姆紅茶栽種，結果發現種植在日月潭附近的茶葉品質最佳，將此茶出口到倫敦茶葉拍賣市場，獲得很高的評價故有台灣香的美名。",
            itemContent,
            group4));

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-3",
            "【尤多拉】炭火種烘焙玄米綠茶(1盒)",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010013-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1302"),
            "玄米飄香使茶溫潤，鮮嫩綠茶風韻清醇，嚴選在黃金75分鐘內以蒸氣殺菁，以黃金比例添加炭火烘焙，茶香與米香溫暖了每個人的心。",
            itemContent,
            group4));

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-4",
            "【尤多拉】不淍花保濕補水幸福面膜",
            "NT$320",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010048-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1355"),
            "日本高滲透親肌保水膜    全天侯補水、鎖水 深入肌膚深層提升保濕  產品完全不含Paraben(苯鉀酸脂類)防腐劑、人工色素、無酒精添加",
            itemContent,
            group4));

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-5",
            "《屏東物產館》鱈魚杏仁脆片",
            "NT$100",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/a12-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1428"),
            "口感酥脆風味獨特不加防腐劑、不加人工色素、非餅乾、非油炸、精工烘焙、無油脂不沾手、薄脆好吃、健康無負擔",
            itemContent,
            group4));

            group4.Items.Add(
            new SampleDataItem(
            "group4-Item-6",
            "【 穎創】毛巾小偶- 小美 (小兔子)",
            "NT$118",
            new Uri("http://www.nargo.com.tw/image/cache/data/TD5212-2-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-936"),
            "保證全程在台灣設計、生產、包裝！ 用真正台灣製的純棉小方巾手工精緻包裝而成。  打開後更是您方便隨身攜帶的小擦汗巾",
            itemContent,
            group4));

            this.AllGroups.Add(group4);


//=============================
            var group5 = new SampleDataCollection(
                "Group-5",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-1",
            "【太陽堂】太陽餅(6入)",
            "NT$140",
            new Uri("http://www.nargo.com.tw/image/cache/data/DSC06460-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1122"),
            "台中著名傳統糕餅點心，皮酥內軟，麥芽淡淡香氣，搭配熱茶最適合。",
            itemContent,
            group5));

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-2",
            "【小林煎餅】林桑手燒超值分享包*1包(300g / 包)",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/3-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-93"),
            "第一次接觸煎餅的最佳選擇。集合七種口味的煎餅，獨立小包裝加上保鮮夾鏈袋，人氣No.1。",
            itemContent,
            group5));

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-3",
            "【尤多拉】紫球藻修護喚采幸福面膜",
            "NT$320",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010049-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1356"),
            "日本高滲透親肌保水膜   高效潤澤 水潤瑩透     產品完全不含Paraben(苯鉀酸脂類)防腐劑、人工色素、無酒精添加",
            itemContent,
            group5));

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-4",
            "【尤多拉】紅藜洛神牛軋糖",
            "NT$250",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/125-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1557"),
            "以台東特產-紅藜及洛神為主要原料.以台東特產-紅藜及洛神為主要原料.    濃醇奶香、擁抱香脆杏仁、爽口不膩、軟Ｑ口感不黏牙，再加上洛神酸酸甜甜的滋味，讓您回到以前的戀愛滋味。",
            itemContent,
            group5));

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-5",
            "尤多拉甜甜禮盒",
            "NT$299",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/_MG_2097-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1551"),
            "尤多拉最暢銷的手工牛嘎糖、有核桃和蔓越莓二種口味、搭配二款甜心巧克力及鳳梨酥是最受客人喜愛的尤多拉禮盒",
            itemContent,
            group5));

            group5.Items.Add(
            new SampleDataItem(
            "group5-Item-6",
            "《古坑農會》加比山咖啡凍酥餅禮盒",
            "NT$380",
            new Uri("http://www.nargo.com.tw/image/cache/data/PimgbImgB9-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1190"),
            "禮盒內裝咖啡果凍粉一包、咖啡餅一盒、精緻二合一咖啡一盒、三合一咖啡兩盒。 同時享有多種的咖啡美食感，歡聚的時刻，午茶時光最愛的組合。",
            itemContent,
            group5));

            this.AllGroups.Add(group5);


//=============================
            var group6 = new SampleDataCollection(
                "Group-6",
                "Group Title: 3",
                "Group Subtitle: 3",
                SampleDataSource.mediumGrayImage,
                SampleDataSource.mediumGrayImage,
                "");

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-1",
            "【躉泰】紫晶酥(12入)",
            "NT$360",
            new Uri("http://www.nargo.com.tw/image/cache/data/7896-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-869"),
            "新鮮的芋頭加上手工麻糬，芋頭的香甜以及麻糬的Q嫩，宜人的口感，在簡單的外表下，蘊藏著絕對的美味，絕對是佐茶伴友，聊天說地的最佳茶點。",
            itemContent,
            group6));

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-2",
            "【尤多拉】小黑糖-桂圓紅棗(1罐)",
            "NT$120",
            new Uri("http://www.nargo.com.tw/image/cache/data/A01008小黑糖-桂圓紅棗-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1297"),
            "黑糖醣含量及熱量低於砂糖，還帶著一般砂糖所沒有的炭燒香氣。  為體貼忙碌的現代人，將隨身系列小黑糖以單顆包裝，便利好攜帶，隨時隨地品味",
            itemContent,
            group6));

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-3",
            "【尤多拉】雪絨花抗痕緊緻幸福面膜",
            "NT$320",
            new Uri("http://www.nargo.com.tw/image/cache/data/A010047-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1354"),
            "超修護緩齡煥顏青春膜     青春緩齡 細紋無蹤   日本高滲透親肌保水膜     產品完全不含Parabens(苯鉀酸脂類)防腐劑、人工色素、無酒精添加",
            itemContent,
            group6));

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-4",
            "《養生十舖》健康養生桑椹原汁400ml",
            "NT$350",
            new Uri("http://www.nargo.com.tw/image/cache/data/ssy-228x228.jpg"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1186"),
            "桑椹所含的酸味是蘋果酸。桑椹含有十八種氨基酸，同時還含有多種維他命B1、B2、C、A、D和胡蘿蔔素，葡萄糖，果糖，蘋果酸以及鈣質、鐵質等，營養成分十分豐富。",
            itemContent,
            group6));

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-5",
            "【西螺名產‧大同醬油】減鹽蔭油(1瓶)",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/A22003-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-240"),
            "找回在記憶中遺失的美味",
            itemContent,
            group6));

            group6.Items.Add(
            new SampleDataItem(
            "group6-Item-6",
            "《屏東物產館》南瓜麵",
            "NT$150",
            new Uri("http://www.nargo.com.tw/image/cache/data/0725/b7-228x228.JPG"),
            new Uri("http://www.nargo.com.tw/qr_print.php?level=L&size=20&data=inargo-1488"),
            "選用竹林有機農場種植的有機南瓜，採用台南意麵作法，以南瓜、麵粉、食鹽、水的完美比例，麵條堅持天然日曬，將南瓜的營養與美麗天然的鵝黃色一起揉入麵條中，讓意麵增添天然色彩而且健康美味。",
            itemContent,
            group6));

            this.AllGroups.Add(group6);
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
