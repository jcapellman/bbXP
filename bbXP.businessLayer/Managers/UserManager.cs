using System.Threading.Tasks;

using bbXP.dataLayer.Context;
using bbXP.dataLayer.Models;

namespace bbXP.businessLayer.Managers {
	public class UserManager : BaseManager {
		public async Task<bool> CreateNewUser(User newUser) {
			using (var pContext = new UserDataContext()) {
				pContext.Users.Add(newUser);
				return await pContext.SaveChangesAsync() > 0;
			}
		}

		public async Task<bool> UpdatePost(User revisedUser) {
			using (var pContext = new UserDataContext()) {
				pContext.Users.Update(revisedUser);
				return await pContext.SaveChangesAsync() > 0;
			}
		}

		public async Task<bool> DeletePost(User user) {
			using (var pContext = new UserDataContext()) {
				pContext.Users.Remove(user);
				return await pContext.SaveChangesAsync() > 0;
			}
		}
	}
}