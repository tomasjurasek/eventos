﻿using Dawn;
using EventPlanning.Domain.Common;
using FluentResults;

namespace EventPlanning.Domain.Registration
{
    public class RegistrationAggregate : AggregateRoot
    {
        internal RegistrationAggregate(string id, string eventId, Attendee attendee) : base(id, DateTimeOffset.UtcNow)
        {
            Attendee = Guard.Argument(attendee).NotNull();
            EventId = Guard.Argument(eventId).NotNull().NotEmpty();
            State = RegistrationState.Waiting;
        }

        public string EventId { get; }

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
                return Result.Fail("REGISTRATION_IS_NOT_IN_WAITING_STATE");
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
                return Result.Fail("REGISTRATION_IS_NOT_IN_WAITING_STATE");
            }

            return Result.Ok();
        }


        public Result Cancel(DateTimeOffset eventStartAt, string userEmail)
        {
            if (eventStartAt <= DateTimeOffset.UtcNow.AddHours(4))
            {
                return Result.Fail("REGISTRATION_CANNOT_BE_CANCELED");
            }

            else if (!Attendee.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
            {
                return Result.Fail("REGISTRATION_CAN_BE_CANCELED_ONLY_BY_ATTENDEE");
            }

            State = RegistrationState.Canceled;

            return Result.Ok();
        }

    }
}