using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Nucleus
{
    /// <summary>
    /// Main class that manages everything used in application-development as a singleton
    /// </summary>
    public class Core
    {
        private static object locker = new object();

        private static Core instance;
        public static Core Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        new Core();
                    }
                    return instance;
                }
            }
        }

        private ResourceManager resourceManager;
        private PlatformManager platformManager;
        private Timer timer;

        public ResourceManager ResourceManager
        {
            get { return resourceManager; }
            set { resourceManager = value; }
        }

        public PlatformManager PlatformManager
        {
            get { return platformManager; }
            set { platformManager = value; }
        }

        private Core()
        {
            lock (locker)
            {
                instance = this;
                resourceManager = new ResourceManager();
                platformManager = new PlatformManager();

                timer = new Timer();
                timer.Interval = 16;
                timer.Elapsed += timer_Elapsed;
                timer.Enabled = true;
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            update();
        }

        public event Action OnUpdate;

        private void update()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }

        public static void Restart()
        {
            if (instance == null)
            {
                new Core();
            }
            else
            {
                PlatformManager plat = instance.PlatformManager;
                lock (locker)
                {
                    instance.resourceManager.Dispose();
                    instance.platformManager.Dispose();
                    instance = null;
                }
                Core core = new Core();
                plat.CopyTo(core.PlatformManager);
            }
        }

        public static double PcWidth(double pc)
        {
            double width = Instance.PlatformManager.GetScreenWidth();
            return width * pc;
        }

        public static double PcHeight(double pc)
        {
            double height = Instance.PlatformManager.GetScreenHeight();
            return height * pc;
        }
    }
}