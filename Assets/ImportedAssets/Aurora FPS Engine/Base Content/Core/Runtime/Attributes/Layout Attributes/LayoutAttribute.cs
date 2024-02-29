﻿/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Renowned Games
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright 2022 Renowned Games All rights reserved.
   ================================================================ */

using System;

namespace AuroraFPSRuntime.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class LayoutAttribute : ApexBaseAttribute
    {
        public readonly string name;

        protected LayoutAttribute(string name)
        {
            this.name = name;
        }
    }
}
