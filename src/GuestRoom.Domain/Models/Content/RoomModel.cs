﻿namespace GuestRoom.Domain.Models.Content
{
    public class RoomModel : ContentBaseModel
    {
        public string Furnishing { get; set; }
        public string Pricing { get; set; }
        public MiscellaneousModel Miscellaneous { get; set; }
    }
}