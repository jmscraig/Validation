﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

using System;

using Validation;

using Xunit;

public class ExceptionExtensionsTests
{
    [Fact]
    public void AddData_NullException()
    {
        Assert.Throws<ArgumentNullException>(() => ExceptionExtensions.AddData<InvalidOperationException>(null!, "a", 2));
    }

    [Fact]
    public void AddData_NullKey()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new InvalidOperationException().AddData(null!, "a"));
        Assert.Equal("key", ex.ParamName);
    }

    [Fact]
    public void AddData_NullValuesArray()
    {
        var ex = new InvalidOperationException().AddData("hi", null);
        Assert.False(ex.Data.Contains("hi"));
    }

    [Fact]
    public void AddData_EmptyValuesArray()
    {
        var ex = new InvalidOperationException().AddData("hi");
        Assert.False(ex.Data.Contains("hi"));
    }

    [Fact]
    public void AddData_One()
    {
        var ex = new InvalidOperationException().AddData("hi", "1");
        Assert.Equal(new object[] { "1" }, ex.Data["hi"]);
    }

    [Fact]
    public void AddData_TwoWithSameKey()
    {
        var ex = new InvalidOperationException().AddData("hi", "1", "2");
        Assert.Equal(new object[] { "1", "2" }, ex.Data["hi"]);
    }

    [Fact]
    public void AddData_TwoKeys()
    {
        var ex = new InvalidOperationException()
            .AddData("hi", "1", "2")
            .AddData("bye", "3");
        Assert.Equal(new object[] { "1", "2" }, ex.Data["hi"]);
        Assert.Equal(new object[] { "3" }, ex.Data["bye"]);
    }
}
