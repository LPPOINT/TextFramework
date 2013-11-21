using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Rose.TextFramework.Parsing;
using Rose.TextFramework.RoseMark;
using Rose.TextFramework.UI.Models;
using Rose.TextFramework.Voice;

namespace Rose.TextFramework.UI.Win
{


    public enum MessageAligment
    {
        Left,
        Right
    }

    public partial class MainWindow
    {

        public Dictionary<Type, Type> ModelMapping { get; private set; } 

        public void AddMessage(Control messageContentControl, MessageAligment alignment)
        {
            var messageBorder = new MessageNode();
            messageBorder.Children = messageContentControl;
            messageBorder.Margin = new Thickness(10, 10, 0, 0);
            messageBorder.HorizontalAlignment = HorizontalAlignment.Center;
            MessagesRoot.Children.Add(messageBorder);
        }

        public MainWindow()
        {
            InitializeComponent();
            ModelMapping = new Dictionary<Type, Type>();

            var module = new TestModule();
            RoseMarkExpression.CommonExecutors.Add(new UseFunctionExecutor());
            var provider = new TextMappingEngine();
            provider.Modules.Add(module);
            ModuleRequest.RequestProvider = provider;

            ModelMapping.Add(typeof(string), typeof(MessageTextContent));
            ModelMapping.Add(typeof(RandomNumberModel), typeof(MessageRandomNumberContent));
        }

        private void RequestClick(object sender, RoutedEventArgs e)
        {
            if(Input.Text == string.Empty)
                return;

            var q = Input.Text;
            var stopwatch = new Stopwatch();
            var request = new ModuleRequest(q);
            var response = request.GetResponse();
            var speaker = new Speaker();

            if (response != null)
            {
                stopwatch.Stop();
            }

            var model = response.Content;

            if (ModelMapping.ContainsKey(model.GetType()))
            {
                var controlType = ModelMapping[model.GetType()];
                var instanse = Activator.CreateInstance(controlType);

                if (instanse is Control && instanse is IModelControl)
                {
                    var asModel = instanse as IModelControl;
                    asModel.Model = model;
                    AddMessage(instanse as Control, MessageAligment.Left);
                }

            }
            else
            {
                var text = new MessageTextContent();
                text.Model = model.ToString();
                AddMessage(text, MessageAligment.Left);
            }

            if (response.Content.ToString() != string.Empty)
            {
            }
        }
    }
}
