using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public static class ApiHeaders
    {
        private static List<string> _publicHeaders;

        private static List<string> _publicHeadersWithSignature;

        private static List<string> _privateHeaders;

        private static List<string> _privateHeadersWithSignature;

        public static List<string> PublicHeaders
        {
            get
            {
                _publicHeaders = new List<string>
                {
                    TOKEN,
                    PORTAL
                };

                return _publicHeaders;
            }
        }

        public static List<string> PpublicHeadersWithSignature
        {
            get
            {
                _publicHeadersWithSignature = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    NONCE,
                    CONTENT_HASH,
                    SIGNATURE
                };

                return _publicHeadersWithSignature;
            }
        }

        public static List<string> PrivateHeaders
        {
            get
            {
                _privateHeaders = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    AUTHORIZATION
                };

                return _privateHeaders;
            }
        }

        public static List<string> PrivateHeadersWithSignature
        {
            get
            {
                _privateHeadersWithSignature = new List<string>
                {
                    TOKEN,
                    PORTAL,
                    NONCE,
                    CONTENT_HASH,
                    SIGNATURE,
                    AUTHORIZATION
                };

                return _privateHeadersWithSignature;
            }
        }

        public static readonly string PORTAL = "Api-X-Portal";

        public static readonly string TOKEN = "Api-X-Token";

        public static readonly string CONTENT_HASH = "Api-X-Content-Hash";

        public static readonly string SIGNATURE = "Api-X-Signature";

        public static readonly string NONCE = "Api-X-Nonce ";

        public static readonly string AUTHORIZATION = "Authorization";

        public static readonly string CONVERTIBLE = "Api-x-Convertible";

        public static readonly string USER_AGENT = "User-Agent";
    }
}
