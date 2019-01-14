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

namespace WikiPedia_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Score = 0;
        private string Text_ = "";
        private dynamic doc = " ";
        private bool ChangingTitle = false;
        private Uri StartPoint;
        private Uri EndPoint; 
        private Uri BeginningOfUri;

        public MainWindow()
        {
            InitializeComponent();
            Uri uri = new Uri("https://en.wikipedia.org/wiki/Main_Page");
            PlayerBrowser.Navigate(uri);
            Target_URI.Navigate("https://www.google.co.uk/");
            MessageBox.Show("Welcome this is the Wikipedia Game," +
                "the Aim of the game is to get from one wikipedia page to another only using wikipedia hyperlinks," +
                "the less hyperlinks used the better the score", "The Wikipedia Game Rules");
            WindowState = WindowState.Maximized;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame2();
        }
        private void NewGame2()
        {
            Uri uri = new Uri("https://en.wikipedia.org/wiki/Special:Random");
            ChangingTitle = true;
            PlayerBrowser.Navigate(uri);
            Target_URI.Navigate(uri);
            Score = 0;
            Text_ = ("Score : " + Score.ToString());
            ScoreBox.Text = (Text_);
            StartPoint = (PlayerBrowser.Source);
            EndPoint = (Target_URI.Source);
        }

        private string getWebsiteTitle(WebBrowser WebPage)
        {
            dynamic doc = WebPage.Document;
            string title = doc.title;
            try
            {
                title = title.Remove(title.Length - 12);
            }
            catch
            {
                NewGame2();
            }
            return title;
        }

        private void PlayerBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            if (ChangingTitle == true)
            {
                StartTitle.Text = (getWebsiteTitle(PlayerBrowser));
                EndTitle.Text = (getWebsiteTitle(Target_URI));

                ChangingTitle = false;
            }
            else if (ChangingTitle == false)
            {
                Score += 1;
                Text_ = ("Score : " + Score.ToString());
                ScoreBox.Text = (Text_);
                if(PlayerBrowser.Source == EndPoint)
                {
                    var result = MessageBox.Show("Your Score is: "+ Score + "\n" + "Would you like to Continue Playing?",
                        "Congratulations! you have Completed the Game", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    
                    if (result == MessageBoxResult.No)
                    {
                        Close();
                    }
                    else if (result == MessageBoxResult.Yes)
                    {
                        NewGame2();
                    }
                }

                if (PlayerBrowser.Source == )
                {

                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if ((PlayerBrowser.Source != new Uri("https://en.wikipedia.org/wiki/Main_Page") 
                &&(PlayerBrowser.Source != StartPoint) ))
            {
                PlayerBrowser.GoBack();
            }
            
            else
            {
                MessageBox.Show("\"Back Button\" cannot be used until there is a webpage to go back to", "Error");
            }
        }
    }
}
