using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace Grav.Players.Input {

    //This script doesn't work, using this composite causes the state to be checked even on focus change
    //It ends up calling Switch multiple times (depends on focus switching, focus switching seems to trigger it even more)
    //Thought it may relate to having the Action registered as a Value, but changing to Pass-Through with no initial state
    //Check hasn't fixed

    #if UNITY_EDITOR
    [InitializeOnLoad] // Automatically register in editor.
    #endif
    //[DisplayStringFormat("{one}+{two}+{three}+{four}")]
    public class HotswapComposite : InputBindingComposite<int> {

        #if UNITY_EDITOR
        static HotswapComposite () {
            InputSystem.RegisterBindingComposite<HotswapComposite>();
        }
        #endif

        /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize () {
            new HotswapComposite();
        }*/

        [InputControl(layout = "Button")]
        public int one;
        [InputControl(layout = "Button")]
        public int two;
        [InputControl(layout = "Button")]
        public int three;
        [InputControl(layout = "Button")]
        public int four;

        public override int ReadValue (ref InputBindingCompositeContext context) {
            /*if (context.ReadValue<float>(one) > 0) return 1;
            if (context.ReadValue<float>(two) > 0) return 2;
            if (context.ReadValue<float>(three) > 0) return 3;
            if (context.ReadValue<float>(four) > 0) return 4;*/

            if (context.ReadValueAsButton(one)) return 0;
			if (context.ReadValueAsButton(two)) return 1;
			if (context.ReadValueAsButton(three)) return 2;
			if (context.ReadValueAsButton(four)) return 3;

			return 0;
		}
    }
}