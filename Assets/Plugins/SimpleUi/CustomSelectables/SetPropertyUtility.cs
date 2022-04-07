﻿using System.Collections.Generic;
using UnityEngine;

namespace CustomSelectables
{
    internal static class SetPropertyUtility
    {
        public static bool SetColor(ref Color currentValue, Color newValue)
        {
            if (currentValue.r - newValue.r == 0 
                && currentValue.g - newValue.g == 0 
                && currentValue.b - newValue.b == 0 
                && currentValue.a - newValue.a == 0)
                return false;

            currentValue = newValue;
            return true;
        }

        public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;

            currentValue = newValue;
            return true;
        }

        public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
        {
            if (currentValue == null && newValue == null 
                || currentValue != null && currentValue.Equals(newValue))
                return false;

            currentValue = newValue;
            return true;
        }
    }
}