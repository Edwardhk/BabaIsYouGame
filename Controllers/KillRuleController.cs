namespace BabaIsYouApp.Controllers
{
    using BabaIsYouApp.Models;
    class KillRuleController: BaseRuleController
    {
        public KillRuleController(GameStateModel gs): base(gs) { }

        public override GameStateModel HandleMovement(int srcRow, int srcCol, char dir)
        {
            GameStateModel res = GetStateModel();



            return res;
        }
    }
}
