using SAE_Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE_Project
{
public class PlayerMovement : MovementBase
  {
        protected override void CalculatedDirection( )
        {
            _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }


   }


}
