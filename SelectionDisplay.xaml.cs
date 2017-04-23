//------------------------------------------------------------------------------
// <copyright file="SelectionDisplay.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.FaceBasics
{
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic
    /// </summary>
    public partial class SelectionDisplay : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionDisplay"/> class. 
        /// </summary>
        /// <param name="itemId">ID of the item that was selected</param>
        public SelectionDisplay(string title, string price, string description, ImageSource imageProduct, ImageSource imageQRcode)
        {
            this.InitializeComponent();

            this.messageTextBlock.Text = string.Format(CultureInfo.CurrentCulture, Properties.Resources.SelectedMessage, title);
            this.title.Text = title;
            this.price.Text = price;
            this.description.Text = description;
            this.imageProduct.Source = imageProduct;
            this.imageQRcode.Source = imageQRcode;
        }

        /// <summary>
        /// Called when the OnLoaded storyboard completes.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnLoadedStoryboardCompleted(object sender, System.EventArgs e)
        {
            var parent = (Panel)this.Parent;
            parent.Children.Remove(this);
        }
    }
}
