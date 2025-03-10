
namespace UniSearch.Extensions
{
    public class CustomStatusResponse
    {


        public static ApiResponse GetResponse(int NetworkCode)
        {
            if (NetworkCode == 200) return new ApiResponse()
            {
                NetworkStatus = 200
            };

            else if (NetworkCode == 401) return new ApiResponse()
            {
                NetworkStatus = 401,
                message = "Unauthorized access"
            };

            else if (NetworkCode == 403) return new ApiResponse()
            {
                NetworkStatus = 403,
                message = "You don’t have permission to this action."
            };

            else if (NetworkCode == 320) return new ApiResponse()
            {
                NetworkStatus = 320,
                message = "User with these credential not exist."
            };

            else if (NetworkCode == 500) return new ApiResponse()
            {
                NetworkStatus = 500,
                message = "Internal server error"
            };

            else if (NetworkCode == 600) return new ApiResponse()
            {
                NetworkStatus = 600
            };

            else return new ApiResponse()
            {
                NetworkStatus = 500,
                message = "Internal server error Error"
            };

        }
    }
}
