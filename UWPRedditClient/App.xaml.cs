using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UWPRedditClient.Entities;
using UWPRedditClient.Interfaces;
using UWPRedditClient.Services;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPRedditClient
{
    sealed partial class App : Application
    {
        private static readonly string REDDIT_USER = Environment.GetEnvironmentVariable("REDDIT_USER");
        private static readonly string REDDIT_PASSWORD = Environment.GetEnvironmentVariable("REDDIT_PASSWORD");
        private static readonly string REDDIT_CLIENT_ID = Environment.GetEnvironmentVariable("REDDIT_CLIENT_ID");
        private static readonly string REDDIT_CLIENT_SECRET = Environment.GetEnvironmentVariable("REDDIT_CLIENT_SECRET");

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public async Task<List<Post>> Main()
        {
            IAuthService authService = AuthService.GetAuthService();
            var authInfo = await authService.GetAuthInfo(REDDIT_USER, REDDIT_PASSWORD, REDDIT_CLIENT_ID, REDDIT_CLIENT_SECRET);

            IRedditService redditService = new RedditService(authInfo);
            return await redditService.GetTopPostsByLimit(50);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage));
                }

                Window.Current.Content = rootFrame;
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
