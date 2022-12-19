// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020-2022. rollrat. Licensed under the MIT Licence.

using alarmbot.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace alarmbot.Postprocessor
{
    /// <summary>
    /// Postprocessor interface
    /// </summary>
    public abstract class IPostprocessor
    {
        public abstract void Run(NetTask task);
    }
}
