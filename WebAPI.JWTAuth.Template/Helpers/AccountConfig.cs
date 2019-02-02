using System;

namespace WebAPI.JWTAuth.Template.Helpers
{
    /// <summary>
    /// Contains sensitive configutation information that is passed to the app during startup.
    /// </summary>
    public class AccountConfig
    {
        private string _hashingSecret;
        public string HashingSecret
        {
            get { return _hashingSecret; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 16)
                {
                    throw new ArgumentException("The argument --hashingsecret must be a string at least 16 characters long.");
                }

                _hashingSecret = value;
            } 
        }
        public string SignupCode { get; set; }
    }
}
