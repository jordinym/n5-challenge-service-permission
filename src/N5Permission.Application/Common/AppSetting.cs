namespace N5Permission.Application.Common
{
    public class AppSetting
    {
        public string Secret { get; set; }
        public N5PermissionSettings ServicePermissionSettings { get; set; }
        public ConnectionString ConnectionStrings { get; set; }
        public ProducerProperties ProducerSettings { get; set; }

        public class ConnectionString
        {
            public string DefaultConnection { get; set; }
        }

        public class BaseApi
        {
            public string Url { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class N5PermissionSettings : BaseApi
        {
            public int HoursOfExpires { get; set; }
            public int MinutesOfExpires { get; set; }
            public string ApiUrl { get; set; }
        }
        public class ProducerProperties  
        {
            public string TopicName { get; set; }
        }

    }
}
