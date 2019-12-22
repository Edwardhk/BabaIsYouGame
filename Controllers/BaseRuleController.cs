namespace BabaIsYouApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BabaIsYouApp.Models;

    public abstract class BaseRuleController
    {
        // Pre-defined possible prefix and suffix for rule construction
        private string[] _rulePrefix, _ruleSuffix;

        private GameStateModel _gameStateModel;

        protected BaseRuleController(GameStateModel gs)
        {
            _gameStateModel = gs;
        }

        // The derived classes need to implement Handle(), to deal with different combinations of movement
        public abstract GameStateModel HandleMovement(int srcRow, int srcCol, char dir);

        protected GameStateModel GetStateModel()
        {
            return _gameStateModel;
        }
    }
}
