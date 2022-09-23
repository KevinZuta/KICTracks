using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace KICTracks
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true; 
            clist.Add(new string[] { "hola", "fight on","danger" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hola":
                    ss.SpeakAsync("hola hola");
                    break;

                    //case "fight on":
                    //reproduce archivo mp3 en el directorio raiz, al detectar la string que genera el "fight on"
                    // ejemplo jago.mp3
                    //break;

                    ////case "fight on":
                    //reproduce archivo mp3 en bucle en el directorio raiz, al detectar la string que genera el "danger"
                    // ejemplo jagoDanger.mp3
                    //break;


            }
            txbContents.Text = e.Result.Text.ToString() + Environment.NewLine;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            btnStart.Enabled = false;
            btnStop.Enabled=true;

        }
    }
}
