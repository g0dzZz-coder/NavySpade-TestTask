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

        protected override void Awake()
        {
            base.Awake();
            Controls.Enable();

            Level.Instance.Restarted += Controls.Enable;
            Level.Instance.GameEnded += Controls.Disable;
        }
    }
}