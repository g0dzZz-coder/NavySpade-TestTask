using UnityEngine;

namespace NavySpade.Gameplay
{
    using Core;

    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(ISelector))]
    [RequireComponent(typeof(ISelectionResponse))]
    public class SelectionManager : MonoBehaviour
    {
        private IRayProvider rayProvider = null;
        private ISelector selector;
        private ISelectionResponse response;

        private Transform currentSelection;

        private void Awake()
        {
            enabled = false;

            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
            response = GetComponent<ISelectionResponse>();

            Game.Restarted += () => enabled = true;
            Game.Ended += flag => enabled = false;
        }

        private void Update()
        {
            if (rayProvider.IsInteracted() == false)
                return;

            selector.Check(rayProvider.CreateRay());

            var selection = selector.GetSelection();
            if (selection == null)
                return;

            if (currentSelection != null && selection != currentSelection)
                response.OnDeselect(currentSelection);

            currentSelection = selection;
            response.OnSelect(currentSelection);
        }
    }
}