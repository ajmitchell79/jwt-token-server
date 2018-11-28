namespace Token.BRL.Model
{
    using System;
    using System.Collections.Generic;

    namespace TokenServer.BRL.Model
    {
        public class TokenResponse
        {
            public string AccessToken { get; set; }

            public string Type { get; set; }

            public string RefreshToken { get; set; }

            public string Issued { get; set; }

            public string Expires { get; set; }

            public int ExpiresIn { get; set; }

        }
    }

}