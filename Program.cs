﻿using System;
using System.Runtime.InteropServices;

namespace NtDllImport
{
    class Program
    {
        private static uint STATUS_ASSERTION_FAILURE = 0xC0000420;

        static void Main(string[] args)
        {
            Console.WriteLine("Adjusting privileges");
            RtlAdjustPrivilege(19, true, false, out bool previousValue);
            Console.WriteLine("Triggering BSOD");
            NtRaiseHardError(STATUS_ASSERTION_FAILURE, 0, 0, (IntPtr)0, 6, out uint oul);
            Console.WriteLine("Done");
        }

        [DllImport("ntdll.dll")]
        private static extern uint RtlAdjustPrivilege(
            int Privilege,
            bool bEnablePrivilege,
            bool IsThreadPrivilege,
            out bool PreviousValue
        );

        [DllImport("ntdll.dll")]
        private static extern uint NtRaiseHardError(
            uint ErrorStatus,
            uint NumberOfParameters,
            uint UnicodeStringParameterMask,
            IntPtr Parameters,
            uint ValidResponseOption,
            out uint Response
        );
    }
}