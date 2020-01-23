// <copyright file="IDtoMethods.cs" company="Erratic Motion Ltd">
// Copyright (c) Erratic Motion Ltd. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DynamicDto.Objects
{
    using System;

    public interface IDtoMethods
    {
        int CardInfoID();
        long CardNumber();
        bool IsValid();
        DateTime CardUpdated();
        string CardNumberDisplay();
    }
}