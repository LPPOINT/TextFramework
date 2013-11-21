using System;
using System.Globalization;
using System.Windows;
using Rose.TextFramework.UI.Models;

namespace Rose.TextFramework.UI.Win
{
    public partial class MessageRandomNumberContent : IModelControl
    {
        private object model;

        public MessageRandomNumberContent()
        {
            InitializeComponent();
        }

        public object Model
        {
            get { return model; }
            set
            {
                model = value;
                var asRandom = model as RandomNumberModel;

                Min.Text = asRandom.From.ToString(CultureInfo.InvariantCulture);
                Max.Text = asRandom.To.ToString(CultureInfo.InvariantCulture);
                Result.Text = asRandom.Number.ToString(CultureInfo.InvariantCulture);

            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = Convert.ToInt32(Min.Text);
                var to = Convert.ToInt32(Max.Text);

                var number = new Random().Next(from, to);

                var asRandom = model as RandomNumberModel;
                asRandom.From = from;
                asRandom.To = to;
                asRandom.Number = number;

                Result.Text = number.ToString(CultureInfo.InvariantCulture);

            }
            catch (Exception)
            {
            }
        }
    }
}
