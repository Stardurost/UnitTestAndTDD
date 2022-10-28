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
using System.Media;
using System.Linq.Expressions;
using NAudio.Wave;
using System.IO;
using System.Collections;

namespace WpfChords
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SoundPlayer player;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string NameFile = "";
            NameFile =Convert.ToString(DateTime.Now);
            NameFile = NameFile.Replace(":"," ");
            //MessageBox.Show(NameFile);                тестирование конвертирования

            string[] chords = new string[20];
            string a = Convert.ToString(StringCh.Text);
            ArrayList StringFiles = new ArrayList();
            chords = a.Split();

            StringFiles = ConvertToFile(chords);
            string PathFile;
            PathFile = "C:\\Users\\Machenike\\source\\repos\\WpfChords\\MusicFiles\\Song"+NameFile+".wav";
            
            FileStream fs = File.Create(PathFile);
            fs.Close();

            Concatenate(PathFile, StringFiles);

            Play(PathFile);
        }

        public ArrayList ConvertToFile(string[] ListChords)
        {
            ArrayList StringFilesName = new ArrayList(20);
            for (int i = 0; i < ListChords.Length; i++)
            {
                StringFilesName.Add("C:/Users/Machenike/source/repos/WpfChords/MusicFiles/Chords " + ListChords[i]+".wav");
            }
            return StringFilesName;
        }

        private static void Concatenate(string outputFile, ArrayList sourceFiles)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter waveFileWriter = null;
            try
            {
                foreach (string sourceFile in sourceFiles)
                {
                    using (WaveFileReader reader = new WaveFileReader(sourceFile))
                    {
                        if (waveFileWriter == null)
                        {
                            // first time in create new Writer
                            waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                        }
                        else
                        {
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            {
                                MessageBox.Show("Can't concatenate WAV Files that don't share the same format");
                                //throw new InvalidOperationException
                            }
                        }
                        int read;
                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.Write(buffer, 0, read);
                        }
                        
                    }
                }
                waveFileWriter.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (waveFileWriter != null)
                {
                    waveFileWriter.Dispose();
                }
            }
        }
        private void Play(string PathFile)
        {
            try
            {
                WaveStream mainOutputStream = new WaveFileReader(@PathFile);
                WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

                WaveOutEvent player = new WaveOutEvent();

                player.Init(volumeStream);
                player.Play();
                
            }
            catch
            {
                MessageBox.Show("Файл не найден");
            }
        }

        private void StringCh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            {
                if (IsNumber(e.Text) != false)
                {
                    e.Handled = true;
                }

            }
        }


        public bool IsNumber(string Text)
        {
            int output;
            return int.TryParse(Text, out output);

        }
    
    }

}
