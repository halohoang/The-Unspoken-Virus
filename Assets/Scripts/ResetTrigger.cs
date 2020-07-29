using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SAE_Project
{
	public class ResetTrigger : StateMachineBehaviour
	{
		[Tooltip("The trigger you wish to reset")]
		[SerializeField]
		private string _trigger;

		public override void OnStateEnter (Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
		{
			animator.ResetTrigger(_trigger);
		}
	}
}
