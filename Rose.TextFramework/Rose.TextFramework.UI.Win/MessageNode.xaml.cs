using System.Windows;
using System.Windows.Controls;

namespace Rose.TextFramework.UI.Win
{
    public partial class MessageNode
    {


        private Control children;
        public Control Children
        {
            get
            {
                return children;
            }
            set
            {
                MessageRoot.Child = value;
                children = value;
                children.Margin = new Thickness(5, 5, 5, 5);
                Height = children.Height;
            }
        }

        public MessageNode()
        {
            InitializeComponent();
        }
    }
}
