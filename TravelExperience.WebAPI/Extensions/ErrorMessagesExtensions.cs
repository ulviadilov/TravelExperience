using TravelExperience.WebAPI.Enums;

namespace TravelExperience.WebAPI.Extensions
{
    public static class ErrorMessagesExtensions
    {
        public static string GetMessage(this ErrorMessages error)
        {
            return error switch
            {
                ErrorMessages.TripNotFound => "Trip not found.",
                ErrorMessages.TripCreationFailed => "An error occurred while creating the trip.",
                ErrorMessages.GetTripsFailed => "An error occurred while retrieving trips.",
                ErrorMessages.GetTripByIdFailed => "An error occurred while retrieving the trip.",
                ErrorMessages.GetDestinationsFailed => "An error occurred while retrieving destinations.",
                _ => "An unexpected error occurred."
            };
        }
    }
}
