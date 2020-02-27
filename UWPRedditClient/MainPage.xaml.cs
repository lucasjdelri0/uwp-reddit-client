using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPRedditClient.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UWPRedditClient
{
    public sealed partial class MainPage : Page
    {
        public static MainPage Current; 
        private ItemViewModel _lastSelectedPost;
        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (Items.Count == 0)
            {
                foreach (var post in await ((App)Application.Current).Main())
                {
                    Items.Add(ItemViewModel.FromPostToItemViewModel(post));
                }
            }
            if (e.Parameter != null)
            {
                var id = (int)e.Parameter;
                _lastSelectedPost = Items.Where((post) => post.PostId == id).FirstOrDefault();
            }

            UpdateForVisualState(AdaptiveStates.CurrentState);
            DisableContentTransitions();
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;

            if (isNarrow && oldState == DefaultState && _lastSelectedPost != null)
            {
                Frame.Navigate(typeof(DetailPage), _lastSelectedPost.PostId, new SuppressNavigationTransitionInfo());
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (ItemViewModel)e.ClickedItem;
            _lastSelectedPost = clickedItem;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(DetailPage), clickedItem.PostId, new DrillInNavigationTransitionInfo());
            }
            else
            {
                EnableContentTransitions();
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            MasterListView.SelectedItem = _lastSelectedPost;
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }

        public ItemViewModel GetPostById(int id)
        {
            return Items[id];
        }
    }
}
