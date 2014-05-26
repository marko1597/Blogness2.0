using System.Text.RegularExpressions;

namespace Blog.Common.Utils.Helpers
{
    public static class PasswordManager
    {
        public static Constants.PasswordScore CheckStrength(string password)
        {
            var score = 1;

            if (password.Length < 1)
                return Constants.PasswordScore.Blank;
            if (password.Length < 4)
                return Constants.PasswordScore.VeryWeak;
            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success && Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return (Constants.PasswordScore)score;
        }
    }
}
