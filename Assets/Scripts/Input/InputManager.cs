namespace NavySpade.Input
{
    using Utils;

    public class InputManager : MonoSingleton<InputManager>
    {
        public Controls Controls
        {
            get
            {
                if (controls == null)
                    controls = new Controls();

                return controls;
            }
        }
        private Controls controls;

        private void OnEnable()
        {
            Controls.Enable();

            Level.Instance.Restarted += Controls.Hero.Enable;
            Level.Instance.GameEnded += Controls.Hero.Disable;
        }

        private void OnDisable()
        {
            Controls.Disable();

            Level.Instance.Restarted -= Controls.Hero.Enable;
            Level.Instance.GameEnded -= Controls.Hero.Disable;
        }
    }
}