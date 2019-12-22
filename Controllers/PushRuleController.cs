namespace BabaIsYouApp.Controllers
{
    using BabaIsYouApp.Models;
    using System;

    class PushRuleController : BaseRuleController
    {
        public PushRuleController(GameStateModel gs) : base(gs) { }

        public override GameStateModel HandleMovement(int srcRow, int srcCol, char dir)
        {
            GameStateModel res = GetStateModel();



            return res;
        }
    }
}
