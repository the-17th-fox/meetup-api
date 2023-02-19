using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public class LogEvents
    {
        public const string ExceptionForm = "[{exception}] has occured with status code [{statusCode}]: [{message}]";

        // Users' auth operations
        public const string LoginAttempt = "The user [{email}] is trying to log in";
        public const string TokenReturned = "JWT was successfully signed and returned to [{email}]";
        public const string LogOutAttempt = "The user [{email}] is trying to log out";
        public const string LogOutSucceeded = "The user [{email}] has successfully loged out";

        // Tokens management
        public const string TokenRefreshingAttempt = "The user [{email}]:[{id}] is trying to refresh access and refresh tokens";
        public const string TokenRefreshingSucceeded = "The user [{email}]:[{id}] has successfully refreshed access and refresh tokens";

        // Meetups management
        public const string MeetupRetrievingAttemptById = "The user [{email}]:[{id}] is trying to retrieve meetup by id: [{meetupId}]";
        public const string MeetupRetrievingByIdSucceeded = "The user [{email}]:[{id}] has successfully retrieved meetup by id: [{meetupId}]";

        public const string PagedMeetupRetrievingAttempt = "The user [{email}]:[{id}] is trying to retrieve meetups page [{page}] with page size [{size}]";
        public const string PagedMeetupRetrievingSucceeded = "The user [{email}]:[{id}] has successfully retrieved meetups page [{page}] with page size [{size}]";

        public const string MeetupCreationAttempt = "The user [{email}]:[{id}] is trying to create meetup: [{meetup}]";
        public const string MeetupCreationSucceeded = "The user [{email}]:[{id}] has successfully created meetup: [{meetup}]";

        public const string MeetupDeletionAttempt = "The user [{email}]:[{id}] is trying to deleted the meetup [{meetupId}]";
        public const string MeetupDeletionSucceeded = "The user [{email}]:[{id}] has successfully deleted the meetup [{meetupId}]";

        public const string MeetupUpdatingAttempt = "The user [{email}]:[{id}] is trying to create meetup [{meetupId}] with [{newMeetup}]";
        public const string MeetupUpdatingSucceded = "The user [{email}]:[{id}] has successfully updated meetup [{meetupId}] with [{newMeetup}]";
    }
}
