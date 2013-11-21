using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Rose.TextFramework.Moduling;
using Rose.TextFramework.RoseMark;

namespace Rose.TextFramework.Utils.RMarkExpressionsChecker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private enum StatusLabelColor
        {
            Green,
            Red,
            Black
        }

        private void SetStatusLabelMessage(string message, StatusLabelColor color = StatusLabelColor.Black)
        {
            var colorfg = new Color();
            switch (color)
            {
                case StatusLabelColor.Black:
                    colorfg = Colors.Black;
                    break;
                case StatusLabelColor.Red:
                    colorfg = Colors.Red;
                    break;
                default:
                    colorfg = Colors.Green;
                    break;
            }

            StatusLabel.Visibility = Visibility.Visible;;
            StatusLabel.Content = message;
            StatusLabel.Foreground = new SolidColorBrush(colorfg);

        }

        private void CalculateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputBox.Text == string.Empty || PatternBox.Text == string.Empty)
                {
                    SetStatusLabelMessage("Следует заполнить все поля формы", StatusLabelColor.Red);
                }

                var expression = new RoseMarkExpression(PatternBox.Text);
                var encodeDataHolder = new Dictionary<string, object>();
                var res = new Dictionary<string, object>();
                var stopwatch = new Stopwatch();
                stopwatch.Start();;
                var compileInfo = new CompileInfo
                                  {
                                      Input = InputBox.Text,
                                      PrecompiledDataHolder = res,
                                      ShouldCheckLenght = true,
                                      EncodeDataHolder = encodeDataHolder
                                  };
                var result = expression.Compile(compileInfo);

                if (result.IsCompiled)
                {
                    SetStatusLabelMessage("Cоответствие", StatusLabelColor.Green);
                }
                else
                {
                    SetStatusLabelMessage("Несоответствие", StatusLabelColor.Red);
                }

                    var builder = new StringBuilder();

                    builder.AppendLine("Затрачено времени: " + stopwatch.Elapsed);

                    var token = ExpressionToken.Parse(PatternBox.Text);
                    builder.AppendLine("Количество элементов токена: " + token.Elements.Count);

                foreach (var el in token.Elements)
                {
                    builder.AppendLine("Элемент '" + el.Text + "': Тип:" + el.GetType());
                }
                    
                    builder.AppendLine("Количество данных кодировщика: " + encodeDataHolder.Count);
                    foreach (var o in encodeDataHolder)
                    {
                        builder.AppendLine(o.Key + ": " + o.Value);
                    }
                    ResultOutput.Text = builder.ToString();


            }
            catch (Exception exception)
            {
                SetStatusLabelMessage("Ошибка во время выполнения", StatusLabelColor.Red);
            }

        }
    }
}
