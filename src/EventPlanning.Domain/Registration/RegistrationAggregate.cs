﻿using Dawn;
using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Domain.Registration
{
    public class RegistrationAggregate : IAggregate
    {
        internal RegistrationAggregate(string id, string eventId, Attendee attendee)
        {
            Id = Guard.Argument(id).NotNull().NotEmpty();
            Attendee = Guard.Argument(attendee).NotNull();
            EventId = Guard.Argument(eventId).NotNull().NotEmpty();
            CreatedAt = DateTimeOffset.UtcNow;
            State = RegistrationState.Waiting;
        }

        public string Id { get; }

        public string EventId { get; }

        public DateTimeOffset CreatedAt { get; }

        public Attendee Attendee { get; private set; }

        public RegistrationState State { get; private set; }


        public Result Accept()
        {
            if (State == RegistrationState.Waiting)
            {
                State = RegistrationState.Accepted;
            }
            else
            {
                return Result.Fail("Registration is not in a waiting state.");
            }

            return Result.Ok();
        }

        public Result Decline()
        {
            if (State == RegistrationState.Waiting)
            {
                State = RegistrationState.Declined;
            }
            else
            {
                return Result.Fail("Registration is not in a waiting state.");
            }

            return Result.Ok();
        }


        public Result Cancel(DateTimeOffset eventStartAt, string userEmail)
        {
            if (eventStartAt <= DateTimeOffset.UtcNow.AddHours(4))
            {
                return Result.Fail("Registration cannot be cancelled.");
            }

            else if (!Attendee.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
            {
                return Result.Fail("Registration can be canceled only by attendee.");
            }

            State = RegistrationState.Canceled;

            return Result.Ok();
        }

    }
}
