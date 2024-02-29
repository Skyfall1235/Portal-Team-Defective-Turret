/* ================================================================
   ----------------------------------------------------------------
   Project   :   Aurora FPS Engine
   Publisher :   Renowned Games
   Developer :   Tamerlan Shakirov
   ----------------------------------------------------------------
   Copyright 2022 Renowned Games All rights reserved.
   ================================================================ */

using UnityEngine;

namespace AuroraFPSRuntime
{
    public interface IControllerInput
    {
        Vector2 GetMovementInput();
        
        float GetHorizontalInput();

        float GetVerticalInput();
    }
}