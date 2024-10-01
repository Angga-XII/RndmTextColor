using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace hana
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int _count = 0;
        int _timer = 0;
        int[] _countTwo = { 0, 0};
        int[] _countThree = { 0, 0};
        int[] _countFour = { 0, 0};
        int[] _countFive = { 0, 0};
        private static Timer timer;
        int numArrays;
        private static bool stopGenerating = false;
        private bool isTbEnabled = false;
        string _temp = "";
        
        private async void StartTest(object sender, RoutedEventArgs e)
        {
            // Set up a timer to stop generating after 1 minute
            //Console.WriteLine("test1: " + ((ComboBoxItem)timerComboBox.SelectedValue).Content.ToString());
            //int minute = Convert.ToInt32(((ComboBoxItem)timerComboBox.SelectedValue).Content.ToString());
            //Timer stopTimer = new Timer(1000 * 60 * 4);
            //stopTimer.Elapsed += StopTimer_Elapsed; ;
            //stopTimer.AutoReset = false;
            //stopTimer.Enabled = true;
            stopGenerating = false;
            _timer = 0;
            _countTwo = new int[]{ 0, 0 };
            _countThree = new int[] { 0, 0 };
            _countFour = new int[] { 0, 0 };
            _countFive = new int[] { 0, 0 };
            GenerateLogic();
        }

        private async void  GenerateLogic()
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                if (_countTwo[0] + _countThree[0] + _countFour[0] + _countFive[0] == _limit*4)
                {
                    applicationOver();
                }
                else 
                {
                    while (randomNumberTextBlock.Text == "")
                    {
                        //textbox1.IsEnabled = false;
                        Random random = new Random();
                        randomNumberTextBlock.Text = "";
                        _count = 0;
                        // Generate a random number between 2 and 5 for the number of arrays
                        textbox1.IsEnabled = false;
                        numArrays = random.Next(2, 6);
                        //int[] rand_logic = { -2, -1, 1, 2 };
                        int[] rand_logic = { -1, 1 };

                        // Generate and print random arrays
                        //_count = numArrays * 4;
                        string _result = "2 chunks: " + _countTwo[0] + " , " + _countTwo[1] + "\n"
                                            + "3 chunks: " + _countThree[0] + " , " + _countThree[1] + "\n"
                                            + "4 chunks: " + _countFour[0] + " , " + _countFour[1] + "\n"
                                            + "5 chunks: " + _countFive[0] + " , " + _countFive[1] + "\n";
                        Console.WriteLine("it's over!\n" + _result);
                        if (typeCounter(numArrays))
                        {
                            for (int i = 0; i < numArrays; i++)
                            {
                                //int index = random.Next(0, 4);
                                int index = random.Next(0, 2);

                                int[] logic = { rand_logic[index], rand_logic[index], rand_logic[index] };
                                // Generate a random number between 0 and 9 for the first element (index 0, 0)
                                int firstValue = random.Next(10);

                                //int numberAmount = random.Next(2, 5);
                                int numberAmount = 4;

                                // Use the first value to init the array
                                int[] randomArray = new int[numberAmount];
                                randomArray[0] = firstValue;

                                _count += numberAmount;
                                // Increment or decrement other values in the array relative to the previous value
                                Console.WriteLine("test count:" + numberAmount.ToString());
                                for (int j = 1; j < numberAmount; j++)
                                {
                                    randomArray[j] = (randomArray[j - 1] + logic[j - 1] + 10) % 10;
                                }
                                Run _tempRun;
                                if (i == numArrays - 1)
                                {
                                    _tempRun = new Run(string.Join("", randomArray));
                                }
                                else
                                {
                                    _tempRun = new Run(string.Join("", randomArray));
                                }
                                int colorRandom = random.Next(0, 6);
                                _tempRun.Foreground = _brushes[colorRandom];
                                randomNumberTextBlock.Inlines.Add(_tempRun);
                                _temp = randomNumberTextBlock.Text;
                                resetTimer(false);
                            }
                        }
                    }
                    Console.WriteLine("test generate: " + randomNumberTextBlock.Text);
                    _timer += _count;
                    this.Title = _timer.ToString();
                }
            });
        }
        private void resetTimer(bool isAnswer)
        {
            //Console.WriteLine("test reset: " + _count);
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
            if (isAnswer)
            {
                timer = new Timer(1000 * _count);
            }
            else 
            {
                timer = new Timer(1000 * _count);
            }
            
            timer.Elapsed += OnTimerElapsed;
            timer.Enabled = true;
        }

        int _limit = 7;
        private bool typeCounter(int numArray) 
        {
            switch (numArray)
            {
                case 2:
                    if (_countTwo[0] < _limit)
                    {
                        _countTwo[0]++;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                    
                case 3:
                    if (_countThree[0] < _limit)
                    {
                        _countThree[0]++;
                        return true;
                    }
                    else
                    {
                        return false;
                    };
                case 4:
                    if (_countFour[0] < _limit)
                    {
                        _countFour[0]++;
                        return true;
                    }
                    else
                    {
                        return false;
                    };
                case 5:
                    if (_countFive[0] < _limit)
                    {
                        _countFive[0]++;
                        return true;
                    }
                    else
                    {
                        return false;
                    };
                default:
                    return false;
            }

        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                //Console.WriteLine("test elapsed: " + _count);
                //checkAnswer();
                if (!isTbEnabled)
                {
                    isTbEnabled = true;
                    randomNumberTextBlock.Text = string.Empty;
                    textbox1.IsEnabled = isTbEnabled;
                    textbox1.Focus();
                    textbox1.Text = "";
                    resetTimer(true);
                }
                else
                {
                    isTbEnabled = false;
                    if (!stopGenerating)
                    {
                        textbox1.Text = "";
                        GenerateLogic();
                    }
                }
            });
        }

        private void StopTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            applicationOver();
        }

        private void applicationOver() 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                stopGenerating = true;
                timer.Stop();
                timer.Dispose();
                textbox1.Text = "";
                randomNumberTextBlock.Text = string.Empty;
                // Stop the timer when 1 minute has passed
                string _result = "2 chunks: " + _countTwo[0] + " , " + _countTwo[1] + "\n"
                    + "3 chunks: " + _countThree[0] + " , " + _countThree[1] + "\n"
                    + "4 chunks: " + _countFour[0] + " , " + _countFour[1] + "\n"
                    + "5 chunks: " + _countFive[0] + " , " + _countFive[1] + "\n";
                MessageBox.Show("it's over!\n" + _result);
            });
        }

        //Magenta Red Yellow Orange
        Brush[] _brushes = {Brushes.Magenta, Brushes.Red, Brushes.Yellow, Brushes.Orange, Brushes.Magenta, Brushes.Red };
        private void ColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedColor = ((ComboBoxItem)colorComboBox.SelectedItem).Content.ToString();

            switch (selectedColor)
            {
                case "First":
                    {
                      _brushes = new Brush[]{ Brushes.White, Brushes.White, Brushes.White, Brushes.White, Brushes.White, Brushes.White };
                    }
                    break;

                case "Second":
                    _brushes = new Brush[] { Brushes.Magenta, Brushes.Magenta, Brushes.Magenta, Brushes.Magenta, Brushes.Magenta, Brushes.Magenta };
                    break;

                case "Third":
                    _brushes = new Brush[] { Brushes.Magenta, Brushes.Red, Brushes.Yellow, Brushes.Cyan, Brushes.Green, Brushes.Magenta };
                    break;
                default:
                    break;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                checkAnswer();

                textbox1.Text = "";
                isTbEnabled = false;
                GenerateLogic();
            }
        }

        private void checkAnswer() 
        {
            Console.WriteLine("test answer: " + textbox1.Text + " : " + _temp);
            if (textbox1.Text == _temp)
            {
                switch (numArrays)
                {
                    case 2:
                        _countTwo[1]++;
                        break;
                    case 3:
                        _countThree[1]++;
                        break;
                    case 4:
                        _countFour[1]++;
                        break;
                    case 5:
                        _countFive[1]++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
