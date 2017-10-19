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

namespace GraphicalSubtitles
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Parser Sub;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void PlayMedia(object sender, RoutedEventArgs args)
        {
            video.Play();
            Sub = new Parser(subtitlesBox);
        }
        /*private void PauseMedia(object sender, RoutedEventArgs args)
        {
            video.Pause();
        }
        private void StopMedia(object sender, RoutedEventArgs args)
        {
            video.Stop();
        }*/
    }
}
