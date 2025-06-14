﻿namespace VehicleInformationAPI.Models
{
    public class AuthorizationResponse
    {
        public string token_type { get; set; } = string.Empty;
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; } = string.Empty;
    }
}
