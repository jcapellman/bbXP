using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using bbxp.PCL.Handlers;
using bbxp.PCL.Transports.Posts;

namespace bbxp.UWP.ViewModels {
    public class SearchViewModel : BaseViewModel {
        private ObservableCollection<PostResponseItem> _searchResults;

        public ObservableCollection<PostResponseItem> SearchPosts {
            get { return _searchResults; }
            set { _searchResults = value; OnPropertyChanged(); }
        }

        private PostResponseItem _selectedPost;

        public PostResponseItem SelectedPost {
            get { return _selectedPost; }
            set { _selectedPost = value; OnPropertyChanged(); }
        }

        public PostResponseItem GetOriginalPost() => _originalSearchResults.FirstOrDefault(a => a.RelativeURL == SelectedPost.RelativeURL);

        private List<PostResponseItem> _originalSearchResults;
        
        private string _searchQuery;
        
        public string SearchQuery {
            get { return _searchQuery; }
            set { _searchQuery = value; OnPropertyChanged();
                if (!string.IsNullOrEmpty(SearchQuery)) {
                    Search();
                }
            }
        }

        public async void Search() {
            var searchHandler = new SearchHandler(GSettings);

            var results = await searchHandler.SearchPosts(SearchQuery);

            _originalSearchResults = results.ReturnValue.Select(a => new PostResponseItem(a)).ToList();

            foreach (var item in results.ReturnValue) {
                item.Body = await GenerateFinalRender(item.Body, true);
            }

            SearchPosts = new ObservableCollection<PostResponseItem>(results.ReturnValue);
        }
    }
}