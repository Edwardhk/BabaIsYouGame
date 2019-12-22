namespace BabaIsYouApp.Controllers
{
    class RuleBaseObject
    {
        private string _name;
        private bool _canPush;
        private bool _canControl;
        private bool _canKill;
        private bool _canWin;
        private bool _canStop;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool CanPush
        {
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
        public bool CanStop
        {
            get { return _canStop; }
            set { _canStop = value; }
        }


        public RuleBaseObject(string name)
        {
            _name = name;
            _canPush = false;
            _canControl = false;
            _canKill = false;
            _canWin = false;
            _canStop = false;
        }
    }
}
