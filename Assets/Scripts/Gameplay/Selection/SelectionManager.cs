using System;
using UnityEngine;

namespace NavySpade.Gameplay
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(ISelector))]
    [RequireComponent(typeof(ISelectionResponse))]
    public class SelectionManager : MonoBehaviour
    {
        public event Action<Transform> SelectionChanged = null;

        private IRayProvider rayProvider = null;
        private ISelector selector;
        private ISelectionResponse response;

        private Transform currentSelection;

        private void Awake()
        {
            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
            response = GetComponent<ISelectionResponse>();
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

            SelectionChanged?.Invoke(currentSelection);
        }
    }
}