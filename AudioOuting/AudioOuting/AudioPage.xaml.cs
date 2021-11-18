using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.AudioRecorder;

namespace AudioOuting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
        public partial class AudioPage : ContentPage
    {
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService()
        {
            StopRecordingOnSilence = false,
            StopRecordingAfterTimeout = false
        };
        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public AudioPage()
        {
            // add a bit of padding to cater to the "notch" on the iPhone.
            if (Device.RuntimePlatform == Device.iOS) { Padding = new Thickness(0, 40, 0, 0); }

            InitializeComponent();

            string selAudio = Preferences.Get("selAudio_key", "none");
            if (selAudio == "none")
            {

            }
            else
            {
                LastAudio.Text = selAudio;
            }
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public interface IAVService
        {
            void setSessionCategory(bool isSpeaker);
        }

        async void RecordNewAudio_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();

            if (status != PermissionStatus.Granted)
                return;

            if (audioRecorderService.IsRecording)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    DependencyService.Get<IAVService>().setSessionCategory(false);//set CategoryPlayAndRecord
                }

                await audioRecorderService.StopRecording();

                AudioImage.Source = "flatlineblue.png";
                RecordNewAudio.Text = "Record New Audio";
                RecordNewAudio.TextColor = Color.Black;

                try
                {
                    // Use default vibration length
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }

                string selAudio = audioRecorderService.FilePath;
                Preferences.Set("selAudio_key", selAudio);
                LastAudio.Text = selAudio;
                LastAudio.TextColor = Color.Green;

            }
            else
            {
                try
                {
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }

                AudioImage.Source = "audiocrop.gif";
                RecordNewAudio.Text = "Recording... Click to Finish";
                RecordNewAudio.TextColor = Color.Red;

                await audioRecorderService.StartRecording();

            }

        }

        private void PlaySelectedAudioButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Vibration.Vibrate();

                if (Device.RuntimePlatform == Device.iOS)
                {
                    DependencyService.Get<IAVService>().setSessionCategory(true);
                }

                audioPlayer.Play(audioRecorderService.FilePath);
            }
            catch (Exception ex)
            {
                // an error has occurred.
            }
        }
    }
}