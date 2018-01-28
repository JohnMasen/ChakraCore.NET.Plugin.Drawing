using System;
using System.Collections.Generic;
using System.Text;

namespace ChakraCore.NET.Plugin.Drawing
{
    public enum BlendModeEnum:int
    {
        //
        // Summary:
        //     Default blending mode, also known as "Normal" or "Alpha Blending"
        Normal = 0,
        //
        // Summary:
        //     Blends the 2 values by multiplication.
        Multiply = 1,
        //
        // Summary:
        //     Blends the 2 values by addition.
        Add = 2,
        //
        // Summary:
        //     Blends the 2 values by subtraction.
        Substract = 3,
        //
        // Summary:
        //     Multiplies the complements of the backdrop and source values, then complements
        //     the result.
        Screen = 4,
        //
        // Summary:
        //     Selects the minimum of the backdrop and source values.
        Darken = 5,
        //
        // Summary:
        //     Selects the max of the backdrop and source values.
        Lighten = 6,
        //
        // Summary:
        //     Multiplies or screens the values, depending on the backdrop vector values.
        Overlay = 7,
        //
        // Summary:
        //     Multiplies or screens the colors, depending on the source value.
        HardLight = 8,
        //
        // Summary:
        //     returns the source colors
        Src = 9,
        //
        // Summary:
        //     returns the source over the destination
        Atop = 10,
        //
        // Summary:
        //     returns the detination over the source
        Over = 11,
        //
        // Summary:
        //     the source where the desitnation and source overlap
        In = 12,
        //
        // Summary:
        //     the destination where the desitnation and source overlap
        Out = 13,
        //
        // Summary:
        //     the destination where the source does not overlap it
        Dest = 14,
        //
        // Summary:
        //     the source where they dont overlap othersie dest in overlapping parts
        DestAtop = 15,
        //
        // Summary:
        //     the destnation over the source
        DestOver = 16,
        //
        // Summary:
        //     the destination where the desitnation and source overlap
        DestIn = 17,
        //
        // Summary:
        //     the source where the desitnation and source overlap
        DestOut = 18,
        //
        // Summary:
        //     the clear.
        Clear = 19,
        //
        // Summary:
        //     clear where they overlap
        Xor = 20

    }

}
