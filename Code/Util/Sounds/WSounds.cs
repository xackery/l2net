using System;
using System.Media;
using System.Collections;
using System.Threading;

namespace L2_login
{
    //sounds:
    //44,100hz
    //16 bit
    //mono = stereo @ 60%

    // 1 - startup
    // 2 - closing
    // 3 - error
    // 4 - error
    // 5 - error
    // 6 - error
    // 7 - force collect
    // 8 - new mail
    // 9 - error

    public class Sound
    {
        public string Name;
        public bool isFile;
        public long PlayBefore = System.DateTime.MaxValue.Ticks;
    }

	public class WSounds
	{
        private Queue sounds = new Queue();
        private System.Threading.Thread soundengine_thread;
        
        public WSounds()
        {
            soundengine_thread = new System.Threading.Thread(new System.Threading.ThreadStart(SoundEngine));

            soundengine_thread.IsBackground = true;

            soundengine_thread.Start();
        }

        private void SoundEngine()
        {
            Sound play;

            while (true == true)
            {
                while (sounds.Count > 0)
                {
                    play = (Sound)sounds.Dequeue();

                    if (System.DateTime.Now.Ticks <= play.PlayBefore)
                    {
                        if (play.isFile)
                        {
                            try
                            {
                                SoundPlayer snd = new SoundPlayer(play.Name);
                                snd.PlaySync();//.Play();
                            }
                            catch
                            {
                                Globals.l2net_home.Add_PopUpError("crash: Media Sound : playwav from file ", false);
                            }
                        }
                        else
                        {
                            try
                            {
                                // get the resource into a stream
                                System.IO.Stream str = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(Globals.SOUND_NAMESPACE + play.Name);

                                if (str == null)
                                    return;

                                // play the resource
                                SoundPlayer snd = new SoundPlayer(str);
                                snd.PlaySync();//.Play();
                            }
                            catch
                            {
                                Globals.l2net_home.Add_PopUpError("crash: Media Sound : playwav from resource", false);
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(Globals.SLEEP_SoundEngine);
            }
        }
       
		public void PlayWavFile(string fname)
		{
            Sound snd = new Sound();
            snd.Name = fname;
            snd.isFile = true;
            snd.PlayBefore = System.DateTime.Now.Ticks + Globals.MAX_SOUND_DELAY;

            sounds.Enqueue(snd);
        }
 
		public void PlayWavResource(string wav)
		{
            Sound snd = new Sound();
            snd.Name = wav;
            snd.isFile = false;
            snd.PlayBefore = System.DateTime.Now.Ticks + Globals.MAX_SOUND_DELAY;

            sounds.Enqueue(snd);
        }
	}//end of class
}
