using System.Linq;
using System.Threading.Tasks;

using bbXP.dataLayer.Context;
using bbXP.dataLayer.Models;

namespace bbXP.businessLayer.Managers {
    public class PostManager : BaseManager {
	    public async Task<bool> CreateNewPost(Post newPost) {
		    using (var pContext = new PostDataContext()) {
			    pContext.Posts.Add(newPost);
			    return await pContext.SaveChangesAsync() > 0;
		    }
	    }

		public Post GetPostForAdmin(int postID) {
			using (var pContext = new PostDataContext()) {
				return pContext.Posts.FirstOrDefault(a => a.ID == postID);
			}
		}

	    public async Task<bool> UpdatePost(Post revisedPost) {
			using (var pContext = new PostDataContext()) {
				pContext.Posts.Update(revisedPost);
				return await pContext.SaveChangesAsync() > 0;
			}
		}

	    public async Task<bool> DeletePost(Post post) {
		    using (var pContext = new PostDataContext()) {
			    pContext.Posts.Remove(post);
			    return await pContext.SaveChangesAsync() > 0;
		    }
	    }
    }
}