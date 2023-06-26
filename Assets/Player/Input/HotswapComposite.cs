using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace Grav.Players.Input {

    #if UNITY_EDITOR
    [InitializeOnLoad] // Automatically register in editor.
    #endif
    [DisplayStringFormat("{one}+{two}+{three}+{four}")]
    public class HotswapComposite : InputBindingComposite<int> {

#if UNITY_EDITOR
        static HotswapComposite () {
            InputSystem.RegisterBindingComposite<HotswapComposite>();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize () {
            new HotswapComposite();
        }

        [InputControl(layout = "Button")]
        public int one;
        [InputControl(layout = "Button")]
        public int two;
        [InputControl(layout = "Button")]
        public int three;
        [InputControl(layout = "Button")]
        public int four;

        public override int ReadValue (ref InputBindingCompositeContext context) {
            if (context.ReadValue<float>(one) > 0) return 1;
            if (context.ReadValue<float>(two) > 0) return 2;
            if (context.ReadValue<float>(three) > 0) return 3;
            if (context.ReadValue<float>(four) > 0) return 4;
            return 0;
		}
    }
}