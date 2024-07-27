namespace GoodsExchange.BusinessLogic.Constants
{
    public sealed class SystemConstant
    {
        public sealed class Roles
        {
            public const string Guest = "Guest";
            public const string User = "User";
            public const string Administrator = "Administrator";
            public const string Moderator = "Moderator";
        }
        public sealed class Images
        {
            public const string UserImageDefault = "https://firebasestorage.googleapis.com/v0/b/fir-project-31c70.appspot.com/o/Images%2F1b789a16-21bb-43b2-8b45-c7ab29a98fe2_user_avatar_def.jfif?alt=media&token=df7abe8e-a87a-4894-a6b3-8a5d2775d7e1";
        }
        public sealed class ProductStatus
        {
            public const string AwaitingApproval = "Awaiting approval";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
            public const string Hidden = "Hidden";
            public const string AreExchanging = "Exchange is in progress";
            public const string ExchangeSuccessful = "Exchange successful";
        }
        public sealed class ExchangeRequestStatus
        {
            public const string Created = "Created";
            public const string Approved = "Approved";
            public const string Complete = "Complete";
        }
    }
}
