using System.Collections;

namespace MinimalChatApplicationAPI.Utils.Helpers
{
    public static class HelperService
    {
        public static bool IsListNullOrEmpty(this IList? list) => list == null || list.Count == 0;
    }
}
