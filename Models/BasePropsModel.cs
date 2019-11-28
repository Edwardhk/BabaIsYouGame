namespace BabaIsYouApp.Models
{
    public class BasePropsModel
    {
        private bool _canPush;
        private bool _canControl;
        private bool _canKill;
        private bool _canWin; 

        public bool CanPush {
            get { return _canPush; }
            set { _canPush = value; }
        }
        public bool CanControl
        {
            get { return _canControl; }
            set { _canControl = value; }
        }
        public bool CanKill
        {
            get { return _canKill; }
            set { _canKill = value; }
        }
        public bool CanWin
        {
            get { return _canWin; }
            set { _canWin = value; }
        }
    }
}
