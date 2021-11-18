using AVFoundation;
using Foundation;
using AudioOuting.iOS;
using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using static AudioOuting.AudioPage;

namespace AudioOuting.iOS
{
    public class AVService_iOS : IAVService
    {
        NSError error;
        public void setSessionCategory(bool isSpeaker)
        {
            AVAudioSession.SharedInstance().SetCategory(isSpeaker == true ? AVAudioSession.CategoryPlayback : AVAudioSession.CategoryPlayAndRecord, out error);
        }
    }
}