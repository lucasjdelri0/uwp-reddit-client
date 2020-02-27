﻿using System;
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
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private static readonly string REDDIT_USER = Environment.GetEnvironmentVariable("REDDIT_USER");
        private static readonly string REDDIT_PASSWORD = Environment.GetEnvironmentVariable("REDDIT_PASSWORD");
        private static readonly string REDDIT_CLIENT_ID = Environment.GetEnvironmentVariable("REDDIT_CLIENT_ID");
        private static readonly string REDDIT_CLIENT_SECRET = Environment.GetEnvironmentVariable("REDDIT_CLIENT_SECRET");

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
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

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage));
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
