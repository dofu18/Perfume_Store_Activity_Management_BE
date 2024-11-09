using Firebase.Storage;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly string _bucket;
    public FirebaseService(IConfiguration configuration)
    {
        _bucket = configuration["Firebase:StorageBucket"];
    }

    public async Task<string> GetImageUrlAsync(string fileName)
    {
        //var auth = new FirebaseAuthProvider(new FirebaseConfig("your-api-key"));
        var storage = new FirebaseStorage(_bucket);

        var imageUrl = await storage.Child("images").Child(fileName).GetDownloadUrlAsync();
        return imageUrl;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var auth = new Firebase.Auth.FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig("AIzaSyANu36ulA9B9SVKi-_LZQb0TGYN0ky4xPs"));
        var storage = new FirebaseStorage(_bucket);

        // Đặt tên cho file (có thể là unique ID hoặc thời gian)
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        using (var stream = file.OpenReadStream())
        {
            // Tải file lên Firebase Storage
            var uploadTask = await storage
                .Child("images")
                .Child(fileName)
                .PutAsync(stream);

            // Trả về URL của file đã tải lên
            return await storage.Child("images").Child(fileName).GetDownloadUrlAsync();
        }
    }

    public async Task DeleteImageAsync(string imageUrl)
    {
        var storage = new FirebaseStorage(_bucket);

        // Lấy tên file từ URL
        var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);

        // Xóa file khỏi Firebase Storage
        await storage.Child("images").Child(fileName).DeleteAsync();
    }

    public async Task<FirebaseToken> VerifyIdTokenAsync(string idToken)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            return decodedToken;
        }
        catch (FirebaseAuthException ex)
        {
            // Handle invalid token
            Console.WriteLine("Token verification failed: " + ex.Message);
            throw new UnauthorizedAccessException("Invalid Firebase ID token.", ex);
        }
    }

    /// <summary>
    /// Extracts user information from FirebaseToken.
    /// </summary>
    /// <param name="decodedToken">Decoded FirebaseToken.</param>
    /// <returns>User information.</returns>
    public UserInfo ExtractUserInfo(FirebaseToken decodedToken)
    {
        var uid = decodedToken.Uid;
        var email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : null;
        var name = decodedToken.Claims.ContainsKey("name") ? decodedToken.Claims["name"].ToString() : null;
        var picture = decodedToken.Claims.ContainsKey("picture") ? decodedToken.Claims["picture"].ToString() : null;

        return new UserInfo
        {
            UserId = uid,
            Email = email,
            DisplayName = name,
            ProfilePictureUrl = picture
        };
    }

    public async Task<UserRecord> GetUserRecordByIdTokenAsync(string idToken)
    {
        var decodedToken = await VerifyIdTokenAsync(idToken);
        var uid = decodedToken.Uid;

        // Retrieve user details from Firebase
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        return userRecord;
    }
}

public class UserInfo
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string ProfilePictureUrl { get; set; }
}