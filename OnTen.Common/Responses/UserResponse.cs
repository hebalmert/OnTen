using OnTen.Common.Entities;
using OnTen.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTen.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public String ImageId { get; set; }

        public string ImageFullPath => ImageId == String.Empty
            //? $"https://onsalezulu.azurewebsites.net/images/noimage.png"
            //: $"https://onsale.blob.core.windows.net/users/{ImageId}";
            ? $"http://onten.linkonext.com/Images/noimage.png"
            : $"http://onten.linkonext.com/Users/{ImageId}";

        public UserType UserType { get; set; }

        public City City { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }
}
