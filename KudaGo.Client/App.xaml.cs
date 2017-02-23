using DailyEvents.Client.Common;
using DailyEvents.Client.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DailyEvents.Client
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private static IDataSource _dataSource;
        private static SettingsChangeNotifier _settingsNotifier;
        private static ISettingsProvider _settingsProvider;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;

            //todo
            var lang = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            var culture = new CultureInfo(lang);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            _settingsProvider = new SettingsProvider();
            _settingsNotifier = new SettingsChangeNotifier();

            var location = _settingsProvider.GetLocation();
            _dataSource = new DataSource(location);
        }

        public static IDataSource DataSource
        {
            get { return _dataSource; }
        }

        internal static ISettingsChangeNotifier SettingsNotifier
        {
            get { return _settingsNotifier; }
        }

        internal static ISettingsProvider SettingsProvider
        {
            get { return _settingsProvider; }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }

            // Register a global back event handler. This can be registered on a per-page-bases if you only have a subset of your pages
            // that needs to handle back or if you want to do page-specific logic before deciding to navigate back on those pages.
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            await InitializeStatusBar();
        }

        private async Task InitializeStatusBar()
        {
            var accentBrush = Application.Current.Resources["ApplicationAccentBrush"] as SolidColorBrush;
            var accentDarkBrush1 = Application.Current.Resources["ApplicationDarkAccentBrush1"] as SolidColorBrush;
            var accentDarkBrush2 = Application.Current.Resources["ApplicationDarkAccentBrush2"] as SolidColorBrush;
            var accentDarkBrush3 = Application.Current.Resources["ApplicationDarkAccentBrush3"] as SolidColorBrush;

            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = accentBrush.Color;
                    titleBar.ButtonHoverBackgroundColor = accentDarkBrush1.Color;
                    titleBar.ButtonPressedBackgroundColor = accentDarkBrush2.Color;
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.ButtonHoverForegroundColor = Colors.White;
                    titleBar.ButtonPressedForegroundColor = Colors.White;
                    titleBar.BackgroundColor = accentBrush.Color;
                    titleBar.ForegroundColor = Colors.White;
                    titleBar.InactiveBackgroundColor = accentBrush.Color;
                    titleBar.InactiveForegroundColor = Colors.White;
                    titleBar.ButtonInactiveBackgroundColor = accentBrush.Color;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = accentBrush.Color;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        /// <summary>
        /// Invoked when a user issues a global back on the device.
        /// If the app has no in-app back stack left for the current view/frame the user may be navigated away
        /// back to the previous app in the system's app back stack or to the start screen.
        /// In windowed mode on desktop there is no system app back stack and the user will stay in the app even when the in-app back stack is depleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // If we can go back and the event has not already been handled, do so.
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
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

        private async void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var dialog = new MessageDialog(e.Message);
            await dialog.ShowAsync();
        }
    }
}
