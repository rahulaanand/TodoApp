using System.Security.Cryptography;
using System.Text;

namespace LoginModule.LoginModule
{
    public class HashPassword
    {
        public static string Hash(string password, int maxLength = 50)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder(hashedBytes.Length * 2);
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("X2"));
                }

                string hashedPassword = builder.ToString().ToLower();

                if (hashedPassword.Length > maxLength)
                {
                    hashedPassword = hashedPassword.Substring(0, maxLength);
                }

                return hashedPassword;
            }
        }
    }
}
