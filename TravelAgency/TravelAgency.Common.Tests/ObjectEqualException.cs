﻿using Xunit.Sdk;

namespace TravelAgency.Common.Tests;

public class ObjectEqualException : AssertActualExpectedException
{
    public ObjectEqualException(object? expected, object? actual, string message)
        : base(expected, actual, "Assert.Equal() Failure")
    {
        Message = message;
    }

    public override string Message { get; }
}