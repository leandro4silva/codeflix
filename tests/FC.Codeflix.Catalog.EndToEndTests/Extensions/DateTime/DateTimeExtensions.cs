﻿namespace FC.Codeflix.Catalog.EndToEndTests.Extensions.DateTime;

public static class DateTimeExtensions
{
    public static System.DateTime TrimMillisSeconds(
        this System.DateTime dateTime
    )
    {
        return new System.DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            dateTime.Microsecond,
            dateTime.Microsecond,
            dateTime.Kind
        );
    }
}
