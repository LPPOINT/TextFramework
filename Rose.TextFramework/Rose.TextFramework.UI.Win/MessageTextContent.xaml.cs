namespace Rose.TextFramework.UI.Win
{
    public partial class MessageTextContent : IModelControl
    {
        private string text;
        private object model;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                TextField.Text = text;
            }
        }

        public MessageTextContent()
        {
            InitializeComponent();
        }

        public object Model
        {
            get { return model; }
            set { model = value;
                Text = value.ToString();
            }
        }
    }
}
